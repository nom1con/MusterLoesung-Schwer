public record DBAdresse(int AdresseID, int OrtID, string Strasse, string Hausnummer);

public record DBBestellung(int KundenID, int Artikelnummer, int Menge);

public record DBKunde(int KundenID, string Vorname, string Nachname, string TelefonNR, DateTime Geburtsdatum, int AdresseID, string Benutzername, string Passwort);

public record DBOrt(int OrtID, string Ortsname, string PLZ);

public record DBWare(int Artikelnummer, string Bezeichnung, double Preis, int Lagerbestand);

