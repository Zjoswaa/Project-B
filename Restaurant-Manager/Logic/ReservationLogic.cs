using System.Globalization;
using System.Text.RegularExpressions;

static class ReservationLogic
{
    public static (bool success, string message) CreateReservation(long userID, long locID, string timeslot, DateOnly date, int groupsize, int table)
    {
        List<Reservation> reservations = Database.GetAllReservations();

        foreach (Reservation reservation in reservations)
        {
            if (IsSameReservation(reservation, userID, locID, timeslot, date))
            {
                return (false, "You already have a reservation for this timeslot. Edit your reservation instead.");
            }

            if (!HasAvailableTable(locID, timeslot, date, 8))
            {
                return (false, "This timeslot is currently unavailable. Please try again later or pick a different time.");
            }
        }
        Database.InsertReservationsTable(userID, locID, timeslot, date, groupsize, table);
        return (true, "Your reservation has been made.");
    }

    public static bool HasAvailableTable(long locID, string timeslot, DateOnly date, int maxTables)
    {
        List<Reservation> reservations = Database.GetAllReservations();

        foreach (Reservation reservation in reservations)
        {
            if (IsUnvailableTimeslot(reservation, locID, timeslot, date, maxTables))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsSameReservation(Reservation reservation, long userID, long locID, string timeslot, DateOnly date)
    {
        return reservation.UserID == userID &&
               reservation.LocationID == locID &&
               reservation.Timeslot == timeslot &&
               reservation.Date == date;
    }

    private static bool IsUnvailableTimeslot(Reservation reservation, long locID, string timeslot, DateOnly date, int maxTables)
    {
        return reservation.LocationID == locID &&
               reservation.Timeslot == timeslot &&
               reservation.Date == date &&
               maxTables == reservation.Table;
    }

    public static int GetTableCount(long locID, string timeslot, DateOnly date)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        int tableCount = 1;

        foreach (Reservation reservation in reservations)
        {
            if (reservation.LocationID == locID && reservation.Timeslot == timeslot && reservation.Date == date)
            {
                tableCount += 1;
            }
        }

        return tableCount;
    }

    public static DateOnly ParseDate(string dateString)
    {
        DateOnly date = DateOnly.ParseExact(dateString, "dd-MM-yyyy");
        return date;
    }

    public static (bool success, string message) VerifyDate(DateTime date)
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

    public static List<string> LocationNamesToList()
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

    public static List<string> TimeslotsToList()
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

    public static long GetLocationIDByName(string locName)
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

    public static string GetLocationDescription(long ID)
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

    public static List<Reservation> GetReservationsByUserID(long userID)
    {
        List<Reservation> userReservations = new(){};
        List<Reservation> reservations = Database.GetAllReservations();

        foreach (Reservation reservation in reservations)
        {
            if (reservation.UserID == userID)
            {
                userReservations.Add(reservation);
            }
        }

        return userReservations;
    }

    public static Reservation GetReservationByID(long ID)
    {
        List<Reservation> reservations = Database.GetAllReservations();
        
        foreach (Reservation reservation in reservations)
        {
            if (reservation.ID == ID)
            {
                return reservation;
            }
        }

        return null;
    }

    public static List<string> ReservationsToString(List<Reservation> reservations)
    {
        List<string> reservationStrings = new(){};

        foreach (Reservation reservation in reservations)
        {
            reservationStrings.Add($"ID: {reservation.ID} Date: {reservation.Date}, Timeslot: {reservation.Timeslot}, Group size: {reservation.GroupSize}");
        }

        return reservationStrings;
    }

    public static long ParseIDFromString(string reservationInfo)
    {
        Match match = Regex.Match(reservationInfo, @"ID:\s*(\d+)");
        if (match.Success)
        {
            long id = int.Parse(match.Groups[1].Value);
            return id;
        }
        else return -1;
    }

    public static void IncreaseDateByDay(ref int Day, ref int Month, ref int Year) {
        Dictionary<int, int> MonthToDays;
        if (DateTime.IsLeapYear(Year)) {
            MonthToDays = new() { { 1, 31 }, { 2, 29 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        } else {
            MonthToDays = new() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        }
        
        if (Day < MonthToDays[Month]) {
            Day++;
        } else if ( Day == MonthToDays[Month]) {
            if (Month == 12) {
                Day = 1;
                Month = 1;
                Year++;
            } else {
                Day = 1;
                Month++;
            }
        }
    }

    public static void DecreaseDateByDay(ref int Day, ref int Month, ref int Year) {
        if (Day == DateTime.Now.Day && Month == DateTime.Now.Month && Year == DateTime.Now.Year) {
            return;
        }

        Dictionary<int, int> MonthToDays;
        if (DateTime.IsLeapYear(Year)) {
            MonthToDays = new() { { 1, 31 }, { 2, 29 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        } else {
            MonthToDays = new() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        }

        if (Day == 1) {
            if (Month == 1) {
                Year--;
                Month = 12;
                Day = MonthToDays[Month];
            } else {
                Month--;
                Day = MonthToDays[Month];
            }
        } else {
            Day--;
        }
    }

    public static void IncreaseDateByMonth(ref int Day, ref int Month, ref int Year) {
        Dictionary<int, int> MonthToDays;
        if (DateTime.IsLeapYear(Year)) {
            MonthToDays = new() { { 1, 31 }, { 2, 29 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        } else {
            MonthToDays = new() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        }

        if (Month == 12) {
            Year++;
            Month = 1;
            if (Day > MonthToDays[Month]) {
                Day = MonthToDays[Month];
            }
        } else {
            Month++;
            if (Day > MonthToDays[Month]) {
                Day = MonthToDays[Month];
            }
        }
    }

    public static void DecreaseDateByMonth(ref int Day, ref int Month, ref int Year) {
        Dictionary<int, int> MonthToDays;
        if (DateTime.IsLeapYear(Year)) {
            MonthToDays = new() { { 1, 31 }, { 2, 29 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        } else {
            MonthToDays = new() { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        }

        if (Month == 1) {
            Year--;
            Month = 12;
            if (Day > MonthToDays[Month]) {
                Day = MonthToDays[Month];
            }
        } else {
            Month--;
            if (Day > MonthToDays[Month]) {
                Day = MonthToDays[Month];
            }
        }
        
        if (new DateTime(Year, Month, Day) < DateTime.Today) {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
        }
    }
}
