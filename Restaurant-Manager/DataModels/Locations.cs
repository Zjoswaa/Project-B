class Locations
{
    public int ID { get; }
    public string Name { get; }

    public Locations(int id, string name)
    {
        ID = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Location {ID}: {Name}";
    }
}