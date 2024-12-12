using Spectre.Console;

static class AboutUsPresentation
{
    public static void DisplayAboutUs()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule("[bold maroon]About Us[/]").RuleStyle("maroon"));

        AnsiConsole.MarkupLine("The best escape room in Rotterdam and the surrounding area");
        AnsiConsole.MarkupLine("Will you conquer the most thrilling experience?\n");

        AnsiConsole.MarkupLine("Immerse yourself in one of our escape rooms, and who knows what might await you at the end.");
        AnsiConsole.MarkupLine("At our escape room in Rotterdam, you’ll experience an exhilarating adventure where you and your team are locked inside one of our four unique themed rooms. What’s the challenge? You must work together as a team to unravel riddles, solve puzzles, and complete various tasks to escape within 60 minutes.");
        AnsiConsole.MarkupLine("[italic grey]Each room is a portal to a different world, each with its own secrets to unravel.[/]\n");

        AnsiConsole.Write(new Rule("[bold maroon]Escape Rooms[/]").RuleStyle("maroon"));
        // misschien eerst key input vragen aan user voordat de verschillende themes gedisplayed worden ?

        // The different escape room themes
        var escapeRooms = new[]
        {
            new { Name = "The Asylum of Whisperers", Storyline = "The asylum was abandoned years ago, yet the townsfolk still speak of the eerie cries that echo from its crumbling halls. Legends tell of a doctor who conducted unspeakable experiments on his patients, searching for a way to silence the voices in their heads. But instead of quieting their minds, he unleashed something far worse.\r\n\r\nTonight, you and your team find yourselves at the gates of this forsaken place, driven by curiosity—or perhaps something darker. As you step inside, the air grows thick, and a faint whisper brushes past your ear. It's not long before the doors slam shut behind you. The whispers grow louder, guiding you deeper into the asylum’s labyrinthine corridors.\r\n\r\nYou must uncover the truth behind the whispers and escape before you, too, are lost to the madness. But beware, some secrets are better left buried." },
            new { Name = "The Silent Hotel", Storyline = "blank" },
            new { Name = "Midnight at the Observatory", Storyline = "blank" },
            new { Name = "Depths of the Abyss", Storyline = "blank" },
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