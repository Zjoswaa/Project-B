public class Location
{
    public long ID { get; }
    public string Name { get; }
    public static long NextID { get; set; } = 0;

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