public class Kunde
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Strasse { get; set; }
    public string Hausnummer { get; set; }
    public string Ort { get; set; }
    public string PLZ { get; set; }
    public string E_Mail { get; set; }
    public string Passwort { get; set; }

    public string Bestellposten { get; set; }
}

public class Produkt
{
    public string Name { get; set; }
    public string Verkaufspreis { get; set; }
    public string Bestand { get; set; }
    public string Beschreibung { get; set; }
    public string Artikelnummer { get; set; }
}

