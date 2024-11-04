using System;
using System.Collections.Generic;
using System.Linq;

class RoomBookingSystem
{
    public void BookRoom(int roomNumber) => Console.WriteLine($"Room {roomNumber} has been booked.");
    public void CheckAvailability(int roomNumber) => Console.WriteLine($"Room {roomNumber} is available.");
    public void CancelBooking(int roomNumber) => Console.WriteLine($"Booking for room {roomNumber} has been canceled.");

}
class RestaurantSystem
{
    public void ReserveTable(int tableNumber) => Console.WriteLine($"Table {tableNumber} has been reserved.");
    public void OrderFood(string dish) => Console.WriteLine($"Ordered {dish} from the restaurant.");

}
class EventManagementSystem
{
    public void BookConferenceRoom(string room, string equipment) => Console.WriteLine($"Conference room '{room}' booked with {equipment}.");

}
class CleaningService
{
    public void ScheduleCleaning(int roomNumber) => Console.WriteLine($"Cleaning scheduled for room {roomNumber}.");
    public void PerformCleaning(int roomNumber) => Console.WriteLine($"Cleaning performed for room {roomNumber}.");

}

class HotelFacade
{
    private readonly RoomBookingSystem _roomBookingSystem = new RoomBookingSystem();
    private readonly RestaurantSystem _restaurantSystem = new RestaurantSystem();
    private readonly EventManagementSystem _eventManagementSystem = new EventManagementSystem();
    private readonly CleaningService _cleaningService = new CleaningService();

    public void BookRoomWithServices(int roomNumber, string dish)
    {
        _roomBookingSystem.BookRoom(roomNumber);
        _restaurantSystem.OrderFood(dish);
        _cleaningService.ScheduleCleaning(roomNumber);
        Console.WriteLine("Room booked with food and cleaning services.");
    }

    public void OrganizeEventWithRoomsAndEquipment(string conferenceRoom, string equipment, int[] roomNumbers)
    {
        _eventManagementSystem.BookConferenceRoom(conferenceRoom, equipment);
        foreach (var roomNumber in roomNumbers)
        {
            _roomBookingSystem.BookRoom(roomNumber);
        }
        Console.WriteLine("Event organized with rooms and equipment.");
    }

    public void ReserveTableWithTaxi(int tableNumber)
    {
        _restaurantSystem.ReserveTable(tableNumber);
        Console.WriteLine("Taxi service has been booked.");
    }

    public void CancelRoomBooking(int roomNumber) => _roomBookingSystem.CancelBooking(roomNumber);

    public void RequestCleaning(int roomNumber) => _cleaningService.PerformCleaning(roomNumber);
}
