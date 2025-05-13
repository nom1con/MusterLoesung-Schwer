using System;
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

        return new Kunde(
            match.Groups[1].Value,
            match.Groups[2].Value,
            match.Groups[3].Value,
            match.Groups[4].Value,
            match.Groups[5].Value,
            match.Groups[6].Value,
            match.Groups[7].Value,
            match.Groups[8].Value,
            match.Groups[9].Value
        );
    }

    public static class Produkte {




    }
}

