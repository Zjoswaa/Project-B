using Spectre.Console;

static class LoginPresentation
{
    public static void Present()
    {
        bool loginSuccessful = false;

        while (!loginSuccessful)
        {
            // Display login header
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Login[/]"));
            AnsiConsole.WriteLine();

            // Ask for a username
            AnsiConsole.MarkupLine("[blue]Please enter your username:[/]");
            string username = PromptUsername();

            // Ask for a password
            AnsiConsole.MarkupLine("[blue]Please enter your password:[/]");
            string password = PromptPassword();

            // Check if user input correct password
            if (LoginLogic.VerifyPassword(username, password))
            {
                State.LoggedInUser = Database.GetUserByUsername(username);
                Console.WriteLine("Successful login");
                loginSuccessful = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid username or password. Please try again.[/]");
                Thread.Sleep(1500);
            }
        }
    }

    private static string PromptUsername()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Username[/]:")
                .PromptStyle("yellow")
                .ValidationErrorMessage("[red]Username cannot be empty[/]")
                .Validate(username => !string.IsNullOrWhiteSpace(username))
        );
    }

    private static string PromptPassword()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("yellow")
                .Secret('*')
                .ValidationErrorMessage("[red]Password cannot be empty[/]")
                .Validate(password => !string.IsNullOrWhiteSpace(password))
        );
    }
}