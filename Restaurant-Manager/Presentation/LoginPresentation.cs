using Spectre.Console;

static class LoginPresentation
{
    public static void Present()
    {
        // display login header
        AnsiConsole.Clear();
        var panel = new Panel("[bold yellow]Welcome to: [/] [bold blue]Escape & Dine[/]")
            .Border(BoxBorder.Rounded)
            .Padding(1, 1);
        AnsiConsole.Write(panel);

        AnsiConsole.MarkupLine("[bold green]Please log in to continue[/]");
        AnsiConsole.WriteLine();

        // dsk for a username
        string username = PromptUsername();

        // ask for a password kan ook prompt password zijn
        string password = PromptPassword(username);
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

    private static string PromptPassword(string username)
    {
        AnsiConsole.MarkupLine($"[green]Username[/]: {username}");
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("yellow")
                .Secret('*')
                .ValidationErrorMessage("[red]Password cannot be empty[/]")
                .Validate(password => !string.IsNullOrWhiteSpace(password))
        );
    }
}
