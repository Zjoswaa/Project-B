using Spectre.Console;

class MenuLogic
{
    public static void AddDish()
    {
        string DishName = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the dish name? ")
                .PromptStyle("yellow")
                .Validate(n => {
                    // TODO valid dishname checker, check if dish already exists
                    if (Database.DishesTableContainsDish(n)) {
                        return ValidationResult.Error("[red]A dish with this name already exists.[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

        string Price = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the dish price? ")
                .PromptStyle("yellow")
                .Validate(n => {
                    // TO DO valid price checker
                    if (!double.TryParse(n, out double d)) {
                        return ValidationResult.Error("[red]That is not a valid price, a valid example: 9,99[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
            );

        var UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish vegan?[/]")
            .AddChoices(["No", "Yes"]);
        var Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsVegan = false;
        if (Input == "Yes")
        {
            IsVegan = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Vegetarian?[/]")
            .AddChoices(["No", "Yes"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsVegetarian = false;
        if (Input == "Yes")
        {
            IsVegetarian = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Halal?[/]")
            .AddChoices(["No", "Yes"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsHalal = false;
        if (Input == "Yes")
        {
            IsHalal = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[cyan]Is the dish Gluten Free?[/]")
            .AddChoices(["No", "Yes"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsGlutenFree = false;
        if (Input == "Yes")
        {
            IsGlutenFree = true;
        }

        // Add the dish to database

        try {
            Database.InsertDishesTable(DishName, Price, IsVegan, IsVegetarian, IsHalal, IsGlutenFree);
            AnsiConsole.WriteLine($"{DishName} was added successfully.");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error adding dish: {ex.Message}");
        }
        Console.ReadKey();
    }

    public static void DeleteDish() {
        string DishName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the name of the dish to remove, or leave empty to cancel: ")
                .PromptStyle("yellow")
                .Validate(n => {
                    if (string.IsNullOrEmpty(n)) {
                        return ValidationResult.Success();
                    }
                    if (!Database.DishesTableContainsDish(n)) {
                        return ValidationResult.Error("[red]There is no dish with that name[/]");
                    }

                    // If all checks pass
                    return ValidationResult.Success();
                })
                .AllowEmpty()
            );

        if (string.IsNullOrEmpty(DishName)) {
            return;
        }

        try {
            Database.DeleteDishesTable(DishName);
            AnsiConsole.WriteLine($"{DishName} was deleted successfully.");
        } catch (Exception ex) {
            AnsiConsole.WriteLine($"Error deleting dish: {ex.Message}");
        }
        Console.ReadKey();
    }
}
