class Location
{
    public int ID { get; }
    public string Name { get; }

    public Location(int id, string name)
    {
        ID = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Location {ID}: {Name}";
    }
}