using Spectre.Console;

static class AdminMenu
{
    public static void ShowAdminMenu()
    {
        while (State.LoggedInUser is not null)
        {
            Console.Clear();

            AnsiConsole.Write(new Rule($" [blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/] "));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[cyan]Please select an option:[/]")
                .AddChoices(new[] { "Manage reservations", "Manage dishes", "View all users", "Logout" });

            var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

            switch (userSelection)
            {
                case "Manage reservations":
                    ShowManageReservationsMenu();
                    break;
                case "Manage dishes":
                    ShowManageDishesMenu();
                    break;
                case "View all users":
                    ViewUsers.ViewAllUsers();
                    AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                    Console.ReadKey();
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

    private static void ShowManageDishesMenu()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule($" [blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/] "));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] { "View dishes", "Add dish", "Edit dish", "Delete dish", "Back" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection)
        {
            case "View dishes":
                MenuCard.DisplayMenuCard();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Add dish":
                MenuManagement.AddDish();
                break;
            case "Edit dish":
                MenuManagement.EditDish();
                break;
            case "Delete dish":
                MenuManagement.DeleteDish();
                break;
            case "Back":
                break;
        }
    }

    private static void ShowManageReservationsMenu()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule($" [blue]Manage reservations ({State.LoggedInUser.GetFullName()})[/] "));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] { "View reservations", "Create reservation", "Edit reservation", "Delete reservation", "Back" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection)
        {
            case "View reservations":
                ShowViewReservationsMenu();
                break;
            case "Create reservation":
                break;
            case "Edit reservation":
                break;
            case "Delete reservation":
                ReservationManagement.DeleteReservation();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Back":
                break;
        }
    }

    private static void ShowViewReservationsMenu()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule($" [blue]View Reservations ({State.LoggedInUser.GetFullName()})[/] "));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] { "View all", "Search by email", "Exit" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection)
        {
            case "View all":
                ReservationManagement.ViewAllReservations();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Search by email":
                ReservationManagement.ViewReservationsByEmail();
                break;
            case "Exit":
                break;
        }
    }
}
