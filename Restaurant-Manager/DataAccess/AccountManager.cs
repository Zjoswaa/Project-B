using System.Data.SQLite;

class AccountManager
{
    //Used for verifying if a user already exists when trying to register
    public bool ExistingUserVerifier(string email)
    {
        using var conn = Database.CreateConnection();
        conn.Open();
        using var cmd = new SQLiteCommand(conn);
        cmd.CommandText = "SELECT COUNT(*) FROM users WHERE email = @email";
        cmd.Parameters.AddWithValue("@email", email);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    //Adds a new user to the database
    public void AddUser(string name, string email, string password, string phonenumber)
    {
        using var conn = Database.CreateConnection();
        conn.Open();

        using var cmd = new SQLiteCommand(conn);
        cmd.CommandText = "INSERT INTO users(name, email, phonenumber, password) VALUES(@name, @email, @phonenumber, @password)";
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@phonenumber", phonenumber);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    //note to self: Change this to EmailVerifier, and make a new PasswordVerifier
    public bool LoginVerifier(string email, string password)
    {
        using var conn = Database.CreateConnection();
        conn.Open();
        using var cmd = new SQLiteCommand(conn);
        cmd.CommandText = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);

        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count == 1;
    }
}