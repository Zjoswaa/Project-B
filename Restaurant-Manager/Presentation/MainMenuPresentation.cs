using Spectre.Console;

static class MainMenuPresentation {
    public static void ShowMainMenu() {
        bool loggedIn = false;
        while (true) {
            // Display welcome message in a box
            AnsiConsole.Write(new Rule("[yellow] Welcome [/]"));

            // Display selection menu
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    //.Title("[yellow]Please make a choice:[/]")
                    .AddChoices(new[] { "Login", "Register", "Exit" }));

            // Handle user selection
            switch (selection) {
                case "Login":
                    LoginPresentation.Present();
                    loggedIn = true;
                    break;
                case "Register":
                    RegisterPresentation.Present(true);
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("[red]Exiting the program...[/]");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }

            UserMenu.ShowUserMenu(ref loggedIn);
        }
    }
}
