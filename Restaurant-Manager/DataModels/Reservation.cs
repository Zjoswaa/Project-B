public class Reservation
{
    public long ID { get; }
    public long UserID { get; }
    public long LocationID { get; }
    public string Timeslot { get; }
    public DateOnly Date { get; }
    public int GroupSize { get; }
    public int Table { get; }

    public Reservation(long id, long user, long location, string timeslot, DateOnly date, int groupsize, int table)
    {
        ID = id;
        UserID= user;
        LocationID = location;
        Timeslot = timeslot;
        Date = date;
        GroupSize = groupsize;
        Table = table;
    }

    public override string ToString()
    {
        return $"Reservation {ID} by User {UserID}\nLocation ID: {LocationID}\nTimeslot of reservation: {Date}\nAmount of people: {GroupSize}\nTable: {Table}";
    }
}