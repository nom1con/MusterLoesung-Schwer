var kundentxt = File.ReadLines("kunden.csv");

// parsen der Kunden Zeilen und Index hinzufügen
var kundenprep = Enumerable.Zip(Enumerable.Range(0, kundentxt.Count() - 1), kundentxt.Skip(1).Select(Transformations.ReturnMatchesKunde));
var kundeOrtList = kundenprep.Select(s => new KundeOrt { KID = s.First, ort = new WohnortNOid { Ort = s.Second.Ort, PLZ = s.Second.PLZ, Strasse = s.Second.Strasse, Hausnummer = s.Second.Hausnummer } });
var ortListprep = Enumerable.Zip(Enumerable.Range(0, kundentxt.Count() - 1), kundeOrtList.Select(s => s.ort).Distinct());
var ortList = ortListprep.Select(s => new DBWohnort { OID = s.First, Hausnummer = s.Second.Hausnummer, Ort = s.Second.Ort, PLZ = s.Second.PLZ, Strasse = s.Second.Strasse });
var kundeortInts = kundeOrtList.Join(ortListprep, kunde => kunde.ort, ort => ort.Second, (kunde, ort) => new { kunde.KID, ort.First });
var kundeList = kundenprep.Select(k => new DBKunde { KID = k.First, E_Mail = k.Second.E_Mail, Nachname = k.Second.Nachname, Passwort = k.Second.Passwort, Vorname = k.Second.Vorname, OID = kundeortInts.Single(o => o.KID == k.First).First });
