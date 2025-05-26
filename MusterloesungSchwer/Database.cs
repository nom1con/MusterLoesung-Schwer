using MySqlConnector;

public static class Database
{
    private static MySqlConnectionStringBuilder conStr = new()
    {
        Server = "localhost",
        Database = "webshop",
        UserID = "root",
        Password = ""
    };

    public static void Store(IEnumerable<DBKunde> kunden)
    {
        using var con = new MySqlConnection(conStr.ToString());
        con.Open();
        using var command = new MySqlCommand("INSERT INTO kunde VALUES (@KundenID, @Vorname, @Nachname, @Telephonnummer, @Geburtsdatum, @AdresseID, @Benutzername, @Passwort)", con);
        foreach (var k in kunden)
        {
            command.Parameters.AddWithValue("@KundenID", k.KundenID);
            command.Parameters.AddWithValue("@Vorname", k.Vorname);
            command.Parameters.AddWithValue("@Nachname", k.Nachname);
            command.Parameters.AddWithValue("@TelephonNummer", k.TelefonNR);
            command.Parameters.AddWithValue("@Geburtsdatum", k.Geburtsdatum);
            command.Parameters.AddWithValue("@AdresseID", k.AdresseID);
            command.Parameters.AddWithValue("@Benutzername", k.Benutzername);
            command.Parameters.AddWithValue("@Passwort", k.Passwort);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


    }

    public static void Store(IEnumerable<DBAdresse> adresse)
    {
        using var con = new MySqlConnection(conStr.ToString());
        con.Open();
        using var command = new MySqlCommand("INSERT INTO adresse VALUES (@AdresseID, @OrtID, @Strasse, @Hausnummer)", con);
        foreach (var a in adresse)
        {
            command.Parameters.AddWithValue("@AdresseID", a.AdresseID);
            command.Parameters.AddWithValue("@OrtID", a.OrtID);
            command.Parameters.AddWithValue("@Strasse", a.Strasse);
            command.Parameters.AddWithValue("@Hausnummer", a.Hausnummer);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


    }

    public static void Store(IEnumerable<DBBestellung> bestellung)
    {
        using var con = new MySqlConnection(conStr.ToString());
        con.Open();
        using var command = new MySqlCommand("INSERT INTO bestellung VALUES (null, @KundenID, @Artikelnummer, @Menge)", con);
        foreach (var b in bestellung)
        {
            command.Parameters.AddWithValue("@KundenID", b.KundenID);
            command.Parameters.AddWithValue("@Artikelnummer", b.Artikelnummer);
            command.Parameters.AddWithValue("@Menge", b.Menge);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


    }


    public static void Store(IEnumerable<DBOrt> ort)
    {
        using var con = new MySqlConnection(conStr.ToString());
        con.Open();
        using var command = new MySqlCommand("INSERT INTO ort VALUES (@OrtID, @Ortsname, @PLZ)", con);
        foreach (var o in ort)
        {
            command.Parameters.AddWithValue("@OrtID", o.OrtID);
            command.Parameters.AddWithValue("@Ortsname", o.Ortsname);
            command.Parameters.AddWithValue("@PLZ", o.PLZ);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


    }


    public static void Store(IEnumerable<DBWare> ware)
    {
        using var con = new MySqlConnection(conStr.ToString());
        con.Open();
        using var command = new MySqlCommand("INSERT INTO ware VALUES (@Artikelnummer, @Bezeichnung, @Preis, @Lagerbestand)", con);
        foreach (var w in ware)
        {
            command.Parameters.AddWithValue("@Artikelnummer", w.Artikelnummer);
            command.Parameters.AddWithValue("@Bezeichnung", w.Bezeichnung);
            command.Parameters.AddWithValue("@Preis", w.Preis);
            command.Parameters.AddWithValue("@Lagerbestand", w.Lagerbestand);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


    }
}