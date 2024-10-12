class Program {
    public static void Main() {
        Database.Connect("database.db");
        Database.OpenConnection();
        Database.CreateUsersTable();
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
