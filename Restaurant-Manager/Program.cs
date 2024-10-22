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
    
// In future move to different location
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(180);
            List<string> timeslots = new(){"12:00", "15:00", "18:00", "21:00"};
            
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }

        while (!Authenticator.Authenticate()) { }
        
        // Call the SendEmail method
        // EmailService.SendReservationEmail("Tom van Genderen", "Rotterdam", "23/10/2024", "21:00", 4, "1092072@hr.nl");

        MainMenuPresentation.ShowMainMenu();
    }
}
