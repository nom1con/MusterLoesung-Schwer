var kundentxt = File.ReadAllLines("kunden.csv");

// parsen der Kunden Zeilen und Index hinzufügen
var kundenprep = Enumerable.Zip(Enumerable.Range(1, kundentxt.Length - 1), kundentxt.Skip(1).Select(Transformations.ReturnMatchesKunde)).ToArray();
var KundeAdresseList = kundenprep.Select(k => new KundeAdresse(k.First, new AdresseNoID(k.Second.Strasse, k.Second.Hausnummer, new OrtNoID(k.Second.Ort, k.Second.PLZ)))).ToArray();
var adresseIndexedList = Enumerable.Zip(Enumerable.Range(1, kundentxt.Length - 1), KundeAdresseList.Select(ka => ka.Adresse).Distinct()).ToArray();
var adresseOrtList = adresseIndexedList.Select(a => new AdresseOrt(a.First,a.Second.Ort)).ToArray();
var ortIndexedList = Enumerable.Zip(Enumerable.Range(1, kundentxt.Length - 1), adresseOrtList.Select(ao => ao.Ort).Distinct()).ToArray();
var OrtList = ortIndexedList.Select(o => new DBOrt(o.First, o.Second.Ortname,o.Second.PLZ));
Database.Store(OrtList);

var AdresseList = ortIndexedList.Join(adresseIndexedList, oi => oi.Second, ao => ao.Second.Ort, (oi, ai) => new DBAdresse(ai.First,oi.First,ai.Second.Strasse,ai.Second.Hausnummer));
Database.Store(AdresseList);

var Passwords = kundenprep.Select(k => new { uName = k.Second.E_Mail, passwort = PasswordHelper.GeneratePassword() }).ToArray();
File.WriteAllLines("PasswordCheat.txt", Passwords.Select(p => string.Format("Username: {0}, Passwort: {1}", p.uName, p.passwort)));

var kundeAdresseInts = KundeAdresseList.Join(adresseIndexedList, ka => ka.Adresse, ai => ai.Second, (ka, ai) =>new {kundeID = ka.KundenID, adresseID = ai.First});
var KundeList = kundeAdresseInts.Join(kundenprep, kai => kai.kundeID, k => k.First, (kai, k) =>
    new DBKunde(kai.kundeID, k.Second.Vorname, k.Second.Nachname, PasswordHelper.GeneratePhoneNumber(), PasswordHelper.GenerateDateOfBirth(),
    kai.adresseID, k.Second.E_Mail, BCrypt.Net.BCrypt.HashPassword(Passwords.First(p => p.uName == k.Second.E_Mail).passwort)));
Database.Store(KundeList);

var produktetxt = File.ReadAllLines("produkte.csv");
var produkteprep = Enumerable.Zip(Enumerable.Range(1, produktetxt.Length - 1), produktetxt.Skip(1).Select(Transformations.ReturnMatchesProdukt));
var ProdukteList = produkteprep.Select(p => new DBWare(p.First, p.Second.Name, float.Parse(p.Second.Verkaufspreis),int.Parse(p.Second.Bestand))).ToArray();
Database.Store(ProdukteList);

var bestellungenprep = kundenprep.Select(k => new KundenBestellungen(k.First, Transformations.Bestellung(k.Second.Bestellposten)));
var flatendBestellungen = bestellungenprep.SelectMany(b => b.Bestellungen, (k, bestellung) => new { k.KundenID, bestellung.Item1, bestellung.Item2 });
var BestellungenList = flatendBestellungen.Join(ProdukteList, b => b.Item1, p => p.Bezeichnung, (b, p) => new DBBestellung(b.KundenID, p.Artikelnummer, b.Item2 ));
Database.Store(BestellungenList);
