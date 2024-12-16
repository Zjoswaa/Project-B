using Spectre.Console;

static class ReservationManagement {
    public static void ViewAllReservations() {
        Console.Clear();
        AnsiConsole.Write(new Rule($" [maroon]View Reservations[/] "));
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

        if (ReservationToDelete == "Quit" || !ConfirmDeletion()) {
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

    public static void CreateReservation() {
        string Email = PromptEmail();
        if (Email == "Quit") {
            return;
        }

        string LocationName = PromptLocation();
        if (LocationName == "Quit") {
            return;
        }
        long LocationID = Database.GetLocationByName(LocationName)!.ID;

        (int Day, int Month, int Year) Date = SelectDate();
        if (Date is (0, 0, 0)) {
            return;
        }

        string Timeslot = PromptTimeslot();
        if (Timeslot == "Quit") {
            return;
        }

        string GroupSize = PromptGroupSize();
        if (GroupSize == "Quit") {
            return;
        }

        string Table = PromptTable($"{Date.Day}-{Date.Month}-{Date.Year}", Timeslot);
        if (Table == "Quit") {
            return;
        }

        try {
            Database.InsertReservationsTable(null, Database.GetUserByEmail(Email).ID, LocationID, Timeslot,
                DateOnly.ParseExact($"{Date.Day}-{Date.Month}-{Date.Year}", "d-M-yyyy"), Int32.Parse(GroupSize),
                Int32.Parse(Table));
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Reservation created[/]");
            Console.ReadKey();
        }
        catch (Exception e) {
            AnsiConsole.WriteLine(e.Message);
        }
    }

    public static void EditReservation() {
        List<Reservation> Reservations = Database.GetAllReservations();

        var reservationChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]Select a reservation to edit:[/]")
                .AddChoices(ReservationLogic.ReservationsToString(Reservations)));

        if (reservationChoice == "Exit") {
            return;
        }

        long reservationID = ReservationLogic.ParseIDFromString(reservationChoice);
        Reservation SelectedReservation = Database.GetAllReservations().Where(r => r.ID == reservationID).ToList()[0];

        List<string> DataToChange = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("What data would you like to edit?")
                .NotRequired()
                .InstructionsText(
                    "[gray](Press [blue]space[/] to select what to edit, " +
                    "[green]Enter[/] to accept)[/]")
                .AddChoices(new[] { "User", "Location", "Date", "Group Size", "Table" }));

        if (DataToChange.Contains("User")) {
            string Email = PromptEmail();
            if (Email == "Quit") {
                return;
            }
            SelectedReservation.UserID = Database.GetUserByEmail(Email)!.ID;
        }
        if (DataToChange.Contains("Location")) {
            string Location = PromptLocation();
            if (Location == "Quit") {
                return;
            }
            SelectedReservation.LocationID = Database.GetLocationByName(PromptLocation())!.ID;
        }
        if (DataToChange.Contains("Date")) {
            (int Day, int Month, int Year) Date = SelectDate();
            if (Date == (0, 0, 0)) {
                return;
            }
            SelectedReservation.Date = DateOnly.ParseExact($"{Date.Day}-{Date.Month}-{Date.Year}", "d-M-yyyy");
        }
        if (DataToChange.Contains("Group Size")) {
            string GroupSize = PromptGroupSize();
            if (GroupSize == "Quit") {
                return;
            }
            SelectedReservation.GroupSize = Int32.Parse(GroupSize);
        }
        if (DataToChange.Contains("Table")) {
            string Table = PromptTable(SelectedReservation.Date?.ToString("dd-MM-yyyy"), SelectedReservation.Timeslot, $"{SelectedReservation.Table}");
            if (Table == "Quit") {
                return;
            }
        }
        try {
            if (DataToChange.Count != 0) {
                Database.UpdateReservation(SelectedReservation);
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Reservation updated[/]");
                Console.ReadKey();
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }

    private static bool ConfirmDeletion() {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you really want to remove this reservation?")
                .AddChoices("Yes", "No"));

        if (choice == "No") {
            return false;
        }
        return true;
    }

    private static string PromptEmail() {
        List<User> Users = Database.GetAllUsers();
        List<string> Emails = Users.Select(user => user.Email).ToList();
        Emails.Add("Quit");
        var Email = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]For what user is this reservation?[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to see more emails)[/]")
                .AddChoices(Emails));

        return Email;
    }

    private static string PromptLocation() {
        List<Location> Locations = Database.GetAllLocations();
        List<string> Names = Locations.Select(loc => loc.Name).ToList();
        Names.Add("Quit");
        var LocationName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]At what location is this reservation?[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to see more locations)[/]")
                .AddChoices(Names));

        return LocationName;
    }

    private static (int Day, int Month, int Year) SelectDate() {
        DateTime date = DateTime.MinValue;
        int SelectedDay = DateTime.Now.Day;
        int SelectedMonth = DateTime.Now.Month;
        int SelectedYear = DateTime.Now.Year;
        var Calendar = new Spectre.Console.Calendar(SelectedYear, SelectedMonth);

        while (true) {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[cyan]Select a date for the reservation:\n[/]");
            Calendar.CalendarEvents.Clear();
            Calendar.Year = SelectedYear;
            Calendar.Month = SelectedMonth;
            Calendar.AddCalendarEvent(SelectedYear, SelectedMonth, SelectedDay);
            Calendar.HighlightStyle(Style.Parse("yellow bold"));
            Console.CursorVisible = false;
            AnsiConsole.Write(Calendar);
            AnsiConsole.Markup(
                "[gray]Left/Right arrow to change day. Up/Down arrow to change month. Enter to confirm. Escape to cancel.[/]");

            ConsoleKeyInfo KeyInfo = Console.ReadKey(intercept: true);

            if (KeyInfo.Key == ConsoleKey.RightArrow) {
                ReservationLogic.IncreaseDateByDay(ref SelectedDay, ref SelectedMonth, ref SelectedYear);
            }
            else if (KeyInfo.Key == ConsoleKey.LeftArrow) {
                ReservationLogic.DecreaseDateByDay(ref SelectedDay, ref SelectedMonth, ref SelectedYear);
            }
            else if (KeyInfo.Key == ConsoleKey.UpArrow) {
                ReservationLogic.IncreaseDateByMonth(ref SelectedDay, ref SelectedMonth, ref SelectedYear);
            }
            else if (KeyInfo.Key == ConsoleKey.DownArrow) {
                ReservationLogic.DecreaseDateByMonth(ref SelectedDay, ref SelectedMonth, ref SelectedYear);
            }
            else if (KeyInfo.Key == ConsoleKey.Enter) {
                Console.CursorVisible = true;
                return (SelectedDay, SelectedMonth, SelectedYear);
            }
            else if (KeyInfo.Key == ConsoleKey.Escape) {
                Console.CursorVisible = true;
                return (0, 0, 0);
            }
        }
    }

    private static string PromptTimeslot() {
        Console.Clear();
        List<string> Options = ReservationLogic.TimeslotsToList();
        Options.Add("Quit");
        
        var timeslotChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]Select a timeslot for the reservation:[/]")
                .AddChoices(Options));

        return timeslotChoice;
    }

    private static string PromptGroupSize() {
        Console.Clear();

        var GroupSize = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]What is the group size for the reservation:[/]")
                .AddChoices(["1", "2", "3", "4", "5", "6", "Quit"]));

        return GroupSize;
    }

    private static string PromptTable(string Date, string Timeslot) {
        Console.Clear();
        List<string> Choices = Database.GetAvailableTables(Date, Timeslot).Select(number => number.ToString()).ToList();
        if (Choices.Count == 0) {
            AnsiConsole.MarkupLine("[red]No tables are available at this date and time[/]");
            Console.ReadKey();
            return "Quit";
        }
        Choices.Add("Quit");

        var TableNumber = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]What is the table for the reservation:[/]")
                .AddChoices(Choices));

        return TableNumber;
    }

    private static string PromptTable(string Date, string Timeslot, string Current) {
        Console.Clear();
        List<string> Choices = Database.GetAvailableTables(Date, Timeslot).Select(number => number.ToString()).ToList();
        if (!Choices.Contains(Current)) {
            Choices.Add(Current);
            Choices.Sort();
        }
        Choices.Add("Quit");

        var TableNumber = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]What is the table for the reservation:[/]")
                .AddChoices(Choices));

        return TableNumber;
    }
}
