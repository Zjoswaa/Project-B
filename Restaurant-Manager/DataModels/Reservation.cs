class Reservation
{
    public int ID { get; }
    public int User { get; }
    public int Location { get; }
    public DateTime ReservationTime { get; }
    public int GroupSize { get; }

    public Reservation(int id, int user, int location, DateTime reservationtime, int groupsize)
    {
        ID = id;
        User = user;
        Location = location;
        ReservationTime = reservationtime;
        GroupSize = groupsize;
    }

    public override string ToString()
    {
        return $"Reservation {ID} by User {User}\nLocation ID: {Location}\nTimeslot of reservation: {ReservationTime}\nAmount of people: {GroupSize}";
    }
}