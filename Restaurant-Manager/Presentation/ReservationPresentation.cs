using System.Globalization;
using Spectre.Console;

public static class ReservationPresentation
{
    public static void Present()
    {
        ReservationManager resManager = new();

        Console.Clear();
        long userID = State.LoggedInUser.ID;

        long locID = UserLocation(resManager);
        if (locID == -1) return;

        string timeslot = UserTimeslot(resManager);
        if (timeslot == "NULL") return;

        string dateString = UserDate(resManager);
        if (dateString == "NULL") return;
        DateTime date = resManager.ParseDate(dateString);

        int groupsize = UserGroupSize();
        if (groupsize == -1) return;

        resManager.CreateReservation(userID, locID, timeslot, date, groupsize);
        string text = "[green]Your reservation has been made.[/]\n\nPress any key to continue.";
        Panel panel = new(new Markup(text).Centered()); // Update the panel and the text in it with the updated buffer
        panel.Expand = true; // Set expand again
        Console.Clear();
        AnsiConsole.Write(panel);
        Console.ReadKey();
    }

    static long UserLocation(ReservationManager resManager)
    {
        Console.CursorVisible = false;

        // User selects a Location, of which the ID gets stored
        var locationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a location:[/]")
            .AddChoices(resManager.LocationNamesToList()));

        if (locationChoice == "Exit Reservation")
        {
            return -1;
        }

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
        
        if (timeslotChoice == "Exit Reservation")
        {
            return "NULL";
        }

        return timeslotChoice;
    }

    static string UserDate(ReservationManager resManager) //Add spectre calendar with unavailable dates maybe
    {

        DateTime date = DateTime.MinValue;
        string dateInput = "";
        while (true)
        {
            try
            {
                Console.WriteLine("Enter a date (DD-MM-YYYY) or type 'Exit' to cancel reservation:");
                dateInput = Console.ReadLine();

                if (dateInput.ToLower() == "exit")
                {
                    return "NULL";
                }
                date = resManager.ParseDate(dateInput);
                break;
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.WriteLine("The date you have entered is not in a valid format.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        (bool success, string message) = resManager.VerifyDate(date);

        if (!success)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return "NULL";
        }

        return dateInput;
    }

    static int UserGroupSize()
    {
        Console.CursorVisible = false;
        bool isSelected = false;
        int currentOption = 1;
        int groupsize = 0;

        // Creates a Spectre panel in which the user can pick the amount of people for a reservation
        string Buffer = $"\n          ^ \nReservation for {currentOption} person\n";
        Panel Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n\n\nPress ESC to exit.").Centered()); // Define the Panel and Text within it
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

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return -1;
            }

                Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n\n\nPress ESC to exit.").Centered()); // Update the panel and the text in it with the updated buffer
                Panel.Expand = true; // Set expand again
                AnsiConsole.Clear(); // Clear the previous print of the panel
                AnsiConsole.Write(Panel); // Re-render the panel with the updated text
        }

        return groupsize;
    }
}