using Spectre.Console;

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
            Database.CreateReservationsTable();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }

        while (!Authenticator.Authenticate()) { }
        
        // Call the SendEmail method
        EmailService.SendEmail("1092072@hr.nl");

        MainMenuPresentation.ShowMainMenu();
    }
}