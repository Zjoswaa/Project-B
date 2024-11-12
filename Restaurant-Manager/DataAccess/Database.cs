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
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users(ID INTEGER PRIMARY KEY AUTOINCREMENT, Email TEXT NOT NULL, Password TEXT NOT NULL, FirstName TEXT, LastName TEXT, Role TEXT NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void CreateLocationsTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Locations(ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Message TEXT NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void CreateReservationsTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Reservations(ID INTEGER PRIMARY KEY AUTOINCREMENT, User INTEGER, Location INTEGER, Timeslot TEXT NOT NULL, DateTime DATETIME NOT NULL, GroupSize INTEGER NOT NULL, GroupTable INTEGER NOT NULL, FOREIGN KEY(User) REFERENCES Users(ID), FOREIGN KEY(Location) REFERENCES Locations(ID))";
        cmd.ExecuteNonQuery();
    }

    public static void CreateDishesTable() {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Dishes(ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Price TEXT NOT NULL, IsVegan INTEGER NOT NULL, IsVegetarian INTEGER NOT NULL, IsHalal INTEGER NOT NULL, IsGlutenFree INTEGER NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void CreateTimeslotsTable()
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText =
            "CREATE TABLE IF NOT EXISTS Timeslots(ID INTEGER PRIMARY KEY AUTOINCREMENT, Timeslot TEXT NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public static void SetUserPassword(string Email, string NewPassword) {
        using SQLiteConnection Connection = new SQLiteConnection($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "UPDATE Users SET Password = @NewPassword WHERE Email = @Email";
        cmd.Parameters.AddWithValue("@NewPassword", Encryptor.Encrypt(NewPassword));
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.ExecuteNonQuery();
    }

    public static void InsertDishesTable(string Name, string Price, bool IsVegan, bool IsVegetarian, bool IsHalal, bool IsGlutenFree)
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO Dishes(Name, Price, IsVegan, IsVegetarian, IsHalal, IsGlutenFree) VALUES(@Name, @Price, @IsVegan, @IsVegetarian, @IsHalal, @IsGlutenFree)";
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@IsVegan", IsVegan ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsVegetarian", IsVegetarian ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsHalal", IsHalal ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsGlutenFree", IsGlutenFree ? 1 : 0);
        cmd.ExecuteNonQuery();
    }

    public static void InsertTimeslotsTable(string timeslot)
    {
        using SQLiteConnection Connection = new SQLiteConnection($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = @"INSERT INTO Timeslots(Timeslot)
                            SELECT @Timeslot
                            WHERE NOT EXISTS (SELECT 1 FROM Timeslots WHERE Timeslot = @Timeslot)";
        cmd.Parameters.AddWithValue("@Timeslot", timeslot);
        cmd.ExecuteNonQuery();
    }

    public static bool DishesTableContainsDish(string Name)
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT COUNT(*) FROM Dishes WHERE Name = @Name";
        cmd.Parameters.AddWithValue("@Name", Name);
        Object result = cmd.ExecuteScalar();

        return (long)result > 0;
    }

    public static void UpdateDishesTable(long ID, string Name, double Price, bool IsVegan, bool IsVegetarian, bool IsHalal, bool IsGlutenFree)
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "UPDATE Dishes SET Name = @Name, Price = @Price, IsVegan = @IsVegan, IsVegetarian = @IsVegetarian, IsHalal = @IsHalal, IsGlutenFree = @IsGlutenFree WHERE ID = @ID";
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@IsVegan", IsVegan ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsVegetarian", IsVegetarian ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsHalal", IsHalal ? 1 : 0);
        cmd.Parameters.AddWithValue("@IsGlutenFree", IsGlutenFree ? 1 : 0);
        cmd.ExecuteNonQuery();
    }

    public static void DeleteDishesTable(string Name) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Dishes WHERE Name = @Name", Connection);
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.ExecuteNonQuery();
    }

    public static void InsertLocationsTable(string name, string message)
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = @"INSERT INTO Locations(Name, Message)
                            SELECT @Name, @Message
                            WHERE NOT EXISTS (SELECT 1 FROM Locations WHERE Name = @Name)";

        cmd.Parameters.AddWithValue("@Name", name);
        cmd.Parameters.AddWithValue("@Message", message);
        cmd.ExecuteNonQuery();
    }

    public static void InsertReservationsTable(long user_id, long loc_id, string timeslot, DateTime datetime, int groupsize, int table)
    {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO Reservations(User, Location, Timeslot, DateTime, GroupSize, GroupTable) VALUES (@User, @Location, @Timeslot, @DateTime, @GroupSize, @GroupTable)";

        cmd.Parameters.AddWithValue("@User", user_id);
        cmd.Parameters.AddWithValue("@Location", loc_id);
        cmd.Parameters.AddWithValue("@Timeslot", timeslot);
        cmd.Parameters.AddWithValue("@DateTime", datetime);
        cmd.Parameters.AddWithValue("@GroupSize", groupsize);
        cmd.Parameters.AddWithValue("@GroupTable", table);
        cmd.ExecuteNonQuery();
    }

    public static List<Location> GetAllLocations()
    {
        List<Location> locations = new();

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);

        cmd.CommandText = "SELECT * FROM Locations";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string message = reader.GetString(2);
            locations.Add(new Location(id, name, message));
        }

        return locations;
    }

    public static List<Reservation> GetAllReservations()
    {
        List<Reservation> reservations = new(){};

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);

        cmd.CommandText = "SELECT * FROM Reservations";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            long ID = reader.GetInt32(0);
            long userID = reader.GetInt32(1);
            long locId = reader.GetInt32(2);
            string timeslot = reader.GetString(3);
            DateTime date = reader.GetDateTime(4);
            int groupsize = reader.GetInt32(5);
            int table = reader.GetInt32(6);

            reservations.Add(new Reservation(ID, userID, locId, timeslot, date, groupsize, table));
        }

        return reservations;
    }

    public static List<Dish> GetAllDishes() {
        List<Dish> dishes = new List<Dish>();
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Dishes", Connection);
        using SQLiteDataReader reader = cmd.ExecuteReader();
        while (reader.Read()) {
            dishes.Add(new Dish(
                (long)reader["ID"],
                (string)reader["Name"],
                Convert.ToDouble(reader["Price"]),
                Convert.ToBoolean(reader["IsVegan"]),
                Convert.ToBoolean(reader["IsVegetarian"]),
                Convert.ToBoolean(reader["IsHalal"]),
                Convert.ToBoolean(reader["IsGlutenFree"])
                ));
        }
        return dishes;
    }

    public static List<Timeslot> GetAllTimeslots()
    {
        List<Timeslot> timeslots = new(){};

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);

        cmd.CommandText = "SELECT * FROM Timeslots";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            long id = reader.GetInt32(0);
            string timeslot = reader.GetString(1);

            timeslots.Add(new Timeslot(id, timeslot));
        }

        return timeslots;
    }

    public static void InsertUsersTable(string Email, string Password, string? FirstName, string? LastName, string Role)
    {
        if (Role != "USER" && Role != "ADMIN")
        {
            throw new InvalidDataException($"Role has to be \"USER\" or \"ADMIN\". Found \"{Role}\"");
        }

        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = "INSERT INTO Users(Email, Password, FirstName, LastName, Role) VALUES(@Email, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(FirstName) ? null : FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(LastName) ? null : LastName);
        cmd.Parameters.AddWithValue("@Role", Role);
        cmd.ExecuteNonQuery();
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
        cmd.CommandText = "INSERT INTO Users(Email, Password, FirstName, LastName, Role) VALUES(@Email, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@Email", User.Email);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(User.FirstName) ? null : User.FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(User.LastName) ? null : User.LastName);
        cmd.Parameters.AddWithValue("@Role", User.Role);
        cmd.ExecuteNonQuery();
    }

    // This forces an ID for the user, use only for debugging
    public static void InsertUsersTable(long ID, string Email, string Password, string? FirstName, string? LastName, string Role)
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
        cmd.CommandText = $"INSERT INTO Users(ID, Email, Password, FirstName, LastName, Role) VALUES(@ID, @Email, @Password, @FirstName, @LastName, @Role)";
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Password", Encryptor.Encrypt(Password));
        cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(FirstName) ? null : FirstName);
        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(LastName) ? null : LastName);
        cmd.Parameters.AddWithValue("@Role", Role);
        cmd.ExecuteNonQuery();
    }

    public static bool UsersTableContainsUser(string Email) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT COUNT(*) FROM Users WHERE Email = @Email";
        cmd.Parameters.AddWithValue("@Email", Email);
        Object result = cmd.ExecuteScalar();

        return (long)result > 0;
    }

    public static User? GetUserByEmail(string Email) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT * FROM Users WHERE Email = @Email LIMIT 1";
        cmd.Parameters.AddWithValue("@Email", Email);
        using SQLiteDataReader result = cmd.ExecuteReader();
        if (!result.HasRows)
        {
            return null;
        }
        result.Read();
        return new User((long)result["ID"], (string)result["Email"], result["FirstName"] == DBNull.Value ? null : (string)result["FirstName"], result["LastName"] == DBNull.Value ? null : (string)result["LastName"], (string)result["Role"]);
    }

    public static string? GetEncryptedPassword(string Email) {
        using SQLiteConnection Connection = new($"Data Source={ConnectionString}");
        Connection.Open();
        using SQLiteCommand cmd = new SQLiteCommand(Connection);
        cmd.CommandText = $"SELECT Password FROM Users WHERE Email = @Email LIMIT 1";
        cmd.Parameters.AddWithValue("@Email", Email);
        using SQLiteDataReader result = cmd.ExecuteReader();
        if (!result.HasRows)
        {
            return null;
        }
        result.Read();
        return (string)result["Password"];
    }
}
