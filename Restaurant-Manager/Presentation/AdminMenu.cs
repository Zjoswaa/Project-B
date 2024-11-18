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
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    Console.Clear();
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
            .AddChoices(new[] { "Add dish", "Delete dish", "Edit dish", "Exit" });

        var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection) {
            case "Add dish":
                MenuLogic.AddDish();
                break;
            case "Delete dish":
                MenuLogic.DeleteDish();
                break;
            case "Edit dish":
                //Console.WriteLine("TBA...");
                //Thread.Sleep(1000);
                //Console.Clear();
                MenuLogic.EditDish();
                break;
            case "Exit":
                break;
        }
    }
}
