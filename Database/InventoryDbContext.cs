using System.IO;
using Microsoft.Data.Sqlite;
using GooseInventorySystem.Models;

namespace GooseInventorySystem.Database;

public class InventoryDbContext
{
    private readonly string _connectionString;
    private static readonly string DefaultDbPath = Path.Combine(
        AppContext.BaseDirectory,
        "inventory.db");

    public InventoryDbContext(string? dbPath = null)
    {
        var path = dbPath ?? DefaultDbPath;
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);
        _connectionString = new SqliteConnectionStringBuilder { DataSource = path }.ToString();
        EnsureTable();
    }

    private void EnsureTable()
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE IF NOT EXISTS Items (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Barcode TEXT NOT NULL UNIQUE,
                Name TEXT NOT NULL,
                Company TEXT,
                Location TEXT,
                Quantity INTEGER NOT NULL DEFAULT 0
            );
            CREATE INDEX IF NOT EXISTS IX_Items_Barcode ON Items(Barcode);
            """;
        cmd.ExecuteNonQuery();
        EnsureCompanyColumn(conn);
    }

    private void EnsureCompanyColumn(SqliteConnection conn)
    {
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "ALTER TABLE Items ADD COLUMN Company TEXT";
            cmd.ExecuteNonQuery();
        }
        catch { /* column already exists */ }
    }

    public List<Item> GetAllItems()
    {
        var list = new List<Item>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Barcode, Name, Company, Location, Quantity FROM Items ORDER BY Name";
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(ReadItem(r));
        return list;
    }

    public Item? GetByBarcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode)) return null;
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Barcode, Name, Company, Location, Quantity FROM Items WHERE Barcode = @barcode";
        cmd.Parameters.AddWithValue("@barcode", barcode.Trim());
        using var r = cmd.ExecuteReader();
        return r.Read() ? ReadItem(r) : null;
    }

    public List<string> GetDistinctLocations()
    {
        var list = new List<string>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT DISTINCT Location FROM Items WHERE Location IS NOT NULL AND TRIM(Location) != '' ORDER BY Location";
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var loc = r.GetString(0);
            if (!string.IsNullOrWhiteSpace(loc))
                list.Add(loc);
        }
        return list;
    }

    public List<string> GetDistinctItemNames()
    {
        var list = new List<string>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT DISTINCT Name FROM Items WHERE Name IS NOT NULL AND TRIM(Name) != '' ORDER BY Name";
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var n = r.GetString(0);
            if (!string.IsNullOrWhiteSpace(n))
                list.Add(n);
        }
        return list;
    }

    public List<string> GetDistinctCompanies()
    {
        var list = new List<string>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT DISTINCT Company FROM Items WHERE Company IS NOT NULL AND TRIM(Company) != '' ORDER BY Company";
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var c = r.GetString(0);
            if (!string.IsNullOrWhiteSpace(c))
                list.Add(c);
        }
        return list;
    }

    public void Insert(Item item)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Items (Barcode, Name, Company, Location, Quantity) VALUES (@barcode, @name, @company, @location, @quantity)";
        cmd.Parameters.AddWithValue("@barcode", item.Barcode.Trim());
        cmd.Parameters.AddWithValue("@name", item.Name.Trim());
        cmd.Parameters.AddWithValue("@company", (object?)item.Company ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@location", (object?)item.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@quantity", item.Quantity);
        cmd.ExecuteNonQuery();
    }

    public void Update(Item item)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Items SET Barcode = @barcode, Name = @name, Company = @company, Location = @location, Quantity = @quantity WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", item.Id);
        cmd.Parameters.AddWithValue("@barcode", item.Barcode.Trim());
        cmd.Parameters.AddWithValue("@name", item.Name.Trim());
        cmd.Parameters.AddWithValue("@company", (object?)item.Company ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@location", (object?)item.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@quantity", item.Quantity);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Items WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void IncrementQuantity(int id, int delta)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Items SET Quantity = MAX(0, Quantity + @delta) WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@delta", delta);
        cmd.ExecuteNonQuery();
    }

    private static Item ReadItem(SqliteDataReader r)
    {
        return new Item
        {
            Id = r.GetInt32(0),
            Barcode = r.GetString(1),
            Name = r.GetString(2),
            Company = r.FieldCount > 4 && !r.IsDBNull(3) ? r.GetString(3) : null,
            Location = r.FieldCount > 4 ? (r.IsDBNull(4) ? null : r.GetString(4)) : (r.IsDBNull(3) ? null : r.GetString(3)),
            Quantity = r.GetInt32(r.FieldCount > 4 ? 5 : 4)
        };
    }
}
