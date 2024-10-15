using Spectre.Console;

class Program
{
    public static void Main()
    {
        try
        {
            Database.Connect("database.db");
            Database.OpenConnection();
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }

        ShowMainMenu();
    }

    public static void ShowMainMenu()
    {
        bool loggedIn = false;
        while (true)
        {
            // Display welcome message in a box
            AnsiConsole.Write(
                new Rule("[yellow]Welcome[/]")
            );

            // Display selection menu
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    .AddChoices(new[] { "Login", "Register", "Exit" }));

            // Handle user selection
            switch (selection)
            {
                case "Login":
                    LoginPresentation.Present();
                    loggedIn = true;
                    break;
                case "Register":
                    RegisterPresentation.Present(true);
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }

            while (loggedIn)
            {
                Console.Clear();

                AnsiConsole.Write(
                    new Rule("[yellow]Reservation Menu[/]")
                );

                var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("")
                .AddChoices(new[] { "Make a Reservation", "View Menu", "View Reservations", "Edit Reservation", "Remove Reservation", "Logout", });

                var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

                switch (userSelection)
                {
                    case "Make a Reservation":
                        Console.WriteLine("TBA...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "View Menu":
                        Console.WriteLine("TBA...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "View Reservations":
                        Console.WriteLine("TBA...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "Edit Reservation":
                        Console.WriteLine("TBA...");
                        Thread.Sleep(1000);
                        break;
                    case "Remove Reservation":
                        Console.WriteLine("TBA...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case "Logout":
                        loggedIn = false;
                        Console.WriteLine("Logging out...");
                        //If CurrentUser object exists, reset it
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
