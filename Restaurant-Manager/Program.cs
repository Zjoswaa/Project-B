class Program {
    public static void Main() {
        Database.Connect("database.db");
        Database.OpenConnection();
        Database.CreateUsersTable();
        Database.CreateDishesTable();
        Database.CreateLocationsTable();
        Database.CreateReservationsTable();

        //Database.InsertDishesTable("Pasta Bolognese", "10.95", false, false, true, false);

        //if (!Database.UsersTableContainsUser("johndoe")) {
        //    Database.InsertUsersTable("johndoe", "AAAA", "John", "Doe", "USER");
        //}
        //if (!Database.UsersTableContainsUser("janedoe")) {
        //    Database.InsertUsersTable("janedoe", "AAAA", "Jane", "Doe", "USER");
        //}
        //User? jane = Database.GetUserByUsername("janedoe");
        //Console.WriteLine($"User: {jane?.GetFullName()} - {jane?.Role}");
        Console.Read();
        RegisterPresentation.Present(true);
        Database.CloseConnection();
    }
}
