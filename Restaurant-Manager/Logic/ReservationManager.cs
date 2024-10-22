class ReservationManager
{
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

    public void CreateReservation(int locID, string timeslot, DateTime date, int groupsize)
    {
        Dictionary<List<object>, int> reservations = Database.GetAllReservations();

        //Database.InsertReservationsTable();
    }
}