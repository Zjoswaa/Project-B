using System.Data.SQLite;

//Temporary database template
static class Database
{
    //Creates a new database
    public static SQLiteConnection CreateConnection()
    {
        //"..\" to put the file on the same level as .cs files for convenience
        string dbFile = @"URI=file:..\..\..\sample.db";
        return new SQLiteConnection(dbFile);
    }

    public static void CreateTableUsers()
    {
        using var conn = CreateConnection();
        conn.Open();

        //Creates table if it doesn't exist
        using var cmd = new SQLiteCommand(conn);
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, name TEXT, email TEXT, phonenumber TEXT, password TEXT)";
        cmd.ExecuteNonQuery();

        conn.Close();
    }
}