using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class Transformations {
    public static Kunde ReturnMatchesKunde(string eintrag) {
        // Zusammensetzung des Regex-Patterns
        string pattern = @"^" +
            @"(.+?)\s+([äöüÄÖÜ\w-]+)(?=,)," +         // 1: Vorname, 2: Nachname
            @"(\d{5})\s" +                            // 3: PLZ
            @"([\w\s]+)\s" +                          // 4: Ort
            @"([a-zA-Z\.\-]+)\s" +                    // 5: Straße
            @"(\d+)," +                               // 6: Hausnummer
            @"(.+)," +                                // 7: Bestellung
            @"(.+)," +                                // 8: Mail
            @"(.+)$";                                 // 9: Passwort

        Match match = Regex.Match(eintrag, pattern);

        if ( !match.Success || match.Groups.Count < 10 ) {
            throw new ArgumentException("Eintrag konnte nicht geparst werden.");
        }

        return new Kunde { 
            Vorname = match.Groups[1].Value,
            Name = match.Groups[2].Value,
            PLZ = match.Groups[3].Value,
            Ort = match.Groups[4].Value,
            Strasse = match.Groups[5].Value,
            Hausnummer = match.Groups[6].Value,
            Bestellposten = match.Groups[7].Value,
            E_Mail = match.Groups[8].Value,
            Passwort = match.Groups[9].Value
        };
    }

    public static class Produkte {
        public static Produkt ReturnMatchesProdukt(string eintrag) {
            // Zusammensetzung des Regex-Patterns
            string pattern = @"^" +
                @"(.+),"                                          // 1: Name
                @"(\d+)€," +                                      // 2: Preis
                @"(\d+)," +                                       // 3: Bestand
                @"(.+)," +                                        // 4: Beschreibung
                @"(\d+)";                                         // 5: Artikelnummer

            Match match = Regex.Match(eintrag, pattern);

            if ( !match.Success || match.Groups.Count < 6 ) {
                throw new ArgumentException("Eintrag konnte nicht geparst werden.");
            }

            return new Produkt { 
                Name = match.Groups[1].Value,
                Verkaufsreis = match.Groups[2].Value,
                Bestand = match.Groups[3].Value,
                Beschreibung = match.Groups[4].Value,
                Artikelnummer match.Groups[5].Value
            };



        }

        public static List<Tuple<string, string>> Bestellung(string bestellungRaw) {
            List<Tuple<string, string>> ausgabe = new List<Tuple<string, string>>();
            string pattern = "(?<=^|,\s)([\\w-\\s]+?)\\s\\((\\d+)\\)";
            
            try {
                MatchCollection matches = Regex.Matches(bestellungRaw, pattern);

                foreach ( Match m in matches ) {
                    if ( m.Success ) {
                        string produkt = m.Groups[1].Value.Trim();
                        string menge = m.Groups[2].Value.Trim();
                        ausgabe.Add(Tuple.Create(produkt, menge));
                    }
                }
            } catch ( RegexMatchTimeoutException ) { }

            return ausgabe;
        }
}

