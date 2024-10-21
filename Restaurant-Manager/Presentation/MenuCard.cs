using Spectre.Console;

//class MenuCard
//{
//    public static void DisplayMenuCard()
//    {
//        var table = new Table()
//        .Border(TableBorder.Square)
//        .AddColumn("[yellow]ID[/]")
//        .AddColumn("[yellow]Name[/]")
//        .AddColumn("[yellow]Price[/]")
//        .AddColumn("[yellow]Vegan[/]")
//        .AddColumn("[yellow]Veggie[/]")
//        .AddColumn("[yellow]Halal[/]")
//        .AddColumn("[yellow]Gluten Free[/]");

//        var panel = new Panel(table)
//            .Header("[bold red] Menu Card[/]")
//            .Expand()
//            .SquareBorder();

//        AnsiConsole.Write(panel);
//    }
//}

// THIS IS A WORK IN PROGRESS :3

class MenuCard
{
    public static void DisplayMenuCard()
    {
        var dishes = Database.GetAllDishes();
        Console.WriteLine("\x1b[92mID   Name                  Price   Vegan   Vegetarian   Halal   Gluten Free");
        Console.WriteLine("---------------------------------------------------------------------------\x1b[37m");
        foreach (var dish in dishes)
        {
            Console.WriteLine($"\x1b[31m{dish.ID,-4}\x1b[37m {dish.Name,-20} {dish.Price,-7:C} {dish.BoolToText(dish.IsVegan),-7} {dish.BoolToText(dish.IsVegetarian),-11} {dish.BoolToText(dish.IsHalal),-6} {dish.BoolToText(dish.IsGlutenFree),-10}");
        }
    }
}

// TESTING SOME COOL FORMATTING RN WIP