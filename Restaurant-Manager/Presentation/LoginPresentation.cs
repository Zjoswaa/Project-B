﻿using Spectre.Console;

static class LoginPresentation
{
    public static void Present()
    {
        //bool loginSuccessful = false;

        while (State.LoggedInUser is null)
        {
            // Display login header
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Login[/]"));
            AnsiConsole.WriteLine();

            // Ask for a Email
            AnsiConsole.MarkupLine("[blue]Please enter your Email, or press Enter to return:[/]");
            string Email = PromptEmail();
            if (string.IsNullOrEmpty(Email)) {
                AnsiConsole.Clear();
                MainMenuPresentation.ShowMainMenu();
            }

            // Ask for a password
            AnsiConsole.MarkupLine("[blue]Please enter your password, or press Enter to return:[/]");
            string Password = PromptPassword();
            if (string.IsNullOrEmpty(Password)) {
                AnsiConsole.Clear();
                MainMenuPresentation.ShowMainMenu();
            }

            // Check if user input correct password
            if (LoginLogic.VerifyPassword(Email, Password))
            {
                State.LoggedInUser = Database.GetUserByEmail(Email);
                Console.WriteLine("Successful login");
                //loginSuccessful = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid Email or password. Please try again.[/]");
                Thread.Sleep(1500);
            }
        }
    }

    private static string PromptEmail()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Email[/]:")
                .PromptStyle("yellow")
                .AllowEmpty()
            );
    }

    private static string PromptPassword()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("yellow")
                .Secret('*')
                .AllowEmpty()
        );
    }
}
