using Spectre.Console;

public static class RemoveReservationPresentation {
    public static void Present() {
        AnsiConsole.Write(new Rule($"[yellow]Remove Reservation ({State.LoggedInUser.GetFullName()})[/]"));
        List<Reservation> userReservations = ReservationLogic.GetReservationsByUserID(State.LoggedInUser.ID);

        string reservationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]Select reservation to remove:[/]")
                .AddChoices(ReservationLogic.ReservationsToString(userReservations))
        );

        if (reservationChoice == "Exit") {
            return;
        }

        long reservationID = ReservationLogic.ParseIDFromString(reservationChoice);
        Database.DeleteReservationsTable(reservationID);
        AnsiConsole.WriteLine("Reservation Removed");
        AnsiConsole.Markup("[gray]Press any key to continue[/]");
        Console.ReadKey();
    }
}
