using Spectre.Console;

static class ReservationManagement {
    public static void ViewAllReservations() {
        Console.Clear();
        AnsiConsole.Write(new Rule($" [yellow]View Reservations[/] "));
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

        // Create the layout
        Console.CursorVisible = false;
        string Buffer = "";
        Panel Panel = new(new Text(Buffer).Centered());
        Panel.Header = new PanelHeader(" [grey]Type email, escape to exit, enter to search[/] ").Centered();
        Panel.Expand = true;
        var layout = new Layout("Root")
            .SplitRows(
                new Layout("Top"),
                new Layout("Bottom"));

        layout["Top"].Size(3);
        layout["Top"].Update(Panel);
        
        layout["Bottom"].Update(
            new Panel(
                    Align.Center(
                        new Markup(""),
                        VerticalAlignment.Middle))
                .Expand());
        
        AnsiConsole.Write(layout);
        
        while (true) {
            ConsoleKeyInfo Input = Console.ReadKey();
            if (Input.Key == ConsoleKey.Escape) {
                return;
            }
            if (Input.Key == ConsoleKey.Backspace) {
                if (!String.IsNullOrEmpty(Buffer)) {
                    Buffer = Buffer.Remove(Buffer.Length - 1);
                }
            } else if (Input.Key == ConsoleKey.Enter) {
                break;
            }
            else {
                Buffer += Input.KeyChar;
            }
            
            Panel = new(new Text(Buffer).Centered());
            Panel.Header = new PanelHeader(" [grey]Type email, escape to exit, enter to search[/] ").Centered();
            Panel.Expand = true;
            AnsiConsole.Clear();

            layout["Top"].Update(Panel);

            layout["Bottom"].Update(
                new Panel(
                        Align.Center(
                            new Markup(""),
                            VerticalAlignment.Middle))
                    .Expand());

            AnsiConsole.Write(layout);
        }

        Panel = new(new Text(Buffer).Centered());
        Panel.Header = new PanelHeader(" [grey]Email[/] ").Centered();
        Panel.Expand = true;
        AnsiConsole.Clear();
        layout["Top"].Update(Panel);

        Table Table = new Table().Centered();
        Table.AddColumn("Reservation ID");
        Table.AddColumn("User"); // ID and Email
        Table.AddColumn("Location"); // ID and Name
        Table.AddColumn("Time");
        Table.AddColumn("Date");
        Table.AddColumn("Group Size");
        Table.AddColumn("Table");
        foreach (Reservation res in Database.GetAllReservations()) {
            User? User = Database.GetUserByEmail(Buffer);
            if (res.UserID == User?.ID) {
                Location? Location = Database.GetLocationByID(res.LocationID);
                Table.AddRow($"{res.ID}", $"({res.UserID}) {User?.Email}", $"({res.LocationID}) {Location?.Name}", $"{res.Timeslot}", $"{res.Date.ToString()}", $"{res.GroupSize}", $"{res.Table}");
            }
        }
        layout["Bottom"].Update(Table);

        AnsiConsole.Write(layout);
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
