public class Location
{
    public long ID { get; }
    public string Name { get; }
    public string Message { get; }
    public static long NextID { get; set; } = 0;

    public Location(int id, string name, string message)
    {
        ID = id;
        Name = name;
        Message = message;
    }

    public override string ToString()
    {
        return $"Location {ID}: {Name}\nInstructions: {Message}";
    }
}