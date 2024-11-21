using Spectre.Console;

static class EditReservationPresentation
{
    public static void Present()
    {
        AnsiConsole.Write(new Rule($"[yellow]Edit Reservation ({State.LoggedInUser.GetFullName()})[/]"));
        long currentUserID = State.LoggedInUser.ID;
        Reservation reservationToEdit = SelectReservation(currentUserID);

        if (reservationToEdit is null)
        {
            return;
        }
        
        List<string> dataToChange = InfoToEdit();

        if (!dataToChange.Any()) return;


        foreach (string variable in dataToChange)
        {
            if (variable == "Date")
            {
                DateOnly date = EditDate();
                reservationToEdit.UpdateDate(date);
            }
            if (variable == "Timeslot")
            {
                string timeslot = EditTimeslot();
                reservationToEdit.UpdateTimeslot(timeslot);
            }
            if (variable == "Group size")
            {
                int groupSize = EditGroupSize();
                reservationToEdit.UpdateGroupSize(groupSize);
            }
        }
        int table = ReservationLogic.GetTableCount(reservationToEdit.LocationID, reservationToEdit.Timeslot, reservationToEdit.Date);
        reservationToEdit.UpdateTable(table);
        string locMessage = ReservationLogic.GetLocationDescription(reservationToEdit.LocationID);

        (bool success, string message) = ReservationLogic.UpdateReservation(reservationToEdit);
        if (success)
        {
            string text = $"[green]Your reservation has been made.[/]\nYour Table Number: {reservationToEdit.Table}\n\n{locMessage}\n\nPress any key to continue.";
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

    public static Reservation SelectReservation(long currentUserID)
    {
        List<Reservation> userReservations = ReservationLogic.GetReservationsByUserID(currentUserID);

        var reservationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a reservation to edit:[/]")
            .AddChoices(ReservationLogic.ReservationsToString(userReservations)));

        if (reservationChoice == "Exit")
        {
            return null;
        }
        
        long reservationID = ReservationLogic.ParseIDFromString(reservationChoice);

        return ReservationLogic.GetReservationByID(reservationID);
    }

    public static List<string> InfoToEdit()
    {
        var dataToChange = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("What data would you like to edit?")
                .NotRequired()
                .InstructionsText(
                    "[grey](Press [blue]space[/] to select what to edit, " + 
                    "[green]Enter[/] to accept)[/]")
                .AddChoices(new[] {"Date", "Timeslot", "Group size"}));
        
        return dataToChange;
    }

    public static DateOnly EditDate()
    {
        (int Day, int Month, int Year) Date = ReservationPresentation.SelectDate();
        string dateString = $"{Date.Day}-{Date.Month}-{Date.Year}";
        DateOnly date = ReservationLogic.ParseDate(dateString);

        return date;
    }

    public static string EditTimeslot()
    {
        string timeslot = ReservationPresentation.SelectTimeslot();
        return timeslot;
    }

    public static int EditGroupSize()
    {
        int groupSize = ReservationPresentation.SelectGroupSize();
        return groupSize;
    }
}