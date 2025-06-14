﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class Transformations
{
    public static Kunde ReturnMatchesKunde(string eintrag)
    {
        // Zusammensetzung des Regex-Patterns
        string pattern = @"^" +
            @"(.+?)\s+([\u00c0-\u00ff\w-]+)(?=,)," +         // 1: Vorname, 2: Nachname
            @"([-\u00c0-\u00ff\w\s]+)\s" +                          // 3: Ort
            @"(\d{5})\s" +                            // 4: PLZ
            @"([-\u00c0-\u00ffa-zA-Z\.\-\s]+)\s" +                    // 5: Straße
            @"(\d+)," +                               // 6: Hausnummer
            @"(.+)," +                                // 7: Bestellung
            @"(.+)," +                                // 8: Mail
            @"(.+)$";                                 // 9: Passwort

        Match match = Regex.Match(eintrag, pattern);

        if (!match.Success || match.Groups.Count < 10)
        {
            Console.WriteLine(eintrag);
            throw new ArgumentException("Eintrag konnte nicht geparst werden.");
        }

        return new Kunde
        {
            Vorname = match.Groups[1].Value,
            Nachname = match.Groups[2].Value,
            Ort = match.Groups[3].Value,
            PLZ = match.Groups[4].Value,
            Strasse = match.Groups[5].Value,
            Hausnummer = match.Groups[6].Value,
            Bestellposten = match.Groups[7].Value,
            E_Mail = match.Groups[8].Value,
            Passwort = match.Groups[9].Value
        };
    }


    public static Produkt ReturnMatchesProdukt(string eintrag)
    {
        // Zusammensetzung des Regex-Patterns
        string pattern = @"^" +
            @"(.+)," +                                          // 1: Name
            @"([\d\.]+)\€," +                                      // 2: Preis
            @"([\d\.]+)," +                                       // 3: Bestand
            @"(.+)," +                                        // 4: Beschreibung
            @"([\d\.]+)";                                         // 5: Artikelnummer

        Match match = Regex.Match(eintrag, pattern);

        if (!match.Success || match.Groups.Count < 6)
        {
            Console.WriteLine(eintrag);
            throw new ArgumentException("Eintrag konnte nicht geparst werden.");
        }

        return new Produkt
        {
            Name = match.Groups[1].Value,
            Verkaufspreis = match.Groups[2].Value,
            Bestand = match.Groups[3].Value,
            Beschreibung = match.Groups[4].Value,
            Artikelnummer = match.Groups[5].Value
        };


    }


    public static List<Tuple<string, int>> Bestellung(string bestellungRaw)
    {
        List<Tuple<string, int>> ausgabe = [];
        string pattern = "(?<=^|,\\s)([\\w-\\s]+?)\\s\\((\\d+)\\)";

        try
        {
            MatchCollection matches = Regex.Matches(bestellungRaw, pattern);

            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    string produkt = m.Groups[1].Value.Trim();
                    string menge = m.Groups[2].Value.Trim();
                    ausgabe.Add(Tuple.Create(produkt, int.Parse(menge)));
                }
            }
        }
        catch (RegexMatchTimeoutException) { }

        return [.. ausgabe.GroupBy(b => b.Item1, b => b.Item2, (item, menge) => new { item, menge }).Select(b => Tuple.Create(b.item, b.menge.Sum()))];
    }

    public static DBProdukt DBProdukt(int index, Produkt produkt)
    {
        int menge = int.Parse(produkt.Bestand);
        var split = produkt.Verkaufspreis.Split('.');
        int preis = int.Parse(split[0] + split[1]);
        return new DBProdukt
        {
            PID = index,
            Artikelnummer = produkt.Artikelnummer,
            Beschreibung = produkt.Beschreibung,
            Bestand = menge,
            Name = produkt.Name,
            Verkaufspreis = preis
        };
    }
}

