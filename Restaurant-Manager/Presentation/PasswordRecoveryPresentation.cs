using Spectre.Console;

static class PasswordRecoveryPresentation {
    public static void Present() {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Forgor Password[/]")
        );

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[blue]Enter the email for which you forgot the password.[/]");
        string Email = PromptEmail();

        string Code = Util.RandomString(5);
        AnsiConsole.MarkupLine($"[blue]Sending a recovery email to {Email}, this may take a moment.[/]");
        EmailService.SendPasswordForgorEmail(Email, Code);
        AnsiConsole.MarkupLine("[blue]Email sent, please enter the code provided in the recovery email.[/]");
        string EnteredCode = PromptCode();

        if (EnteredCode == Code) {
            AnsiConsole.MarkupLine("[blue]Enter your new password.[/]");
            Database.SetUserPassword(Email, PromptNewPassword());
        } else {
            AnsiConsole.MarkupLine("[red]Incorrect code[/]");
        }

        AnsiConsole.MarkupLine("[bold yellow]Press any key to continue to the main menu.[/]");
        Console.Read();
        AnsiConsole.Clear();
    }

    private static string PromptEmail()
    {
        while (true)
        {
            string Email = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Email:[/]")
            );

            return Email;
        }
    }

    private static string PromptCode() {
        while (true) {
            string Code = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Code:[/]")
            );

            return Code;
        }
    }

    private static string PromptNewPassword() {
        while (true) {
            string NewPassword = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("yellow")
                .Secret('*')
            );

            return NewPassword;
        }
    }
}
