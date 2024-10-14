class Authenticator
{
    public const string SecretCode = "0000";
    public static bool Authenticate()
    {
        Console.Clear();
        Console.WriteLine("Welcome to GertSoft-Authenticator!");
        Console.WriteLine();
        Console.WriteLine("Please enter your authentication code:");

        string UserInput = Console.ReadLine();

        if (UserInput == SecretCode)
        {
            // Real application will start
            return true;
        }
        else
        {
            Console.WriteLine("Access Denied.");
            return false;
        }
    }
}
// SIMPLE START. I WILL MAKE THIS PRETTIER AND BETTER LATER <3