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
            .Title("[yellow]Is the dish vegan?[/]")
            .AddChoices(["Yes", "No"]);
        var Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsVegan = false;
        if (Input == "Yes")
        {
            IsVegan = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[yellow]Is the dish Vegetarian?[/]")
            .AddChoices(["Yes", "No"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsVegetarian = false;
        if (Input == "Yes")
        {
            IsVegetarian = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[yellow]Is the dish Halal?[/]")
            .AddChoices(["Yes", "No"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool IsHalal = false;
        if (Input == "Yes")
        {
            IsHalal = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
            .Title("[yellow]Is the dish Gluten Free?[/]")
            .AddChoices(["Yes", "No"]);
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

    public static void DeleteDish() // NEEDS A LITTLE CHANGING SO IT HAS THE SELECTION MENU LIKE EditDish();
    {
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

    public static void EditDish()
    {
        // First get all the dishes from the database
        var dishes = Database.GetAllDishes();
        if (dishes == null || dishes.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No Dishes available to edit.[/]");
            Console.ReadKey();
            return;
        }

        // Prompt the user to select a dish
        var DishToEdit = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a dish to [yellow]edit[/]")
            .PageSize(10)
            .MoreChoicesText("[grey]Move up or down to see more dishes[/]")
            .AddChoices(dishes.Select(d => d.Name))
            );

        // Get the selected dish's details and prompt for new details after
        var dish = dishes.First(d => d.Name == DishToEdit);

        string NewDishName = AnsiConsole.Prompt(
            new TextPrompt<string>($"New dish name (leave empty to keep '{dish.Name}'): ")
            .PromptStyle("yellow")
            .AllowEmpty()
            );

        string NewPrice = AnsiConsole.Prompt(
            new TextPrompt<string>($"New price (leave empty to keep {dish.Price:C}): ")
            .PromptStyle("yellow")
            .AllowEmpty()
            .Validate(n =>
            {
                if (string.IsNullOrEmpty(n)) return ValidationResult.Success();
                if (!double.TryParse(n, out _)) return ValidationResult.Error("[red]Invalid price format.[/]");
                return ValidationResult.Success();
            })
        );

        //bool isVegan = AnsiConsole.Confirm($"Is the dish vegan? (current: {(dish.IsVegan ? "Yes" : "No")})");
        //bool isVegetarian = AnsiConsole.Confirm($"Is the dish vegetarian? (current: {(dish.IsVegetarian ? "Yes" : "No")})");
        //bool isHalal = AnsiConsole.Confirm($"Is the dish halal? (current: {(dish.IsHalal ? "Yes" : "No")})");
        //bool isGlutenFree = AnsiConsole.Confirm($"Is the dish gluten-free? (current: {(dish.IsGlutenFree ? "Yes" : "No")})");
        var UserSelectionPrompt = new SelectionPrompt<string>()
        .Title("[yellow]Is the dish vegan?[/]")
        .AddChoices(["Yes", "No"]);
        var Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool isVegan = false;
        if (Input == "Yes")
        {
            isVegan = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
        .Title("[yellow]Is the dish Vegetarian?[/]")
        .AddChoices(["Yes", "No"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool isVegetarian = false;
        if (Input == "Yes")
        {
            isVegetarian = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
        .Title("[yellow]Is the dish Halal?[/]")
        .AddChoices(["Yes", "No"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool isHalal = false;
        if (Input == "Yes")
        {
            isHalal = true;
        }

        UserSelectionPrompt = new SelectionPrompt<string>()
        .Title("[yellow]Is the dish Gluten Free?[/]")
        .AddChoices(["Yes", "No"]);
        Input = AnsiConsole.Prompt(UserSelectionPrompt);
        bool isGlutenFree = false;
        if (Input == "Yes")
        {
            isGlutenFree = true;
        }

        // If strings left empty, keep the old values
        string FinalDishName = string.IsNullOrEmpty(NewDishName) ? dish.Name : NewDishName;
        double FinalPrice = string.IsNullOrEmpty(NewPrice) ? dish.Price : double.Parse(NewPrice);

        // Lastly, update the dish in the database
        try
        {
            Database.UpdateDishesTable(dish.ID, FinalDishName, FinalPrice, isVegan, isVegetarian, isHalal, isGlutenFree);
            AnsiConsole.MarkupLine($"[green]{FinalDishName} was updated successfully.[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error updating dish: {ex.Message}[/]");
        }
        Console.ReadKey();
    }
}
