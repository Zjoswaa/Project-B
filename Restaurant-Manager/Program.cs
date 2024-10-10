using System.Data.Entity;

class Program
{
    public static void Main()
    {
        Database.CreateTableUsers();
        AccountPresentation acc = new();
        acc.RegisterUI();
    }
}