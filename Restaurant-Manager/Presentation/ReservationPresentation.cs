using System.Globalization;
using Spectre.Console;

public static class ReservationUI
{
    public static void Present()
    {
        ReservationManager resManager = new();

        Console.Clear();
        long userID = State.LoggedInUser.ID;

        long locID = SelectLocation(resManager);
        if (locID == -1) return;
        string locMessage = resManager.GetLocationDescription(locID);

        string timeslot = SelectTimeslot(resManager);
        if (timeslot == "NULL") return;

        string dateString = SelectDate(resManager);
        if (dateString == "NULL") return;
        DateTime date = resManager.ParseDate(dateString);

        int groupsize = SelectGroupSize();
        if (groupsize == -1) return;

        int table = resManager.GetTableCount(locID, timeslot, date);

        (bool success, string message) = resManager.CreateReservation(userID, locID, timeslot, date, groupsize, table);
        if (success)
        {
            string text = $"[green]Your reservation has been made.[/]\nYour Table Number: {table}\n\n{locMessage}\n\nPress any key to continue.";
            Panel panel = new(new Markup(text).Centered()); // Update the panel and the text in it with the updated buffer
            panel.Expand = true; // Set expand again
            Console.Clear();
            AnsiConsole.Write(panel);
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }

    private static long SelectLocation(ReservationManager resManager)
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

        return resManager.GetLocationIDByName(locationChoice);
    }

    private static string SelectTimeslot(ReservationManager resManager)
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

    private static string SelectDate(ReservationManager resManager)
    {

        DateTime date = DateTime.MinValue;
        string dateInput = "";
        while (true)
        {
            Console.WriteLine("Enter a date (DD-MM-YYYY) or leave empty to cancel reservation:");
            dateInput = Console.ReadLine();
            if (dateInput == "") return "NULL";

            try
            {
                date = resManager.ParseDate(dateInput);
            }

            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("The date you have entered is not in a valid format.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }

            break;
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

    private static int SelectGroupSize()
    {
        Console.CursorVisible = false;
        bool isSelected = false;
        int currentOption = 1;
        const int minGroupSize = 1;
        const int maxGroupSize = 6;

        DisplayGroupSizeSelectionPanel(currentOption, minGroupSize, maxGroupSize);

        while (!isSelected)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (currentOption != maxGroupSize && keyInfo.Key == ConsoleKey.UpArrow) currentOption += 1;

            if (currentOption != minGroupSize && keyInfo.Key == ConsoleKey.DownArrow) currentOption -= 1;

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                isSelected = true;
            }

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return -1;
            }

            // Replaces the first panel with one where the changes have been made
            DisplayGroupSizeSelectionPanel(currentOption, minGroupSize, maxGroupSize);
        }

        return currentOption;
    }

    private static void DisplayGroupSizeSelectionPanel(int currentOption, int minGroupSize, int maxGroupSize)
    {
        string arrowUp = (currentOption < maxGroupSize) ?  "         ^" : "";
        string arrowDown = (currentOption > minGroupSize) ? "          v " : "";
        string personOrPeople = (currentOption == minGroupSize) ? "person" : "people";

        string displayText = $"\n{arrowUp}\nReservation for {currentOption} {personOrPeople}\n{arrowDown}";
        Panel panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{displayText}\n\n\nPress ESC to exit.").Centered());
        panel.Expand = true;

        AnsiConsole.Clear();
        AnsiConsole.Write(panel);
    }
}