using Spectre.Console;
using Restaurant_Manager.Presentation;

class Program
{
    public static void Main()
    {
        try
        {
            Database.ConnectionString = "database.db";
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            //Database.InsertLocationsTable("Placeholder_1");
            //Database.InsertLocationsTable("Placeholder_2");
            //Database.InsertLocationsTable("Placeholder_3");
            //Database.InsertLocationsTable("Placeholder_4");
            Database.CreateReservationsTable();
            Database.CreateAvailableSlots();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }
        while (!Authenticator.Authenticate()) { }
        ShowMainMenu();
    }

    public static void ShowMainMenu()
    {
        bool loggedIn = false;
        while (true)
        {
            // Display welcome message in a box
            AnsiConsole.Write(new Rule("[yellow] Welcome [/]"));

            // Display selection menu
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Please make a choice:[/]")
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

            UserMenu.ShowUserMenu(ref loggedIn);
        }
    }
}