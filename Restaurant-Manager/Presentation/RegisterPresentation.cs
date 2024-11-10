using Spectre.Console;

static class RegisterPresentation
{
    public static void Present(bool InsertIntoUsersTable)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Register[/]")
        );

        AnsiConsole.WriteLine();

        // Ask for new user email
        AnsiConsole.MarkupLine("[blue]Please enter your email, or press Enter to return:[/]");
        string email = PromptEmail();
        if (string.IsNullOrEmpty(email)) {
            AnsiConsole.Clear();
            MainMenuPresentation.ShowMainMenu();
        }

        // Ask for new user password
        AnsiConsole.MarkupLine("[blue]Please enter your password, or press Enter to return:[/]");
        string Password = PromptPassword(email);
        if (string.IsNullOrEmpty(Password)) {
            AnsiConsole.Clear();
            MainMenuPresentation.ShowMainMenu();
        }

        // Ask for user first name
        string FirstName = PromptFirstName(email, Password);

        // Ask for user last name
        string LastName = PromptLastName(email, Password, FirstName);

        // Optionally insert a new user into the table
        if (InsertIntoUsersTable)
        {
            Database.InsertUsersTable(email, Password, FirstName, LastName, "USER");
        }

        // Display registration success message and redirect to choice page
        ShowSuccessMessageAndRedirect();
    }

    private static string PromptEmail()
    {
        while (true)
        {
            string Email = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Email:[/]")
                .Validate(n => {
                    // Another user with that Email already exists
                    if (Database.UsersTableContainsUser(n))
                    {
                        return ValidationResult.Error("[red]This email is already registered to another account[/]");
                    }

                    // Invalid Email length, null or only whitespace
                    if (!RegisterLogic.EmailValid(n))
                    {
                        return ValidationResult.Error("[red]Invalid email[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
                .AllowEmpty()
            );

            return Email;
        }
    }

    private static string PromptPassword(string Email)
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[yellow]Register[/]")
            );
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[blue]Please enter your email, or press Enter to return:[/]");
            AnsiConsole.MarkupLine($"[green]Email:[/] {Email}");

            AnsiConsole.MarkupLine("[blue]Please enter your password, or press Enter to return:[/]");
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
                .AllowEmpty()
            );

            return Password;
        }
    }

    private static string PromptFirstName(string Email, string Password)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Register[/]")
        );
        AnsiConsole.MarkupLine($"[green]Email:[/] {Email}");
        AnsiConsole.MarkupLine($"[green]Password:[/] {new string('*', Password.Length)}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]First name (Optional):[/]").AllowEmpty());
    }

    private static string PromptLastName(string Email, string Password, string FirstName)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Register[/]")
        );
        AnsiConsole.MarkupLine($"[green]Email:[/] {Email}");
        AnsiConsole.MarkupLine($"[green]Password:[/] {new string('*', Password.Length)}");
        AnsiConsole.MarkupLine($"[blue]First name (Optional):[/] {FirstName}");
        return AnsiConsole.Prompt(new TextPrompt<string>("[blue]Last name (Optional):[/]").AllowEmpty());
    }

    private static void ShowSuccessMessageAndRedirect()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Registration Successful![/]");
        AnsiConsole.MarkupLine("[bold yellow]Please log in.[/]");
        Console.Read();
        AnsiConsole.Clear();

        // Redirect to the choice page
        MainMenuPresentation.ShowMainMenu();
    }
}
