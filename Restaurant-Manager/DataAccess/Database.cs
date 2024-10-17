using System.Data.SQLite;

public static class Database {
    public static string ConnectionString { get; set; } = "database.db";

    public static long GetUsersTableSize() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "SELECT COUNT(*) FROM Users";
        Object result = cmd.ExecuteScalar();

        return (long)result;
    }

    public static void CreateUsersTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users(ID INTEGER PRIMARY KEY, Username TEXT NOT NULL, Password TEXT NOT NULL, FirstName TEXT, LastName TEXT, Role TEXT NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void CreateLocationsTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Locations(ID INTEGER PRIMARY KEY, Name TEXT NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void CreateReservationsTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Reservations(ID INTEGER PRIMARY KEY, User INTEGER, Location INTEGER, DateTime DATETIME NOT NULL, GroupSize INTEGER NOT NULL, FOREIGN KEY(User) REFERENCES Users(ID), FOREIGN KEY(Location) REFERENCES Locations(ID))";
        cmd.ExecuteNonQuery();
    }

    public static void CreateDishesTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Dishes(ID INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price TEXT NOT NULL, IsVegan INTEGER NOT NULL, IsVegetarian INTEGER NOT NULL, IsHalal INTEGER NOT NULL, IsGlutenFree INTEGER NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void InsertDishTable(string Name, string Price, bool IsVegan, bool IsVegetarian, bool IsHalal, bool IsGlutenFree)
    {
        using SQLiteConnection Connection = new SQLiteConnection($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO dishes(Name, Price, IsVegan, IsVegetarian, IsHalal, IsGlutenFree)" +
                           "VALUES(@Name, @Price, @IsVegan, @IsVegetarian, @IsHalal, @IsGlutenFree)";
        cmd.Parameters.AddWithValue("@ID", 0);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@IsVegan", IsVegan ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("@IsVegetarian", IsVegetarian ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("@IsHalal", IsHalal ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("IsGlutenFree", IsGlutenFree ? "TRUE" : "FALSE");
        cmd.ExecuteNonQuery();
    }

    public static void UpdateDishTable()
    {
        // Method to update an existing dish in db
    }

    public static void DeleteDishTable()
    {
        // Method to delete a dish from db
    }

    public static void InsertUsersTable(string Username, string Password, string? FirstName, string? LastName, string Role)
    {
        if (Role != "USER" && Role != "ADMIN")
        {
            throw new InvalidDataException($"Role has to be \"USER\" or \"ADMIN\". Found \"{Role}\"");
        }

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO users(ID, Username, Password, FirstName, LastName, Role) VALUES(@ID, @Username, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@ID", User.NextID);
        cmd.Parameters.AddWithValue("@Username", Username);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(FirstName) ? null : FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(LastName) ? null : LastName);
        cmd.Parameters.AddWithValue("@Role", Role);
        cmd.ExecuteNonQuery();

        User.NextID++; // Increase ID of next user
    }

    public static void InsertUsersTable(User User, string Password)
    {
        if (User.Role != "USER" && User.Role != "ADMIN")
        {
            throw new InvalidDataException($"Role has to be \"USER\" or \"ADMIN\". Found \"{User.Role}\"");
        }
        if (User.ID <= 0)
        {
            throw new InvalidDataException($"ID has to be a positive non-zero long. Found \"{User.ID}\"");
        }

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO users(ID, Username, Password, FirstName, LastName, Role) VALUES(@ID, @Username, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@ID", User.NextID);
        cmd.Parameters.AddWithValue("@Username", User.Username);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(User.FirstName) ? null : User.FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(User.LastName) ? null : User.LastName);
        cmd.Parameters.AddWithValue("@Role", User.Role);
        cmd.ExecuteNonQuery();

        User.NextID++; // Increase ID of next user
    }

    // This forces an ID for the user, use only for debugging
    public static void InsertUsersTable(long ID, string Username, string Password, string? FirstName, string? LastName, string Role)
    {
        if (Role != "USER" && Role != "ADMIN")
        {
            throw new InvalidDataException($"Role has to be \"USER\" or \"ADMIN\". Found \"{Role}\"");
        }
        if (ID <= 0)
        {
            throw new InvalidDataException($"ID has to be a positive non-zero long. Found \"{ID}\"");
        }

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"INSERT INTO Users(ID, Username, Password, FirstName, LastName, Role) VALUES(@ID, @Username, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@Username", Username);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(FirstName) ? null : FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(LastName) ? null : LastName);
        cmd.Parameters.AddWithValue("@Role", Role);
        cmd.ExecuteNonQuery();
    }

    public static void InsertDishesTable(string Name, string Price, bool IsVegan, bool IsVegetarian, bool IsHalal, bool IsGlutenFree) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"INSERT INTO Dishes(ID, Name, Price, IsVegan, IsVegetarian, IsHalal, IsGlutenFree) VALUES(@ID, @Name, @Price, @IsVegan, @IsVegetarian, @IsHalal, @IsGlutenFree)";
        cmd.Parameters.AddWithValue("@ID", 0);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@IsVegan", IsVegan ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("@IsVegetarian", IsVegetarian ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("@IsHalal", IsHalal ? "TRUE" : "FALSE");
        cmd.Parameters.AddWithValue("@IsGlutenFree", IsGlutenFree ? "TRUE" : "FALSE");
        cmd.ExecuteNonQuery();
    }

    public static bool UsersTableContainsUser(string Username) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT COUNT(*) FROM Users WHERE Username = @Username";
        cmd.Parameters.AddWithValue("@Username", Username);
        Object result = cmd.ExecuteScalar();

        return (long)result > 0;
    }

    public static User? GetUserByUsername(string Username) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT * FROM Users WHERE Username = @Username LIMIT 1";
        cmd.Parameters.AddWithValue("@Username", Username);
        SQLiteDataReader result = cmd.ExecuteReader();
        if (!result.HasRows)
        {
            return null;
        }
        result.Read();
        return new User((long)result["ID"], (string)result["Username"], result["FirstName"] == DBNull.Value ? null : (string)result["FirstName"], result["LastName"] == DBNull.Value ? null : (string)result["LastName"], (string)result["Role"]);
    }

    public static string? GetEncryptedPassword(string Username) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT Password FROM Users WHERE Username = @Username LIMIT 1";
        cmd.Parameters.AddWithValue("@Username", Username);
        SQLiteDataReader result = cmd.ExecuteReader();
        if (!result.HasRows)
        {
            return null;
        }
        result.Read();
        return (string)result["Password"];
    }
}