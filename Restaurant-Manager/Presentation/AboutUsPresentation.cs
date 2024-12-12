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
            new { Name = "The Silent Hotel", Storyline = "It was supposed to be a quick overnight stay. The Silent Hotel, nestled in the middle of nowhere, seemed like the perfect place to rest before continuing your journey. Its antique charm and the promise of a quiet night’s sleep drew you in.\r\n\r\nBut something feels... off. The receptionist barely speaks as they hand you a tarnished key with the number 13 etched into it. The elevator creaks as it takes you to your floor, but instead of stopping at your room, the doors open onto an unfamiliar hallway. Dim lights flicker, and the air is heavy with the scent of mildew and something metallic.\r\n\r\nAs you step out, the elevator vanishes behind you. The walls seem to shift, and the faint sound of footsteps echoes in the distance. Is it your imagination, or is someone—or something—following you? You must navigate the eerie halls, uncover the secrets of the Silent Hotel, and find your way back to the real world before the hotel claims you as its next permanent guest." },
            new { Name = "Midnight at the Observatory", Storyline = "The famous astronomer, Dr. Evelyn Mercer, vanished without a trace five years ago while conducting research at the Hillcrest Observatory. Since then, the facility has been locked up, abandoned. But rumors persist of strange lights emanating from the dome at night and an otherworldly hum that grows louder as midnight approaches.\r\n\r\nYou’ve been invited to investigate these phenomena under the guise of a scientific experiment. As the clock strikes twelve, the observatory’s mechanisms roar to life. The telescope begins to move on its own, locking onto a star that grows brighter and brighter. The hum transforms into a deafening vibration, and suddenly, the room is bathed in a blinding light.\r\n\r\nWhen you open your eyes, the observatory is no longer the same. Time and space seem distorted, and cryptic symbols glow faintly on the walls. You must work together to decipher the celestial clues, repair the observatory’s systems, and stop the rift from tearing reality apart." },
            new { Name = "Depths of the Abyss", Storyline = "What was meant to be an exciting deep-sea exploration has turned into a fight for survival. Your team of researchers descended to explore the legendary ruins of a lost underwater city, thought to hold artifacts of unimaginable power. But as you approached the site, a sudden tremor shook your vessel, and you lost contact with the surface.\r\n\r\nWhen you awaken, your submersible is partially flooded, and the ancient ruins loom outside, eerily illuminated by bioluminescent flora. The tremors have triggered something deep within the city—a mechanism long dormant now groans to life. Strange symbols pulse with an ominous light, and the water pressure rises dangerously.\r\n\r\nYou must navigate the labyrinth of underwater tunnels, solve the mysteries of the ruins, and reactivate the ancient technology to power your escape. But hurry—oxygen is running low, and the abyss is awakening." },
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

        AnsiConsole.MarkupLine("[italic grey]Press any key to return to the main menu...[/]");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }
}