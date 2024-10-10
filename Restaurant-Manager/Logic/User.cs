class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public User(string name, string email, string phonenumber, string password)
    {
        Name = name;
        Email = email;
        PhoneNumber = phonenumber;
        Password = password;
    }

    public bool Register(string name, string email, string phonenumber, string password)
    {
        AccountManager manager = new();
        if (!manager.ExistingUserVerifier(email))
        {
            manager.AddUser(name, email, phonenumber, password);
            return true;
        }
        return false;
    }

    public static bool Login(string email, string password)
    {
        AccountManager manager = new();
        if (manager.LoginVerifier(email, password))
        {
            return true;
        }
        return false;
    }
}
