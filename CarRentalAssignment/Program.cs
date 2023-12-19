using CarRentalAssignment.BLL.Service;

class Program
{
    static void Main(string[] args)
    {
        CarService carLeaseService = new CarService();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nCar Rental System");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Get Car By ID");
            Console.WriteLine("3. Remove Car");
            Console.WriteLine("4. List Available Cars");
            Console.WriteLine("5. List Rented Cars");
            Console.WriteLine("6. Add a new customer");
            Console.WriteLine("7. Add a new Lease");
            Console.WriteLine("8. Remove Customer");
            Console.WriteLine("9. List of customers");
            Console.WriteLine("10. Get customer by ID");
     
           Console.WriteLine("11. Return Car");
            Console.WriteLine("12. List active lease");
            Console.WriteLine("13. List lease history");
            Console.WriteLine("14. Record a payment");
            Console.WriteLine("15. List of payments");

            Console.Write("Enter your choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            try
            {
                switch (choice)
                {
                    case 1:
                        carLeaseService.AddCarService();
                        break;
                    case 2:
                        carLeaseService.GetCarByIdService();
                        break;
                    case 3:
                        carLeaseService.RemoveCarService();
                        break;
                    case 4:
                        carLeaseService.ListAvailableCarsService();
                        break;
                    case 5:
                        carLeaseService.ListRentedCarsService();
                        break;
                    case 6:
                        carLeaseService.AddCustomerService();
                        break;

                    case 7:
                        carLeaseService.CreateLeaseService();

                        break;
                    case 8: carLeaseService.RemoveCustomerService();
                        break;
                    case 9: carLeaseService.ListCustomerService();
                        break;
                    case 10: carLeaseService.GetCustomerByIdService();
                        break;
                    case 11:carLeaseService.ReturnCarService();
                        break;
                    case 12:carLeaseService.ListActiveLeaseHistory();
                        break;
                    case 13: carLeaseService.ListLeaseHistoryService();
                        break;
                    case 14: carLeaseService.RecordPaymentService();
                        break;
                    case 15: carLeaseService.ListPaymentService();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
