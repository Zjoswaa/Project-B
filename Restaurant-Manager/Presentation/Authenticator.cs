//using Spectre.Console;

//class Authenticator
//{
//    public const string SecretCode = "0000";
//    public static bool Authenticate()
//    {
//        Console.Clear();
//        Console.WriteLine("Welcome to GertSoft-Authenticator!");
//        Console.WriteLine();
//        Console.WriteLine("Please enter your authentication code:");

//        string UserInput = Console.ReadLine();

//        if (UserInput == SecretCode)
//        {
//            // Real application will start
//            return true;
//        }
//        else
//        {
//            Console.WriteLine("Access Denied.");
//            return false;
//        }
//    }
//}
//// SIMPLE START. I WILL MAKE THIS PRETTIER AND BETTER LATER <3

using Spectre.Console;

class Authenticator
{
    public static void Authenticate()
    {
        string Buffer = ""; // Define an empty string as buffer
        Panel Panel = new(new Text($"Please enter your authentication code:\n{Buffer}\n").Centered()); // Define the Panel and Text within it
        Panel.Header = new PanelHeader("[blue] Welcome to GertSoft Authenticator [/]").Centered(); // Set the panel header
        Panel.Expand = true; // Panel takes full width
        AnsiConsole.Write(Panel); // Render it

        while (true)
        { // Keep going
            ConsoleKeyInfo Input = Console.ReadKey(); // Read the pressed key
            if (Input.Key == ConsoleKey.Enter)
            { // If key was Enter, break the loop
                break;
            }
            Buffer += Input.KeyChar; // Append the character of pressed key to the buffer
            Panel = new(new Text($"Please enter your authentication code:\n{Buffer}\n").Centered()); // Update the panel and the text in it with the updated buffer
            Panel.Header = new PanelHeader("[blue] Welcome to GertSoft Authenticator [/]").Centered(); // Set the header again
            Panel.Expand = true; // Set expand again
            AnsiConsole.Clear(); // Clear the previous print of the panel
            AnsiConsole.Write(Panel); // Re-render the panel with the updated text
        }
    }
}