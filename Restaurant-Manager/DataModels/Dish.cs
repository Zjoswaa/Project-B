class Dish
{
    public long ID { get; }
    public string Name { get; }
    public double Price { get; }
    public bool IsVegan { get; }
    public bool IsVegetarian { get; }
    public bool IsHalal { get; }
    public bool IsGlutenFree { get; }

    public Dish(long id, string name, double price, bool isvegan, bool isvegetarian, bool ishalal, bool isglutenfree)
    {
        ID = id;
        Name = name;
        Price = price;
        IsVegan = isvegan;
        IsVegetarian = isvegetarian;
        IsHalal = ishalal;
        IsGlutenFree = isglutenfree;
    }

    public string BoolToText(bool status)
    {
        if (status)
        {
            return "Yes";
        }
        return "No";
    }

    public override string ToString()
    {
        return $"ID: {ID}\nDish Name: {Name}\nDish Price: {Price}\nVegan: {BoolToText(IsVegan)}\nVegetarian: {BoolToText(IsVegetarian)}\nHalal: {BoolToText(IsHalal)}\nGluten free: {BoolToText(IsGlutenFree)}";
    }
}