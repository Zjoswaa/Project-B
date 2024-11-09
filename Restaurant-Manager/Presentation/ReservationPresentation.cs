using System.Globalization;
using Spectre.Console;

public static class ReservationPresentation
{
    public static void Present()
    {
        ReservationManager resManager = new();

        long userID = State.LoggedInUser.ID;
        long locID = UserLocation(resManager);
        string timeslot = UserTimeslot(resManager);
        DateTime date = UserDate();
        int groupsize = UserGroupSize();

        resManager.CreateReservation(userID, locID, timeslot, date, groupsize);
    }

    static long UserLocation(ReservationManager resManager)
    {
        Console.CursorVisible = false;

        // User selects a Location, of which the ID gets stored
        var locationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a location:[/]")
            .AddChoices(resManager.LocationNamesToList()));

        return resManager.GetSelectedLocationID(locationChoice);
    }

    static string UserTimeslot(ReservationManager resManager)
    {
        Console.CursorVisible = false;

        // Timeslot object gets converted into a list of times in string format, user can pick one
        var timeslotChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a timeslot:[/]")
            .AddChoices(resManager.TimeslotsToList()));
        
        return timeslotChoice;
    }

    //TO BE CHANGED
    static DateTime UserDate()
    {
        Console.WriteLine("Enter a date (DD-MM-YYYY):");
        string dateInput = Console.ReadLine();
        DateTime date = DateTime.ParseExact(dateInput, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        return date;
    }

    static int UserGroupSize()
    {
        Console.CursorVisible = false;
        bool isSelected = false;
        int currentOption = 1;
        int groupsize = 0;

        // Creates a Spectre panel in which the user can pick the amount of people for a reservation
        string Buffer = $"\n          ^ \nReservation for {currentOption} person\n";
        Panel Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n").Centered()); // Define the Panel and Text within it
        Panel.Expand = true; // Panel takes full width

        AnsiConsole.Clear();
        AnsiConsole.Write(Panel); // Render it

        while (!isSelected)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            Buffer = (currentOption != 6) ? $"\n          ^ \nReservation for {currentOption} person\n" : $"\n         \nReservation for {currentOption} people\n         v";

            if (currentOption != 6 && keyInfo.Key == ConsoleKey.UpArrow)
            {
                currentOption += 1;
                Buffer = (currentOption != 6) ? $"\n          ^ \nReservation for {currentOption} people\n         v" : $"\n         \nReservation for {currentOption} people\n         v";
            }

            if (currentOption != 1 && keyInfo.Key == ConsoleKey.DownArrow)
            {
                currentOption -= 1;
                Buffer = (currentOption != 1) ? $"\n          ^ \nReservation for {currentOption} people\n         v" : $"\n          ^ \nReservation for {currentOption} person\n";
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                groupsize = currentOption;
                isSelected = true;
            }

            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                break;
            }

                Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n").Centered()); // Update the panel and the text in it with the updated buffer
                Panel.Expand = true; // Set expand again
                AnsiConsole.Clear(); // Clear the previous print of the panel
                AnsiConsole.Write(Panel); // Re-render the panel with the updated text
        }

        return groupsize;
    }
}