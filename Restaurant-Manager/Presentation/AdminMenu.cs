using Spectre.Console;

class AdminMenu {
    public static void ShowAdminMenu() {
        while (State.LoggedInUser is not null) {
            Console.Clear();

            AnsiConsole.Write(new Rule($"[blue]Admin Menu ({State.LoggedInUser.GetFullName()})[/]"));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[cyan]Please select an option:[/]")
                .AddChoices(new[] { "Manage reservations", "Manage menu", "Logout" });

            var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

            switch (userSelection) {
                case "Manage reservations":
                    Console.WriteLine("TBA...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "Manage menu":
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
