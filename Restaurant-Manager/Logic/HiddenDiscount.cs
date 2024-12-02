static class HiddenDiscount
{
    private static List<string> HiddenCodes = new()
        { "A9fL2", "Xb3Kd", "J7qYp", "L5rCt", "Z1NmW"};

    private const int HiddenCodeID = 999999999;

    private static string RandomCodePicker()
    {
        int amountOfCodes = HiddenCodes.Count;
        Random rand = new();
        int randomCodeIndex = rand.Next(0, amountOfCodes);

        return HiddenCodes[randomCodeIndex];
    }

    private static void AddCodeToMenu()
    {
        Database.InsertDishesTable(RandomCodePicker(), "0", true, true, true, true);
    }

    public static void RemoveCodeFromMenu()
    {
        foreach (string HiddenCode in HiddenCodes)
        {
            try
            {
                Database.DeleteDishesTable(HiddenCode);
            }
            catch (Exception ex){}
        }
    }

    private static void AddCodeToReservations()
    {
        DateOnly date = ReservationLogic.ParseDate("9-9-9999");
        Database.InsertReservationsTable(HiddenCodeID, State.LoggedInUser.ID, 0, RandomCodePicker(), date, 0, 0);
    }

    public static void RemoveCodeFromReservations()
    {
        try
        {
            Database.DeleteReservationsTable(HiddenCodeID);
        }
        catch (Exception ex){}
    }

    public static void InsertCodeIntoUI()
    {
        List<Action> HiddenCodeFunctions = new()
            { AddCodeToMenu, AddCodeToReservations};

        int amountOfFunctions = HiddenCodeFunctions.Count;
        Random rand = new();
        int randomCodeIndex = rand.Next(0, amountOfFunctions);

        HiddenCodeFunctions[randomCodeIndex]();
    }
}