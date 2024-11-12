using System.Globalization;

class ReservationManager
{
    public (bool success, string message) CreateReservation(long userID, long locID, string timeslot, DateTime date, int groupsize)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        int totalPeople = 0;

        foreach (Reservation reservation in reservations)
        {
            if (reservation.UserID == userID && reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.ReservationTime == date)
            {
                return (false, "You already have a reservation for this timeslot. Edit your reservation instead.");
            }

            if (!CheckMaxReservations(locID, timeslot, date, groupsize))
            {
                return (false, "This timeslot is currently unavailable. Please try again later or pick a different time.");
            }
        }
        Database.InsertReservationsTable(userID, locID, timeslot, date, groupsize);
        return (true, "Your reservation has been made.");
    }

    public bool CheckMaxReservations(long locID, string timeslot, DateTime date, int groupsize)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        int totalPeople = 0;

        foreach (Reservation reservation in reservations)
        {
            if (reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.ReservationTime == date)
            {
                totalPeople += reservation.GroupSize;
            }
        }

        if ((totalPeople + groupsize) > 48)
        {
            return false;
        }
        return true;
    }

    public DateTime ParseDate(string dateString)
    {
        DateTime date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        return date;
    }

    public (bool success, string message) VerifyDate(DateTime date)
    {
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddDays(180);

        if (date > endDate)
        {
            return (false, "Reservations can only be made six months in advance. Please try again later or pick a different date.");
        }

        if (date < startDate)
        {
            return (false, "The date you have selected has already passed. Please pick a different date.");
        }

        return (true, null);
    }

    public List<string> LocationNamesToList()
    {
        List<string> locationNames = new(){};
        Dictionary<int, string> locations = Database.GetAllLocations();
        
        foreach (KeyValuePair<int, string> kvp in locations)
        {
            locationNames.Add(kvp.Value);
        }
        //Adds another option used for exiting the menu in ReservationPresentation.cs
        locationNames.Add("Exit Reservation");

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
        //Adds another option used for exiting the menu in ReservationPresentation.cs
        timeslotStrings.Add("Exit Reservation");

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
}