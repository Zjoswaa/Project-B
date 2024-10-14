using Spectre.Console;

static class RegisterPresentation {
    public static void Present(bool InsertIntoUsersTable) {
        // Ask for new user username
        string Username = PromptUsername();

        // Ask for new user password
        string Password = PromptPassword(Username);

        // Ask for user first name
        string FirstName = PromptFirstName(Username, Password);
        
        // Ask for user last name
        string LastName = PromptLastName(Username, Password, FirstName);

        // Optionally insert a new user into the table
        if (InsertIntoUsersTable) {
            Database.InsertUsersTable(Username, Password, FirstName, LastName, "USER");
        }
    }

    private static string PromptUsername() {
        while (true) {
            AnsiConsole.Clear();
            string Username = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Username[/]:")
                .Validate(n => {
                    // Another user with that username already exists
                    if (Database.UsersTableContainsUser(n)) {
                        AnsiConsole.Clear();
                        return ValidationResult.Error("[red]This username is already taken[/]");
                    }

                    // Invalid username length, null or only whitespace
                    if (!RegisterLogic.UsernameValid(n)) {
                        AnsiConsole.Clear();
                        return ValidationResult.Error("[red]Username must be at least 3 characters long[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

            return Username;
        }
    }

    private static string PromptPassword(string Username) {
        while (true) {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[green]Username[/]: {Username}");

            string Password = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
                .Validate(p => {
                    // Invalid password length, null or only whitespace
                    if (!RegisterLogic.PasswordValid(p)) {
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine($"[green]Username[/]: {Username}");
                        return ValidationResult.Error("[red]Password must be at least 3 characters long[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
                .Secret('*')
            );

            return Password;
        }
    }

    private static string PromptFirstName(string Username, string Password) {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[green]Username[/]: {Username}");
        AnsiConsole.MarkupLine($"[green]Password[/]: {new string('*', Password.Length)}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]First name (Optional)[/]: ").AllowEmpty());
    }

    private static string PromptLastName(string Username, string Password, string FirstName) {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[green]Username[/]: {Username}");
        AnsiConsole.MarkupLine($"[green]Password[/]: {new string('*', Password.Length)}");
        AnsiConsole.MarkupLine($"[blue]First name (Optional)[/]: {FirstName}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]Last name (Optional)[/]: ").AllowEmpty());
    }
}
