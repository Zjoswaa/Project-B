using Spectre.Console;


class UserMenu
{
    public static void ShowUserMenu()
    {
        while (State.LoggedInUser is not null)
        {
            Console.Clear();

            AnsiConsole.Write(new Rule($"[yellow]Reservation Menu ({State.LoggedInUser.GetFullName()})[/]"));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[cyan]Please select an option:[/]")
                .AddChoices(new[] { "Make a Reservation", "View Reservation", "Edit Reservations", "Remove Reservation", "View Menu", "Logout" });

            var userSelection = AnsiConsole.Prompt(userSelectionPrompt);
          
            switch (userSelection)
            {
                case "Make a Reservation":
                    Console.Clear();
                    ReservationPresentation.Present();
                    break;
                case "View Reservation":
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "Edit Reservations":
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "Remove Reservation":
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    break;
                case "View Menu":
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "Logout":
                    AnsiConsole.MarkupLine("[red]Logging out...[/]");
                    State.LoggedInUser = null;
                    Thread.Sleep(1000);
                    Console.Clear();
                    MainMenuPresentation.ShowMainMenu();
                    break;
            }
        }
    }
}
