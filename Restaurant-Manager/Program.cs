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
            //Database.InsertLocationsTable("Placeholder_1");
            //Database.InsertLocationsTable("Placeholder_2");
            //Database.InsertLocationsTable("Placeholder_3");
            //Database.InsertLocationsTable("Placeholder_4");
            Database.CreateReservationsTable();
            Database.CreateAvailableSlots();

            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(180);
            List<string> timeslots = new(){"12:00", "15:00", "18:00", "21:00"};
            List<AvailableSlot> slots =  AvailableSlot.CreateSlots(startDate, endDate, 4, timeslots);
            AvailableSlot.FillAvailableSlotsTable(slots);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }
        while (!Authenticator.Authenticate()) { }
        MainMenuPresentation.ShowMainMenu();
    }
}