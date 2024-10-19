// Interface to create a reservation by the user
interface IReservation
{
    //Dictionary containing 
    List<string> Tables { get; set; }

    string SelectLocation();
    string SelectTimeSlot();
    string SelectAmountOfPeople();
    Reservation CreateReservation();
    string DisplayInfo();
}