using Spectre.Console;


class UserMenu
{
    public static void ShowUserMenu()
    {
        HiddenDiscount.RemoveCodeFromMenu();
        HiddenDiscount.RemoveCodeFromReservations();
        HiddenDiscount.ChangeMenuHeadToFalse();
        HiddenDiscount.InsertCodeIntoUI();
        
        while (State.LoggedInUser is not null)
        {
            Console.Clear();
            if (HiddenDiscount.ChangeMenuHead)
            {
                AnsiConsole.Write(new Rule($"[maroon]Reservation Menu ({HiddenDiscount.RandomCodePicker()}) ({State.LoggedInUser.GetFullName()})[/]"));
            }
            else AnsiConsole.Write(new Rule($"[maroon]Reservation Menu ({State.LoggedInUser.GetFullName()})[/]"));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[gray]A 10% off discount code is hidden somewhere in the main menu...[/]\n[cyan]Please select an option:[/]")
                .AddChoices(new[]
                {
                    "Make a Reservation", "View Reservations", "Edit Reservation", "Remove Reservation", "View Menu", "About Us",
                    "Logout"
                });
            
            var userSelection = AnsiConsole.Prompt(userSelectionPrompt);

            switch (userSelection)
            {
                case "Make a Reservation":
                    Console.Clear();
                    ReservationPresentation.Present();
                    break;
                case "View Reservations":
                    Console.Clear();
                    ViewReservations.PrintUserReservations();
                    break;
                case "Edit Reservation":
                    Console.Clear();
                    EditReservationPresentation.Present();
                    break;
                case "Remove Reservation":
                    //Console.WriteLine("TBA...");
                    //Thread.Sleep(1000);
                    Console.Clear();
                    RemoveReservationPresentation.Present();
                    break;
                case "View Menu":
                    //Console.WriteLine("TBA...");
                    //Thread.Sleep(1000);
                    //Console.Clear();
                    MenuCard.DisplayMenuCard();
                    AnsiConsole.MarkupLine("[grey]Press any key to return[/]");
                    Console.ReadKey();
                    break;
                case "About Us":
                    Console.Clear();
                    AboutUsPresentation.DisplayAboutUs();
                    break;
                case "Logout":
                    AnsiConsole.MarkupLine("[red]Logging out...[/]");
                    State.LoggedInUser = null;
                    Thread.Sleep(1000);
                    Console.Clear();
                    MainMenuPresentation.ShowMainMenu();
                    break;
            }
        }
    }
}