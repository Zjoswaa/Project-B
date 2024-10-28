// MENU LOGIC

class MenuLogic
{
    public static void AddDish()
    {
        Console.WriteLine("Enter the dish name:");
        string dishname = Console.ReadLine();

        double price;
        while (true)
        {
            Console.WriteLine("Enter the dish price:");
            string dishprice = Console.ReadLine();

            if (double.TryParse(dishprice, out price))
            {
                break; // if price is valid, break out the loop
            }
            else
            {
                Console.WriteLine("Invalid price. Please enter a valid number.");
            }
        }

        Console.WriteLine("Is the dish vegan? (yes/no)");
        string input = Console.ReadLine().Trim().ToLower();
        bool isVegan = false;
        if (input == "yes")
        {
            isVegan = true;
        }
        else if (input != "no")
        {
            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
        }
    }
}