public record WohnortNOid
{
    public string Ort { get; set; }
    public string PLZ { get; set; }
    public string Strasse { get; set; }
    public string Hausnummer { get; set; }
}

public class KundeOrt
{
    public int KID { get; set; }
    public WohnortNOid ort { get; set; }
}

public class KundenBestellungen
{
    public int KID { get; set; }
    public List<Tuple<string, int>> bestellungen { get; set; }
}