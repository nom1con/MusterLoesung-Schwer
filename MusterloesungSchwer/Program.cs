var kundentxt = File.ReadLines("kunden.csv");

// parsen der Kunden Zeilen und Index hinzufügen
var kundenprep = Enumerable.Zip(Enumerable.Range(0, kundentxt.Count() - 1), kundentxt.Skip(1).Select(Transformations.ReturnMatchesKunde));
var kundeOrtList = kundenprep.Select(s => new KundeOrt { KID = s.First, ort = new WohnortNOid { Ort = s.Second.Ort, PLZ = s.Second.PLZ, Strasse = s.Second.Strasse, Hausnummer = s.Second.Hausnummer } });

