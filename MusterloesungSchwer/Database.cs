using System.Reflection.Metadata.Ecma335;
using MySqlConnector;

public static class Database
{
    public static void StoreKunden(IEnumerable<DBKunde> kunden)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var batch = new MySqlBatch(con);
        foreach (var k in kunden)
        {
            batch.BatchCommands.Add(new MySqlBatchCommand("INSERT INTO kunde VALUES (@KID, @Vorname, @Nachname, @E_Mail, @Passwort, @OID)")
            {
                Parameters =
                {
                    new MySqlParameter("@KID", k.KID),
                    new MySqlParameter("@Vorname", k.Vorname),
                    new MySqlParameter("@Nachname", k.Nachname),
                    new MySqlParameter("@E_Mail", k.E_Mail),
                    new MySqlParameter("@Passwort", k.Passwort),
                    new MySqlParameter("@OID", k.OID)
                }
            });
        }
        batch.ExecuteNonQuery();
    }

    public static void StoreOrte(IEnumerable<DBWohnort> orte)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var batch = new MySqlBatch(con);
        foreach (var o in orte)
        {
            batch.BatchCommands.Add(new MySqlBatchCommand("INSERT INTO wohnort VALUES (@OID, @Ort, @PLZ, @Strasse, @Hausnummer)")
            {
                Parameters = 
                {
                    new MySqlParameter("@OID", o.OID),
                    new MySqlParameter("@Ort", o.Ort),
                    new MySqlParameter("@PLZ", o.PLZ),
                    new MySqlParameter("@Strasse", o.Strasse),
                    new MySqlParameter("@Hausnummer", o.Hausnummer)
                }
            });
        }
        batch.ExecuteNonQuery();
    }

    public static void StoreProdukte(IEnumerable<DBProdukt> produkte)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var batch = new MySqlBatch(con);
        foreach (var p in produkte)
        {
            batch.BatchCommands.Add(new MySqlBatchCommand("INSERT INTO produkte VALUES (@PID, @Name, @Verkaufspreis, @Bestand, @Beschreibung, @Artikelnummer)")
            {
                Parameters =
                {
                    new MySqlParameter("@PID", p.PID),
                    new MySqlParameter("@Name", p.Name),
                    new MySqlParameter("@Verkaufspreis", p.Verkaufspreis),
                    new MySqlParameter("@Bestand", p.Bestand),
                    new MySqlParameter("@Beschreibung", p.Beschreibung),
                    new MySqlParameter("@Artikelnummer", p.Artikelnummer)
                }
            });
        }
        batch.ExecuteNonQuery();
    }

    public static void StoreBestellungen(IEnumerable<DBBestellungen> bestellungen)
    {
        using var con = new MySqlConnection("Server=127.0.0.1; Database=MusterloesungSchwer; UID=root");
        con.Open();
        using var batch = new MySqlBatch(con);
        using var command = new MySqlCommand("INSERT INTO bestellungen VALUES (@PID, @KID, @Menge)", con);
        foreach (var b in bestellungen)
        {
            batch.BatchCommands.Add(new MySqlBatchCommand("INSERT INTO produkte VALUES (@PID, @Name, @Verkaufspreis, @Bestand, @Beschreibung, @Artikelnummer)")
            {
                Parameters = 
                {
                    new MySqlParameter("@PID", b.PID),
                    new MySqlParameter("@KID", b.KID),
                    new MySqlParameter("@Menge", b.Menge)
                }
            });
        }
        batch.ExecuteNonQuery();
    }
}