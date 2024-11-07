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
            Database.CreateTimeslotsTable();

            // In future move to different location
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(180);
            
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }

        while (!Authenticator.Authenticate()) { }
        
        //Call the SendEmail method
        //EmailService.SendReservationEmail("Joshua van der Jagt", "Rotterdam", "24/10/2024", "21:00", 4, "1092067@hr.nl");

        MainMenuPresentation.ShowMainMenu();
    }
}
