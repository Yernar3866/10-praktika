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
// Абстрактный класс OrganizationComponent
abstract class OrganizationComponent
{
    public string Name { get; protected set; }
    public abstract int GetEmployeeCount();
    public abstract decimal GetBudget();
    public abstract void Display(int indent = 0);
}

// Класс Employee для сотрудников
class Employee : OrganizationComponent
{
    public string Position { get; private set; }
    public decimal Salary { get; set; }

    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override int GetEmployeeCount() => 1;

    public override decimal GetBudget() => Salary;

    public override void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}Employee: {Name}, Position: {Position}, Salary: {Salary}");
    }
}

// Класс Department для подразделений
class Department : OrganizationComponent
{
    private readonly List<OrganizationComponent> _components = new List<OrganizationComponent>();

    public Department(string name)
    {
        Name = name;
    }

    public void AddComponent(OrganizationComponent component)
    {
        if (!_components.Contains(component))
            _components.Add(component);
    }

    public void RemoveComponent(OrganizationComponent component)
    {
        if (_components.Contains(component))
            _components.Remove(component);
    }

    public override int GetEmployeeCount() => _components.Sum(c => c.GetEmployeeCount());

    public override decimal GetBudget() => _components.Sum(c => c.GetBudget());

    public override void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}Department: {Name}");
        foreach (var component in _components)
        {
            component.Display(indent + 1);
        }
    }

    public OrganizationComponent FindEmployee(string name)
    {
        foreach (var component in _components)
        {
            if (component is Employee emp && emp.Name == name)
                return emp;
            if (component is Department dept)
            {
                var found = dept.FindEmployee(name);
                if (found != null) return found;
            }
        }
        return null;
    }
}


class Program
{
    static void Main()
    {
        #region Использование фасада для гостиницы

        var hotelFacade = new HotelFacade();

        hotelFacade.BookRoomWithServices(101, "Pasta");
        hotelFacade.OrganizeEventWithRoomsAndEquipment("Main Hall", "Projector", new[] { 102, 103 });
        hotelFacade.ReserveTableWithTaxi(5);
        hotelFacade.CancelRoomBooking(101);
        hotelFacade.RequestCleaning(101);




        var emp1 = new Employee("Alice", "Manager", 5000);
        var emp2 = new Employee("Bob", "Developer", 3000);
        var emp3 = new Employee("Charlie", "Designer", 3500);
        var emp4 = new Employee("Dave", "HR", 4000);

        var devDept = new Department("Development");
        devDept.AddComponent(emp1);
        devDept.AddComponent(emp2);

        var hrDept = new Department("Human Resources");
        hrDept.AddComponent(emp4);

        var designDept = new Department("Design");
        designDept.AddComponent(emp3);

        var company = new Department("Company");
        company.AddComponent(devDept);
        company.AddComponent(hrDept);
        company.AddComponent(designDept);

        Console.WriteLine("\nCompany Structure:");
        company.Display();


        Console.WriteLine($"\nTotal Budget: {company.GetBudget()}");
        Console.WriteLine($"Total Employees: {company.GetEmployeeCount()}");
        var searchName = "Alice";
        var foundEmployee = company.FindEmployee(searchName);
        Console.WriteLine($"\nSearch Result for '{searchName}':");
        foundEmployee?.Display();

        #endregion
    }
}
