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
        AnsiConsole.Write(new Rule($"[yellow]View Reservations[/]"));
        var table = new Table().Centered();
        
        string Email = PromptEmail();
        if (String.IsNullOrEmpty(Email)) {
            return;
        }

        List<Reservation> Reservations = Database.GetReservationsByEmail(Email);

        // Animate
        AnsiConsole.Clear();
        AnsiConsole.Live(table)
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Top)
            .Start(ctx =>
            {
                void Update(int delay, Action action) {
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
                    Update(100,
                        () => table.AddRow($"{res.ID}", $"({res.UserID}) {User?.Email}",
                            $"({res.LocationID}) {Location?.Name}", $"{res.Timeslot}", res.Date.ToString(),
                            $"{res.GroupSize}", $"{res.Table}"));
                }
            });
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
