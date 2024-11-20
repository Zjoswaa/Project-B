using Spectre.Console;

public class ViewReservations
{
    private readonly string _connectionString;

    // Constructor: Initializes the connection string
    public ViewReservations(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Method to print reservations of the logged-in user
    public static void PrintUserReservations()
    {
        var reservations = Database.GetAllReservations();
        var userId = State.LoggedInUser.ID;

        var table = new Table().Centered();

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
                Update(200, () => table.AddColumn("Table"));
                Update(200, () => table.AddColumn("Date"));
                Update(200, () => table.AddColumn("Time"));
                Update(200, () => table.AddColumn("Guests"));

                // Rows
                foreach (var reservation in reservations)
                {
                    if (reservation.UserID == userId)
                    {
                        Update(100, () => table.AddRow(
                            reservation.Table.ToString(),
                            reservation.Date.ToString("dd-MM-yyyy"),
                            reservation.Timeslot,
                            reservation.GroupSize.ToString()
                        ));
                    }
                }
            });

        // Display the prompt after the table animation completes
        AnsiConsole.MarkupLine("[grey]Press any key to return...[/]");
        Console.ReadKey();
    }
}
