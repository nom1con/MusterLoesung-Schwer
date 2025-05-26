public record OrtNoID(string Ortname, string PLZ);

public record AdresseOrt(int AdresseID, OrtNoID Ort);

public record AdresseNoID(string Strasse, string Hausnummer, OrtNoID Ort);

public record KundeAdresse(int KundenID, AdresseNoID Adresse);

public record KundenBestellungen(int KundenID, List<Tuple<string, int>> Bestellungen);