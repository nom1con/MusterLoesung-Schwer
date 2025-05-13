using MySqlConnector;

public static class Database
{
    public static void StoreKunden(IEnumerable<DBKunde> kunden)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var command = new MySqlCommand("INSERT INTO kunden VALUES (@KID, @Vorname, @Nachname, @E_Mail, @Passwort, @OID)", con);
        foreach (var k in kunden)
        {
            command.Parameters.AddWithValue("@KID", k.KID);
            command.Parameters.AddWithValue("@Vorname", k.Vorname);
            command.Parameters.AddWithValue("@Nachname", k.Nachname);
            command.Parameters.AddWithValue("@E_Mail", k.E_Mail);
            command.Parameters.AddWithValue("@Passwort", k.Passwort);
            command.Parameters.AddWithValue("@OID", k.OID);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
    }

    public static void StoreOrte(IEnumerable<DBWohnort> orte)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var command = new MySqlCommand("INSERT INTO wohnort VALUES (@OID, @Ort, @PLZ, @Strasse, @Hausnummer)", con);
        foreach (var o in orte)
        {
            command.Parameters.AddWithValue("@OID", o.OID);
            command.Parameters.AddWithValue("@Ort", o.Ort);
            command.Parameters.AddWithValue("@PLZ", o.PLZ);
            command.Parameters.AddWithValue("@Strasse", o.Strasse);
            command.Parameters.AddWithValue("@Hausnummer", o.Hausnummer);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
    }

    public static void StoreProdukte(IEnumerable<DBProdukt> produkte)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var command = new MySqlCommand("INSERT INTO produkte VALUES (@PID, @Name, @Verkaufspreis, @Bestand, @Beschreibung, @Artikelnummer)", con);
        foreach (var p in produkte)
        {
            command.Parameters.AddWithValue("@PID", p.PID);
            command.Parameters.AddWithValue("@Name", p.Name);
            command.Parameters.AddWithValue("@Verkaufspreis", p.Verkaufspreis);
            command.Parameters.AddWithValue("@Bestand", p.Bestand);
            command.Parameters.AddWithValue("@Beschreibung", p.Beschreibung);
            command.Parameters.AddWithValue("@Artikelnummer", p.Artikelnummer);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
    }

    public static void StoreBestellungen(IEnumerable<DBBestellungen> bestellungen)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var command = new MySqlCommand("INSERT INTO bestellungen VALUES (@PID, @KID, @Menge)", con);
        foreach (var b in bestellungen)
        {
            command.Parameters.AddWithValue("@PID", b.PID);
            command.Parameters.AddWithValue("@KID", b.KID);
            command.Parameters.AddWithValue("@Menge", b.Menge);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
    }
}