using Spectre.Console;

public static class ReservationPresentation
{
    public static void Present()
    {
        ReservationManager resManager = new();

        long userID = State.LoggedInUser.ID;

        var locationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a location:[/]")
            .AddChoices(resManager.LocationNamesToList()));

        long locID = resManager.GetSelectedLocationID(locationChoice);

        var timeslotChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[cyan]Select a timeslot:[/]")
            .AddChoices(resManager.TimeslotsToList()));
        string timeslot = timeslotChoice;

        

        resManager.CreateReservation(userID, locID, timeslot, date, groupsize);
    }
}