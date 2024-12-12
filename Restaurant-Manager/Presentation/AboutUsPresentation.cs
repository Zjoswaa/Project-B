using Spectre.Console;

static class AboutUsPresentation
{
    public static void DisplayAboutUs()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule("[bold yellow]Escape Rooms[/]").RuleStyle("yellow"));

        AnsiConsole.MarkupLine("The best escape room in Rotterdam and the surrounding area");
        AnsiConsole.MarkupLine("Will you conquer the most thrilling experience?\n");

        AnsiConsole.MarkupLine("Immerse yourself in one of our escape rooms, and who knows what might await you at the end.");
        AnsiConsole.MarkupLine("At our escape room in Rotterdam, you’ll experience an exhilarating adventure where you and your team are locked inside one of our four unique themed rooms. What’s the challenge? You must work together as a team to unravel riddles, solve puzzles, and complete various tasks to escape within 60 minutes.");
        AnsiConsole.MarkupLine("[italic grey]Each room is a portal to a different world, each with its own secrets to unravel.[/]\n");

        // misschien eerst key input vragen aan user voordat de verschillende themes gedisplayed worden ?

        // The different escape room themes
        var escapeRooms = new[]
        {
            new { Name = "The Pharaoh's Tomb", Storyline = "Epic description here" },
            new { Name = "theme name", Storyline = "blank" },
            new { Name = "theme name", Storyline = "blank" },
            new { Name = "theme name", Storyline = "blank" },
        };

        foreach (var room in escapeRooms)
        {
            AnsiConsole.Write(new Panel($"[bold maroon]{room.Name}[/]\n\n[italic]{room.Storyline}[/]")
                .Header("[green]Escape Room[/]")
                .Expand());
            Console.WriteLine();
        }

        AnsiConsole.MarkupLine("[grey italic]Escape & Dine offers an adventure like no other, full of mystery, puzzles and unforgettable surprises.[/]");
        AnsiConsole.MarkupLine("[italic]We can't wait to see you![/]\n");

        AnsiConsole.MarkupLine("[yellow]Press any key to return to the main menu...[/]");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }
}