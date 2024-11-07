class ReservationManager
{
    public List<string> LocationNamesToList()
    {
        List<string> locationNames = new(){};
        Dictionary<int, string> locations = Database.GetAllLocations();
        
        foreach (KeyValuePair<int, string> kvp in locations)
        {
            locationNames.Add(kvp.Value);
        }

        return locationNames;
    }

    public List<string> TimeslotsToList()
    {
        List<string> timeslotStrings = new(){};
        List<Timeslot> timeslots = Database.GetAllTimeslots();

        foreach (Timeslot slot in timeslots)
        {
            timeslotStrings.Add(slot.Slot);
        }

        return timeslotStrings;
    }

    public int GetSelectedLocationID(string locName)
    {
        Dictionary<int, string> locations = Database.GetAllLocations();
        int locID = 0;

        foreach (KeyValuePair<int, string> kvp in locations)
        {
            if (locName == kvp.Value)
            {
                locID = kvp.Key;
            }
        }

        return locID;
    }

    public (bool success, string message) CreateReservation(long userID, long locID, string timeslot, DateTime date, int groupsize)
    {
        Dictionary<List<object>, int> reservations = Database.GetAllReservations();

        foreach (KeyValuePair<List<object>, int> kvp in reservations)
        {
            if (kvp.Key.Contains(locID) && kvp.Key.Contains(timeslot) && kvp.Key.Contains(date) && kvp.Key.Contains(userID))
            {
                return (false, "You already have a reservation for this timeslot. Edit your reservation instead.");
            }

            if (kvp.Key.Contains(locID) && kvp.Key.Contains(timeslot) && kvp.Key.Contains(date) && kvp.Value < groupsize)
            {
                return (false, "This timeslot is currently unavailable. Please try again later or pick a different time.");
            }
        }
        Database.InsertReservationsTable(userID, locID, timeslot, date, groupsize);
        return (true, "Your reservation has been made.");
    }
}