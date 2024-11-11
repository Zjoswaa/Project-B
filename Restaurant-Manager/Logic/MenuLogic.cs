using Spectre.Console;

class MenuLogic
{
    public static void AddDish()
    {
        string dishname = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the dish name? ")
                .Validate(n => {
                    // TO DO valid dishname checker

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

        string price = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the dish price? ")
                .Validate(n => {
                    // TO DO valid price checker

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

        var userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish vegan?:[/]")
            .AddChoices(new[] { "Yes", "No"});
        var input = AnsiConsole.Prompt(userSelectionPrompt);
        bool isVegan = false;
        if (input == "Yes")
        {
            isVegan = true;
        }

        userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Vegetarian?:[/]")
            .AddChoices(new[] { "Yes", "No" });
        input = AnsiConsole.Prompt(userSelectionPrompt);
        bool isVegetarian = false;
        if (input == "Yes")
        {
            isVegetarian = true;
        }

        userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Halal?:[/]")
            .AddChoices(new[] { "Yes", "No" });
        input = AnsiConsole.Prompt(userSelectionPrompt);
        bool isHalal = false;
        if (input == "Yes")
        {
            isHalal = true;
        }

        userSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Gluten Free?:[/]")
            .AddChoices(new[] { "Yes", "No" });
        input = AnsiConsole.Prompt(userSelectionPrompt);
        bool isGlutenFree = false;
        if (input == "Yes")
        {
            isGlutenFree = true;
        }

        // Add the dish to database

        try {
            //Database.InsertUsersTable("test", "a", "first", "last", "USER");
            Database.InsertDishesTable(dishname, price, isVegan, isVegetarian, isHalal, isGlutenFree);
            Console.WriteLine($"{dishname} added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding dish: {ex.Message}");
        }
        Console.ReadKey();
    }
}