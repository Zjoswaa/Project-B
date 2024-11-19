using Spectre.Console;

static class EditReservationPresentation
{
    public static void Present()
    {
        long currentUserID = State.LoggedInUser.ID;
        Reservation reservationToEdit = SelectReservation(currentUserID);
        List<string> dataToChange = InfoToEdit();

        foreach (string variable in dataToChange)
        {
            if (variable == "Date")
            {
                DateOnly date = EditDate();
                reservationToEdit.Date = date;
            }
            if (variable == "Timeslot")
            {

            }
            if (variable == "Group size")
            {

            }
        }
    }

    public static Reservation SelectReservation(long currentUserID)
    {
        List<Reservation> userReservations = ReservationLogic.GetReservationsByUserID(currentUserID);

        var reservationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a reservation to edit:[/]")
            .AddChoices(ReservationLogic.ReservationsToString(userReservations)));
        
        long reservationID = ReservationLogic.ParseIDFromString(reservationChoice);

        return ReservationLogic.GetReservationByID(reservationID);
    }

    public static List<string> InfoToEdit()
    {
        var dataToChange = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("What data would you like to edit?")
                .NotRequired() // Not required to have a favorite fruit
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
}