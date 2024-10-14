using Spectre.Console;
using System;
using System.Threading;

class Program
{
    public static void Main()
    {
        try
        {
            Database.Connect("database.db");
            Database.OpenConnection();
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error initializing the database: {ex.Message}[/]");
            Environment.Exit(1);
        }

        ShowMainMenu();
    }

    public static void ShowMainMenu()
    {
        // Display welcome message in a box
        AnsiConsole.Write(
            new Panel("[bold yellow]Welcome![/] [green]What would you like to do?[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
        );

        // Display selection menu
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .AddChoices(new[] { "Login", "Register", "Exit" }));

        // Handle user selection
        switch (selection)
        {
            case "Login":
                LoginPresentation.Present();
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
    }
}