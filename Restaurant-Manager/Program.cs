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
            Database.CreateReviewsTable();

            Database.InsertLocationsTable("Amsterdam", "The Vanishing Vault", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Amsterdam", "Labyrinth of Shadows", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Amsterdam", "The Alchemist's Sanctum", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Amsterdam", "Secrets of the Forgotten Asylum", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");

            Database.InsertLocationsTable("Groningen", "Timekeeper’s Paradox", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Groningen", "Curse of the Crimson Manor", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Groningen", "The Phantom's Playhouse", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Groningen", "The Oracle’s Chamber", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");

            Database.InsertLocationsTable("Rotterdam", "The Asylum of Whisperers", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Rotterdam", "The Silent Hotel", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Rotterdam", "Midnight at the Observatory", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Rotterdam", "Depths of the Abyss", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");

            Database.InsertLocationsTable("Utrecht", "Echoes of the Enchanted Library", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Utrecht", "The Blackwell Experiment", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Utrecht", "The Silent Carnival", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Utrecht", "Portal of the Forgotten Realm", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");

            Database.InsertLocationsTable("Zwolle", "Whispering Woods Cottage", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Zwolle", "Crypt of the Forgotten Pharaoh", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Zwolle", "The Ship of Lost Souls", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");
            Database.InsertLocationsTable("Zwolle", "The Midnight Apothecary", "Enter 'Freddy's Comfort Food, ask for 'Secret Menu #2' and an employee will lead you to your designated starting point.");

            Database.InsertTimeslotsTable("12:00");
            Database.InsertTimeslotsTable("15:00");
            Database.InsertTimeslotsTable("18:00");
            Database.InsertTimeslotsTable("21:00");

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
