using System.Globalization;

class ReservationManager
{
    public (bool success, string message) CreateReservation(long userID, long locID, string timeslot, DateTime date, int groupsize, int table)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        int totalPeople = 0;

        foreach (Reservation reservation in reservations)
        {
            if (reservation.UserID == userID && reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.ReservationTime == date)
            {
                return (false, "You already have a reservation for this timeslot. Edit your reservation instead.");
            }

            if (!CheckReservationLimit(locID, timeslot, date, 8))
            {
                return (false, "This timeslot is currently unavailable. Please try again later or pick a different time.");
            }
        }
        Database.InsertReservationsTable(userID, locID, timeslot, date, groupsize, table);
        return (true, "Your reservation has been made.");
    }

    public bool CheckReservationLimit(long locID, string timeslot, DateTime date, int maxTables)
    {
        List<Reservation> reservations = Database.GetAllReservations();

        foreach (Reservation reservation in reservations)
        {
            if (reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.ReservationTime == date && maxTables == reservation.Table)
            {
                return false;
            }
        }
        return true;
    }

    public int AssignedTable(long locID, string timeslot, DateTime date)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        int tableCount = 1;

        foreach (Reservation reservation in reservations)
        {
            if (reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.ReservationTime == date)
            {
                tableCount += 1;
            }
        }

        return tableCount;
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
        List<Location> locations = Database.GetAllLocations();
        
        foreach (Location location in locations)
        {
            locationNames.Add(location.Name);
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

    public long GetSelectedLocationID(string locName)
    {
        List<Location> locations = Database.GetAllLocations();
        long locID = 0;

        foreach (Location location in locations)
        {
            if (locName == location.Name)
            {
                locID = location.ID;
            }
        }

        return locID;
    }

    public string GetLocationDescription(long ID)
    {
        List<Location> locations = Database.GetAllLocations();

        foreach (Location location in locations)
        {
            if (location.ID == ID)
            {
                return location.Message;
            }
        }
        return "unknown";
    }
}