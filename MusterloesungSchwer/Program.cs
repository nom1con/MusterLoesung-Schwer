var kundentxt = File.ReadAllLines("kunden.csv");

// parsen der Kunden Zeilen und Index hinzufügen
var kundenprep = Enumerable.Zip(Enumerable.Range(0, kundentxt.Length - 1), kundentxt.Skip(1).Select(Transformations.ReturnMatchesKunde));
var kundeOrtList = kundenprep.Select(s => new KundeOrt { KID = s.First, ort = new WohnortNOid { Ort = s.Second.Ort, PLZ = s.Second.PLZ, Strasse = s.Second.Strasse, Hausnummer = s.Second.Hausnummer } });
var ortListprep = Enumerable.Zip(Enumerable.Range(0, kundentxt.Length - 1), kundeOrtList.Select(s => s.ort).Distinct());
var ortList = ortListprep.Select(s => new DBWohnort { OID = s.First, Hausnummer = s.Second.Hausnummer, Ort = s.Second.Ort, PLZ = s.Second.PLZ, Strasse = s.Second.Strasse });
Database.StoreOrte(ortList);

var kundeortInts = kundeOrtList.Join(ortListprep, kunde => kunde.ort, ort => ort.Second, (kunde, ort) => new { kunde.KID, ort.First });
var kundeList = kundenprep.Select(k => new DBKunde { KID = k.First, E_Mail = k.Second.E_Mail, Nachname = k.Second.Nachname, Passwort = k.Second.Passwort, Vorname = k.Second.Vorname, OID = kundeortInts.Single(o => o.KID == k.First).First });
Database.StoreKunden(kundeList);

var produktetxt = File.ReadAllLines("produkte.csv");
var produkteprep = Enumerable.Zip(Enumerable.Range(0, produktetxt.Length - 1), produktetxt.Skip(1).Select(Transformations.ReturnMatchesProdukt));
var produkte = produkteprep.Select(p => Transformations.DBProdukt(p.First, p.Second));
Database.StoreProdukte(produkte);

var bestellungenprep = kundenprep.Select(k => new KundenBestellungen { KID = k.First, bestellungen = Transformations.Bestellung(k.Second.Bestellposten) });
var flatendBestellungen = bestellungenprep.SelectMany(b => b.bestellungen, (k, bestellung) => new { k.KID, bestellung.Item1, bestellung.Item2 });
var bestellungen = flatendBestellungen.Join(produkte, b => b.Item1, p => p.Name, (b, p) => new DBBestellungen { KID = b.KID, Menge = int.Parse(b.Item2), PID = p.PID });
Database.StoreBestellungen(bestellungen);
