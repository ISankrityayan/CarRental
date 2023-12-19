using System;
using Application;
using CarRentalAssignment;
namespace CarRentalAssignment.BLL.Interface
{
	public interface ICarLease
	{
        void AddCar(Vehicle car);
        void RemoveCar(int carID);
        List<Vehicle> ListAvailableCars();
        List<Vehicle> ListRentedCars();
        Vehicle FindCarById(int carID);

        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);

        Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate);
        void ReturnCar(int leaseID);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();

       
        void RecordPayment(Lease lease, double amount);
        List<Payment> ListPayments(Lease lease);

    }
}

