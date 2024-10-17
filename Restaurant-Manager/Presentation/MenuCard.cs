using Spectre.Console;

class MenuCard
{
    public void DisplayMenuCard()
    {
        var table = new Table()
        .Border(TableBorder.Square)
        .AddColumn("[yellow]ID[/]")
        .AddColumn("[yellow]Name[/]")
        .AddColumn("[yellow]Price[/]")
        .AddColumn("[yellow]Vegan[/]")
        .AddColumn("[yellow]Veggie[/]")
        .AddColumn("[yellow]Halal[/]")
        .AddColumn("[yellow]Gluten Free[/]");

        var panel = new Panel(table)
            .Header("[bold red] Menu Card[/]")
            .Expand()
            .SquareBorder();

        AnsiConsole.Write(panel);
    }
}

// THIS IS A WORK IN PROGRESS :3