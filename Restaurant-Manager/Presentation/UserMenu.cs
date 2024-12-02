﻿using Spectre.Console;


class UserMenu
{
    public static void ShowUserMenu()
    {
        while (State.LoggedInUser is not null)
        {
            HiddenDiscount.InsertCodeIntoUI();
            Console.Clear();

            AnsiConsole.Write(new Rule($"[yellow]Reservation Menu ({State.LoggedInUser.GetFullName()})[/]"));

            var userSelectionPrompt = new SelectionPrompt<string>()
                .Title("[cyan]Please select an option:[/]")
                .AddChoices(new[]
                {
                    "Make a Reservation", "View Reservations", "Edit Reservation", "Remove Reservation", "View Menu",
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
                case "Logout":
                    AnsiConsole.MarkupLine("[red]Logging out...[/]");
                    State.LoggedInUser = null;
                    Thread.Sleep(1000);
                    Console.Clear();
                    HiddenDiscount.RemoveCodeFromMenu();
                    HiddenDiscount.RemoveCodeFromReservations();
                    MainMenuPresentation.ShowMainMenu();
                    break;
            }
        }
    }
}