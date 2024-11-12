public class Reservation
{
    public long ID { get; }
    public long UserID { get; }
    public long LocationID { get; }
    public string Timeslot { get; }
    public DateTime ReservationTime { get; }
    public int GroupSize { get; }
    public int Table { get; }

    public Reservation(long id, long user, long location, string timeslot, DateTime reservationtime, int groupsize, int table)
    {
        ID = id;
        UserID= user;
        LocationID = location;
        Timeslot = timeslot;
        ReservationTime = reservationtime;
        GroupSize = groupsize;
        Table = table;
    }

    public override string ToString()
    {
        return $"Reservation {ID} by User {UserID}\nLocation ID: {LocationID}\nTimeslot of reservation: {ReservationTime}\nAmount of people: {GroupSize}\nTable: {Table}";
    }
}