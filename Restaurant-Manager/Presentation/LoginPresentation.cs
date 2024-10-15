using Spectre.Console;

static class LoginPresentation
{
    public static void Present()
    {
        // display login header
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[bold yellow]Login[/]")
        );

        AnsiConsole.WriteLine();

        // dsk for a username
        string Username = PromptUsername();

        // ask for a password kan ook prompt password zijn
        string Password = PromptPassword(Username);

        // Check if user input correct password
        if (LoginLogic.VerifyPassword(Username, Password)) {
            State.LoggedInUser = Database.GetUserByUsername(Username);
            Console.WriteLine("Successful login");
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

    private static string PromptPassword(string Username)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("yellow")
                .Secret('*')
                .ValidationErrorMessage("[red]Password cannot be empty[/]")
                .Validate(p => {
                    if (string.IsNullOrWhiteSpace(p)) {
                        return ValidationResult.Error("[red]Invalid password[/]");
                    }

                    if (!LoginLogic.VerifyPassword(Username, p)) {
                        return ValidationResult.Error("[red]Invalid password[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                }
        ));
    }
}
