using Spectre.Console;

static class ReservationManagement {
    public static void ViewAllReservations() {
        Console.Clear();
        AnsiConsole.Write(new Rule($"[yellow]View Reservations[/]"));
        var table = new Table().Centered();
        List<Reservation> Reservations = Database.GetAllReservations();

        // Animate
        AnsiConsole.Live(table)
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Top)
            .Start(ctx =>
            {
                void Update(int delay, Action action)
                {
                    action();
                    ctx.Refresh();
                    Thread.Sleep(delay);
                }

                // Columns
                Update(200, () => table.AddColumn("Reservation ID"));
                Update(200, () => table.AddColumn("User")); // ID and Email
                Update(200, () => table.AddColumn("Location")); // ID and Name
                Update(200, () => table.AddColumn("Time"));
                Update(200, () => table.AddColumn("Date"));
                Update(200, () => table.AddColumn("Group Size"));
                Update(200, () => table.AddColumn("Table"));

                // Rows
                foreach (Reservation res in Reservations) {
                    User? User = Database.GetUserByID(res.UserID);
                    Location? Location = Database.GetLocationByID(res.LocationID);
                    Update(100, () => table.AddRow($"{res.ID}", $"({res.UserID}) {User?.Email}", $"({res.LocationID}) {Location?.Name}", $"{res.Timeslot}", res.Date.ToString(), $"{res.GroupSize}", $"{res.Table}"));
                }
            });
    }

    public static void ViewReservationsByEmail() {
        AnsiConsole.Clear();
        // AnsiConsole.Write(new Rule($"[yellow]View Reservations[/]"));
        // var table = new Table().Centered();
        //
        // string Email = PromptEmail();
        // if (String.IsNullOrEmpty(Email)) {
        //     return;
        // }

        // Create the layout
        Console.CursorVisible = false;
        string Buffer = "";
        Panel Panel = new(new Text(Buffer).Centered());
        Panel.Header = new PanelHeader("[grey]Type email, escape to exit[/]").Centered();
        Panel.Expand = true;
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Bottom"));

        layout["Top"].Size(3);

        layout["Top"].Update(Panel);
        AnsiConsole.Write(layout);
        while (true) {
            ConsoleKeyInfo Input = Console.ReadKey();
            if (Input.Key == ConsoleKey.Escape) {
                return;
            }
            if (Input.Key == ConsoleKey.Backspace) {
                if (!String.IsNullOrEmpty(Buffer)) {
                    Buffer = Buffer.Substring(1);
                }
            }
            else {
                Buffer += Input.KeyChar;
            }
            
            Panel = new(new Text(Buffer).Centered());
            Panel.Header = new PanelHeader("[grey]Type email, escape to exit[/]").Centered();
            Panel.Expand = true;
            AnsiConsole.Clear();

            layout["Top"].Update(Panel);

            AnsiConsole.Write(layout);
        }


        // List<Reservation> Reservations = Database.GetReservationsByEmail(Email);
        //
        // // Animate
        // AnsiConsole.Clear();
        // AnsiConsole.Live(table)
        //     .AutoClear(false)
        //     .Overflow(VerticalOverflow.Ellipsis)
        //     .Cropping(VerticalOverflowCropping.Top)
        //     .Start(ctx =>
        //     {
        //         void Update(int delay, Action action) {
        //             action();
        //             ctx.Refresh();
        //             Thread.Sleep(delay);
        //         }
        //
        //         // Columns
        //         Update(200, () => table.AddColumn("Reservation ID"));
        //         Update(200, () => table.AddColumn("User")); // ID and Email
        //         Update(200, () => table.AddColumn("Location")); // ID and Name
        //         Update(200, () => table.AddColumn("Time"));
        //         Update(200, () => table.AddColumn("Date"));
        //         Update(200, () => table.AddColumn("Group Size"));
        //         Update(200, () => table.AddColumn("Table"));
        //
        //         // Rows
        //         foreach (Reservation res in Reservations) {
        //             User? User = Database.GetUserByID(res.UserID);
        //             Location? Location = Database.GetLocationByID(res.LocationID);
        //             Update(100,
        //                 () => table.AddRow($"{res.ID}", $"({res.UserID}) {User?.Email}",
        //                     $"({res.LocationID}) {Location?.Name}", $"{res.Timeslot}", res.Date.ToString(),
        //                     $"{res.GroupSize}", $"{res.Table}"));
        //         }
        //     });
        // AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
        Console.ReadKey();
        Console.CursorVisible = true;
    }

    public static void DeleteReservation() {
        var Reservations = Database.GetAllReservations();
        if (Reservations == null || Reservations.Count == 0) {
            AnsiConsole.MarkupLine("[red]No reservations available to edit.[/]");
            Console.ReadKey();
            return;
        }

        // Prompt the user to select a dish
        string ReservationToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a reservation to [yellow]delete[/]")
                .PageSize(10)
                .MoreChoicesText("[grey]Move up or down to see more reservations[/]")
                .AddChoices(Reservations.Select(r => $"{r.ID} {Database.GetUserByID(r.UserID)?.Email} - {Database.GetLocationByID(r.LocationID)?.Name} {r.Date.ToString()} {r.Timeslot} {(r.GroupSize == 1 ? "1 person" : $"{r.GroupSize} people")}").Append("Quit"))
        );

        if (ReservationToDelete == "Quit") {
            return;
        }

        try {
            Database.DeleteReservationsTable((long)Convert.ToDouble(ReservationToDelete.Split(" ")[0]));
            AnsiConsole.MarkupLine($"[green]Reservation was deleted successfully.[/]");
        }
        catch (Exception ex) {
            AnsiConsole.MarkupLine($"[red]Error deleting reservation: {ex.Message}[/]");
        }
    }

    private static string PromptEmail() {
        AnsiConsole.MarkupLine("[blue]Enter email, or leave empty to cancel:[/]");
        while (true) {
            string Email = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Email:[/]")
                    .PromptStyle("yellow")
                    .Validate(n =>
                    {
                        // Invalid Email length, null or only whitespace
                        if (!Util.EmailValidOrEmpty(n)) {
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
}
