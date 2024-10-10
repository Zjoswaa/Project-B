class AccountPresentation
{
    public void LoginUI()
    {
        Console.Write("Enter your email address: ");
        string email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        bool loginStatus = User.Login(email, password);
        if (loginStatus)
        {
            //name TBA
            Console.WriteLine($"You are now logged in. Welcome back, [NAME]");
            return;
        }
        //Add differentiation between wrong email and wrong passwordd
    }

    public void RegisterUI()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Enter your email address: ");
        string email = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();
        Console.Write("Enter your phone number: ");
        string phonenumber = Console.ReadLine();

        User newUser = new(name, email, phonenumber, password);
        bool registerStatus = newUser.Register(name, email, phonenumber, password);
        if (registerStatus)
        {
            Console.WriteLine("Your account has been created.");
            return;
        }
        Console.WriteLine("A user with this email address already exists. Please login using this address or make an account with a different address.");
    }
}