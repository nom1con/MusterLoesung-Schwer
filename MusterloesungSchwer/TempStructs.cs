public class WohnortNOid
{
    public string Ort { get; set; }
    public string PLZ { get; set; }
    public string Strasse { get; set; }
    public string Hausnummer { get; set; }

    public static bool operator == (WohnortNOid a, WohnortNOid b) {
        return a.Ort == b.Ort && a.PLZ == b.PLZ && a.Strasse == b.Strasse && a.Hausnummer == b.Hausnummer;
    }

    public static bool operator !=(WohnortNOid a, WohnortNOid b) {
        return !(a.Ort == b.Ort && a.PLZ == b.PLZ && a.Strasse == b.Strasse && a.Hausnummer == b.Hausnummer);
    }

    public override bool Equals(object obj) {
        if (obj is WohnortNOid)
            return Ort == obj.Ort && PLZ == obj.PLZ && Strasse == obj.Strasse && Hausnummer == obj.Hausnummer;
        return false;
    }
}

public class KundeOrt
{
    public int KID { get; set; }
    public WohnortNOid ort { get; set; }
}

public class KundenBestellungen
{
    public int KID { get; set; }
    public List<Tuple<string, string>> bestellungen { get; set; }
}