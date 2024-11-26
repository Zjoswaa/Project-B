using Spectre.Console;

static class AdminMenu {
    public static void ShowAdminMenu() {
        while (State.LoggedInUser is not null) {
            Console.Clear();

            AnsiConsole.Write(new Rule($"[blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/]"));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[cyan]Please select an option:[/]")
                .AddChoices(new[] { "Manage reservations", "Manage dishes", "Logout" });

            var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

            switch (userSelection) {
                case "Manage reservations":
                    ShowManageReservationsMenu();
                    break;
                case "Manage dishes":
                    ShowManageDishesMenu();
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

    private static void ShowManageDishesMenu() {
        Console.Clear();

        AnsiConsole.Write(new Rule($"[blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/]"));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] { "Add dish", "Delete dish", "Edit dish", "View dishes", "Exit" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection) {
            case "Add dish":
                MenuManagement.AddDish();
                break;
            case "Delete dish":
                MenuManagement.DeleteDish();
                break;
            case "Edit dish":
                MenuManagement.EditDish();
                break;
            case "View dishes":
                MenuCard.DisplayMenuCard();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Exit":
                break;
        }
    }

    private static void ShowManageReservationsMenu() {
        Console.Clear();

        AnsiConsole.Write(new Rule($"[blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/]"));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] { "Create reservation", "Delete reservation", "Edit reservation", "View reservations", "Exit" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection) {
            case "Create reservation":
                break;
            case "Delete reservation":
                break;
            case "Edit reservation":
                break;
            case "View reservations":
                ShowViewReservationsMenu();
                break;
            case "Exit":
                break;
        }
    }

    private static void ShowViewReservationsMenu() {
        Console.Clear();

        AnsiConsole.Write(new Rule($"[blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/]"));

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[] {"View all", "Search by email", "Exit" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection) {
            case "View all":
                ReservationManagement.ViewAllReservations();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Search by email":
                ReservationManagement.ViewReservationsByEmail();
                AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                Console.ReadKey();
                break;
            case "Exit":
                break;
        }
    }
}
