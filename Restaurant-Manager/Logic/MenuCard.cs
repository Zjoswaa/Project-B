//using Spectre.Console;
//using System;

//class MenuCard
//{
//    private List<Dish> Dishes;
    
//    public MenuCard()
//    {
//        Dishes = new List<Dish>();
//    }

//    public void AddDish(Dish dish)
//    {
//        Dishes.Add(dish);
//    }

//    public void DisplayMenuCard()
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

// fix layer model stuff 

// TO BE DELETED!!!!!