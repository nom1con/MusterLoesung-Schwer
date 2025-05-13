public class DBWohnort
{
    public int OID { get; set; }
    public string Ort { get; set; }
    public string PLZ { get; set; }
    public string Strasse { get; set; }
    public string Hausnummer { get; set; }
}

public class DBKunde
{
    public int KID { get; set; }
    public int OID { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string E_Mail { get; set; }
    public string Passwort { get; set; }
}

public class DBProdukt
{
    public int PID { get; set; }
    public int Bestand { get; set; }
    public int Verkaufspreis { get; set; }
    public string Name { get; set; }
    public string Beschreibung { get; set; }
    public string Artikelnummer { get; set; }
}

public class DBBestellungen
{
    public int PID { get; set; }
    public int KID { get; set; }
    public int Menge { get; set; }
}