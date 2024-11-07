using Spectre.Console;


// TO FIX: weird string formatting before giving input for group size
// TO FIX: garbage if statements
public static class ReservationPresentation
{
    public static void Present()
    {
        ReservationManager resManager = new();

        long userID = State.LoggedInUser.ID;

        // User selects a Location, of which the ID gets stored
        var locationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a location:[/]")
            .AddChoices(resManager.LocationNamesToList()));

        long locID = resManager.GetSelectedLocationID(locationChoice);

        // Timeslot object gets converted into a list of times in string format, user can pick one
        var timeslotChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a timeslot:[/]")
            .AddChoices(resManager.TimeslotsToList()));
        string timeslot = timeslotChoice;

        // Creates a Spectre panel in which the user can pick the amount of people for a reservation
        Console.CursorVisible = false;
        string Buffer = "Reservation for ^\n1\nv people"; // Define an empty string as buffer
        Panel Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n").Centered()); // Define the Panel and Text within it
        Panel.Expand = true; // Panel takes full width

        AnsiConsole.Clear();
        AnsiConsole.Write(Panel); // Render it

        bool isSelected = false;
        int currentOption = 1;
        int groupsize = 0;

        while (!isSelected)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (currentOption != 7 && keyInfo.Key == ConsoleKey.UpArrow)
            {
                currentOption += 1;
                Buffer = $"         ^ \nReservation for {currentOption} people\n         v";
            }
            if (currentOption != 0 && keyInfo.Key == ConsoleKey.DownArrow)
            {
                currentOption -= 1;
                Buffer = $"         ^ \nReservation for {currentOption} people\n         v";
            }

            if (currentOption == 7 && keyInfo.Key == ConsoleKey.UpArrow)
            {
                currentOption = 1;
                Buffer = $"         ^ \nReservation for {currentOption} person\n         v";
            }
            if (currentOption == 0 && keyInfo.Key == ConsoleKey.DownArrow)
            {
                currentOption = 6;
                Buffer = $"         ^ \nReservation for {currentOption} people\n         v";
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                groupsize = currentOption;
                break;
            }

                Panel = new(new Text($"Enter the amount of people for the reservation using the arrow keys:\n{Buffer}\n").Centered()); // Update the panel and the text in it with the updated buffer
                Panel.Expand = true; // Set expand again
                AnsiConsole.Clear(); // Clear the previous print of the panel
                AnsiConsole.Write(Panel); // Re-render the panel with the updated text
        }

        ///resManager.CreateReservation(userID, locID, timeslot, date, groupsize);
    }
}