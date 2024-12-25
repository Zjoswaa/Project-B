using Spectre.Console;

static class AboutUsPresentation
{
    public static void DisplayAboutUs()
    {
        Console.Clear();

        AnsiConsole.Write(new Rule("[bold maroon]About Us[/]").RuleStyle("maroon"));

        AnsiConsole.MarkupLine("The best escape rooms in The Netherlands!");
        AnsiConsole.MarkupLine("Will you conquer the most thrilling experience?\n");

        AnsiConsole.MarkupLine("Immerse yourself in one of our escape rooms, and who knows what might await you at the end.");
        AnsiConsole.MarkupLine("At our escape rooms, you’ll experience an exhilarating adventure where you (and your team) are locked inside one of our unique themed rooms. What’s the challenge? You must give all your brain power to unravel riddles, solve puzzles, and complete various tasks to escape within 60 minutes.");
        AnsiConsole.MarkupLine("[italic grey]Each room is a portal to a different world, each with its own secrets to unravel.[/]\n");

        AnsiConsole.Write(new Rule("[bold maroon]Escape Rooms[/]").RuleStyle("maroon"));
        // misschien eerst key input vragen aan user voordat de verschillende themes gedisplayed worden ?

        // The different escape room themes
        var escapeRoomsRotterdam = new[]
        {
            new { Name = "The Asylum of Whisperers", Storyline = "The abandoned asylum whispers secrets of its tormented patients. As you explore its shadowy halls, the whispers grow louder, revealing dark truths about the past—and your own connection to them. Can you escape before the whispers consume you?" },
            new { Name = "The Silent Hotel", Storyline = "An eerie hotel sits empty, its halls echoing with the absence of guests who vanished one fateful night. As you investigate, the silence feels oppressive, and each room hides a piece of the mystery. Will you check out in time?" },
            new { Name = "Midnight at the Observatory", Storyline = "At midnight, the abandoned observatory comes alive with celestial maps and strange instruments. The stars shift unnaturally, and you must decode the astronomer’s last discovery before the sky locks you in forever." },
            new { Name = "Depths of the Abyss", Storyline = "Deep beneath the ocean, an underwater station has gone silent. As you descend, cryptic clues and a growing sense of dread point to something ancient lurking in the depths. Escape before the abyss swallows you whole." }
        };

        var escapeRoomsAmsterdam = new[]
        {
            new { Name = "The Vanishing Vault", Storyline = "You’ve been hired to investigate an old bank that mysteriously shut down overnight in 1923. The vault remains sealed, and every previous attempt to open it ended in failure—--or worse, disappearance. Can you uncover the truth before the vault consumes you too?" },
            new { Name = "Labyrinth of Shadows", Storyline = "Hidden beneath the ruins of an ancient cathedral is a labyrinth filled with shifting walls and whispers of a lost relic. Your team must navigate the maze, solve its riddles, and escape before the shadows take form." },
            new { Name = "The Alchemist's Sanctum", Storyline = "In the heart of a crumbling tower lies the laboratory of an alchemist who sought the secret of eternal life. The room is alive with his unfinished experiments, and it’s up to you to escape before becoming his next subject." },
            new { Name = "Secrets of the Forgotten Asylum", Storyline = "An abandoned asylum holds the secrets of a doctor who conducted unethical experiments on his patients. You must uncover his dark research to find a way out before the spirits of the past catch up to you." }
        };

        var escapeRoomsGroningen = new[]
        {
            new { Name = "Timekeeper’s Paradox", Storyline = "You’ve stumbled upon the workshop of a mysterious clockmaker who vanished decades ago. The clocks within tick backward, and reality seems to shift with each chime. Can you repair the timeline and escape?" },
            new { Name = "Curse of the Crimson Manor", Storyline = "The once-grand Crimson Manor is rumored to be cursed, and those who enter are said to vanish forever. As you investigate, the walls seem to close in, and a sinister presence begins to manifest." },
            new { Name = "The Phantom's Playhouse", Storyline = "An abandoned theater is haunted by the spirit of a playwright whose final work was never performed. You must bring the script to life to free his soul and escape the stage before the final curtain falls." },
            new { Name = "The Oracle’s Chamber", Storyline = "Buried deep within the mountains is the Oracle’s Chamber, a place said to reveal your future—if you survive its trials. The chamber tests your mind and resolve. Will you uncover the truth or be lost to time?" }
        };

        var escapeRoomsZwolle = new[]
        {
            new { Name = "Whispering Woods Cottage", Storyline = "You’ve sought refuge in a small cottage in the woods, but as night falls, the house begins to whisper your darkest fears. Solve its mysteries to escape before dawn breaks—or you may never leave." },
            new { Name = "Crypt of the Forgotten Pharaoh", Storyline = "You’ve been hired to explore an undiscovered Egyptian tomb. But the deeper you go, the more it feels like the tomb is alive. Can you escape before the Forgotten Pharaoh claims you as part of his eternal court?" },
            new { Name = "The Ship of Lost Souls", Storyline = "A ghostly ship has reappeared after centuries, shrouded in mist. You and your team board it, but the doors slam shut behind you. Solve its mysteries to escape before the ship disappears again—with you on it." },
            new { Name = "The Midnight Apothecary", Storyline = "An ancient apothecary’s shop opens only at midnight, selling cures for ailments no modern doctor can treat. But the cure comes with a price—and a clock counting down. Can you escape before it’s too late?" }
        };

        var escapeRoomsUtrecht = new[]
        {
            new { Name = "Echoes of the Enchanted Library", Storyline = "Books in this ancient library hold more than words—they hold trapped souls. You must solve the riddles within the enchanted texts to find your way out before you become part of the collection." },
            new { Name = "The Blackwell Experiment", Storyline = "A biotech lab known as Blackwell Industries has gone dark. Inside, you discover a chilling experiment that has gone horribly wrong. The only way out is through a series of locked doors—and the answers lie in the experiments themselves." },
            new { Name = "The Silent Carnival", Storyline = "A carnival suddenly appeared on the outskirts of town, but it’s eerily silent. Each attraction seems designed to test your courage, wit, and sanity. Can you survive the games and escape?" },
            new { Name = "Portal of the Forgotten Realm", Storyline = "An abandoned observatory is home to a strange portal humming with energy. When you step inside, you’re transported to a realm of impossible geometry and ancient beings. Solve the puzzles to reopen the portal and return to your world." }
        };



        //foreach (var room in escapeRoomsRotterdam)
        //{
        //    AnsiConsole.Write(new Panel($"[bold maroon]{room.Name}[/]\n\n[italic]{room.Storyline}[/]")
        //        .Header("[green]Escape Room[/]")
        //        .Expand());
        //    Console.WriteLine();
        //}

        AnsiConsole.MarkupLine("[grey italic]Escape & Dine offers an adventure like no other, full of mystery, puzzles and unforgettable surprises.[/]");
        AnsiConsole.MarkupLine("[italic]We can't wait to see you![/]\n");

        AnsiConsole.MarkupLine("[italic grey]Press any key to return to the main menu...[/]");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }
}