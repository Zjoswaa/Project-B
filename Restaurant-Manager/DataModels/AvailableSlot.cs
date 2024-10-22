public class AvailableSlot
{
    public long ID { get; } = -1;
    public long LocationID { get; }
    public DateTime Date { get; }
    public string TimeSlot { get; }
    public int AvailableSpace { get; }

    public AvailableSlot(long id, long loc_id, DateTime date, string timeslot, int space)
    {
        ID = id;
        LocationID = loc_id;
        Date = date;
        TimeSlot = timeslot;
        AvailableSpace = space;
    }

    public AvailableSlot(long loc_id, DateTime date, string timeslot, int space)
    {
        ID++;
        LocationID = loc_id;
        Date = date;
        TimeSlot = timeslot;
        AvailableSpace = space;
    }

    public override string ToString()
    {
        return $"ID : {ID}\nLocation ID: {LocationID}\nDate: {Date}\nTimeslot: {TimeSlot}\nAvailable space: {AvailableSpace}";
    }

    public static List<AvailableSlot> CreateSlots(DateTime startDate, DateTime endDate, int amountOfLocations, List<string> timeslots)
    {
        List<AvailableSlot> slots = new();

        //Add method here that empties table

        if (startDate > endDate)
        {
            throw new Exception("The end date cannot be earlier than the start date.");
        }

        DateTime cutStart = startDate.Date;
        DateTime cutEnd = endDate.Date;
        TimeSpan amountOfTime = cutEnd - cutStart;
        int daysBetween = amountOfTime.Days;

        for (int i = 0 ; i < amountOfLocations; i++)
        {
            for (int j = 0; j < (daysBetween - 1); j++)
            {
                for (int k = 0; k < (timeslots.Count()); k++)
                {
                    AvailableSlot slot = new(i, (cutStart.AddDays(j + 1)), timeslots[k], 48);
                    slots.Add(slot);
                }
            }
        }
        return slots;
    }

    public static void FillAvailableSlotsTable(List<AvailableSlot> slots)
    {
        foreach (AvailableSlot slot in slots)
        {
            Database.InsertAvailableSlots(slot.LocationID, slot.Date, slot.TimeSlot, slot.AvailableSpace);
        }

        Database.DeleteOldSlots();
    }
}