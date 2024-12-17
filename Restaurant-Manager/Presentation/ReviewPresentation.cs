using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;
static class ReviewPresentation
{
    public static void Present()
    {
        Console.Clear();
        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please select an option:[/]")
            .AddChoices(new[]
            {
                    "Leave a Review", "View Reviews", "Back"
            });

        string userSelection = AnsiConsole.Prompt(userSelectionPrompt);

        switch (userSelection)
        {
            case "Back":
                Console.Clear();
                return;
            case "Leave a Review":
                Console.Clear();
                LeaveReview();
                break;
            case "View Reviews":
                break;
        }
    }
    
    private static void LeaveReview()
    {
        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Please choose a rating:[/]")
            .AddChoices(new[]
            {
                    "1", "2", "3", "4", "5", "Back"
            });

        string rating = AnsiConsole.Prompt(userSelectionPrompt);

        if (rating == "Back")
            Present();

        string review = AnsiConsole.Prompt(
            new TextPrompt<string>("Write a Review [grey](optional)[/]:"));

        try
        {
            Database.InsertReviewsTable(State.LoggedInUser.ID, Int32.Parse(rating), review, DateOnly.FromDateTime(DateTime.Now));
            AnsiConsole.MarkupLine("[green]Your review has been added![/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}