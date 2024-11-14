using Spectre.Console;

static class PasswordRecoveryPresentation {
    public static void Present() {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new Rule("[yellow]Forgor Password[/]")
        );

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[blue]Enter the email for which you forgot the password, or leave empty to cancel.[/]");
        string Email = PromptEmail();
        if (string.IsNullOrEmpty(Email)) {
            AnsiConsole.Clear();
            return;
        }

        string Code = Util.RandomString(5);
        AnsiConsole.MarkupLine($"[blue]Sending a recovery email to {Email}, this may take a moment.[/]");
        EmailService.SendPasswordForgorEmail(Email, Code);
        AnsiConsole.MarkupLine("[blue]Email sent, please enter the code provided in the recovery email.[/]");
        string EnteredCode = PromptCode();
        AnsiConsole.Clear();

        if (EnteredCode == Code) {
            while (true) {
                AnsiConsole.MarkupLine("[blue]Enter your new password.[/]");
                string NewPassword = PromptNewPassword();
                if (string.IsNullOrEmpty(NewPassword)) {
                    AnsiConsole.Clear();
                    return;
                }

                AnsiConsole.MarkupLine("[blue]Confirm your new password.[/]");
                string ConfirmPassword = PromptNewPassword();
                if (string.IsNullOrEmpty(ConfirmPassword)) {
                    AnsiConsole.Clear();
                    return;
                }

                if (NewPassword == ConfirmPassword) {
                    Database.SetUserPassword(Email, NewPassword);
                    AnsiConsole.MarkupLine("[green]Password updated successfully.[/]");
                    break;
                } else {
                    AnsiConsole.MarkupLine("[red]Passwords do not match. Please try again or press enter to cancel.[/]");
                }
            }
        } else {
            AnsiConsole.MarkupLine("[red]Incorrect code.[/]");
        }

        AnsiConsole.MarkupLine("[bold yellow]Press any key to continue to the main menu.[/]");
        Console.Read();
        AnsiConsole.Clear();
    }

    private static string PromptEmail() {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Email:[/]")
            .Validate(e => {
                // Invalid password length, null or only whitespace
                if (!RegisterLogic.EmailValidOrEmpty(e)) {
                    return ValidationResult.Error("[red]Invalid email[/]");
                }

                // If all checks pass
                return ValidationResult.Success();
            })
                .PromptStyle("yellow")
                .AllowEmpty()
            );
    }

    private static string PromptCode() {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Code:[/]")
        );
    }

    private static string PromptNewPassword() {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
            .PromptStyle("yellow")
            .Validate(p => {
                // Invalid password length, null or only whitespace
                if (!RegisterLogic.PasswordValidOrEmpty(p)) {
                    return ValidationResult.Error("[red]Password must be at least 8 characters long[/]");
                }

                // If all checks pass
                return ValidationResult.Success();
            })
            .Secret('*')
            .AllowEmpty()
        );
    }
}
