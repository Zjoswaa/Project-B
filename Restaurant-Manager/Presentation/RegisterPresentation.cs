using Spectre.Console;

static class RegisterPresentation
{
    public static void Present(bool InsertIntoUsersTable)
    {

        AnsiConsole.Write(
                new Rule("[yellow]Register[/]")
            );

        // Ask for new user username
        string Username = PromptUsername();

        // Ask for new user password
        string Password = PromptPassword(Username);

        // Ask for user first name
        string FirstName = PromptFirstName(Username, Password);

        // Ask for user last name
        string LastName = PromptLastName(Username, Password, FirstName);

        // Optionally insert a new user into the table
        if (InsertIntoUsersTable)
        {
            Database.InsertUsersTable(Username, Password, FirstName, LastName, "USER");
        }

        // Display registration success message and redirect to choice page
        ShowSuccessMessageAndRedirect();
    }

    private static string PromptUsername()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[yellow]Register[/]")
            );
            string Username = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Enter your username:[/]")
                .Validate(n => {
                    // Another user with that username already exists
                    if (Database.UsersTableContainsUser(n))
                    {
                        return ValidationResult.Error("[red]This username is already taken[/]");
                    }

                    // Invalid username length, null or only whitespace
                    if (!RegisterLogic.UsernameValid(n))
                    {
                        return ValidationResult.Error("[red]Username must be at least 4 characters long[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

            return Username;
        }
    }

    private static string PromptPassword(string Username)
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[yellow]Register[/]")
            );
            AnsiConsole.MarkupLine($"[green]Username:[/] {Username}");

            string Password = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Enter your password:[/]")
                .Validate(p => {
                    // Invalid password length, null or only whitespace
                    if (!RegisterLogic.PasswordValid(p))
                    {
                        return ValidationResult.Error("[red]Password must be at least 8 characters long[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
                .Secret('*')
            );

            return Password;
        }
    }

    private static string PromptFirstName(string Username, string Password)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Register[/]")
        );
        AnsiConsole.MarkupLine($"[green]Username:[/] {Username}");
        AnsiConsole.MarkupLine($"[green]Password:[/] {new string('*', Password.Length)}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter your first name (Optional):[/]").AllowEmpty());
    }

    private static string PromptLastName(string Username, string Password, string FirstName)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Register[/]")
        );
        AnsiConsole.MarkupLine($"[green]Username:[/] {Username}");
        AnsiConsole.MarkupLine($"[green]Password:[/] {new string('*', Password.Length)}");
        AnsiConsole.MarkupLine($"[blue]First name (Optional):[/] {FirstName}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter your last name (Optional):[/]").AllowEmpty());
    }

    private static void ShowSuccessMessageAndRedirect()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Registration Successful![/]");
        AnsiConsole.MarkupLine("[bold yellow]Please log in.[/]");
        Console.Read();
        AnsiConsole.Clear();

        // Redirect to the choice page
        Program.ShowMainMenu();
    }
}
