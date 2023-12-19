using System;
using Application;
using CarRentalAssignment.BLL.Repository;

namespace CarRentalAssignment.BLL.Service
{
    public class CarService
    {

        private CarLease carLease;

        public CarService()
        {
            carLease = new CarLease();
        }

        public void AddCarService()
        {
            Console.WriteLine("Enter Car Details:");

            Console.Write("Vehicle ID: ");
            int vehicleId = int.Parse(Console.ReadLine());

            Console.Write("Make: ");
            string make = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Daily Rate: ");
            decimal dailyRate = decimal.Parse(Console.ReadLine());

            Console.Write("Is Available (true/false): ");
            bool isAvailable = bool.Parse(Console.ReadLine());

            Vehicle newCar = new Vehicle
            {
                VehicleId = vehicleId,
                Make = make,
                Model = model,
                Year = year,
                DailyRate = dailyRate,
                IsAvailable = isAvailable
            };

            carLease.AddCar(newCar);
            Console.WriteLine("Car added successfully.");
        }


        public void GetCarByIdService()
        {
            Console.WriteLine("Enter the id of the car");
            int carId;
            if (int.TryParse(Console.ReadLine(), out carId))
            {

                try
                {
                    Vehicle car = carLease.FindCarById(carId);
                    if (carId != null)
                    {
                        Console.WriteLine($"Car ID: {car.VehicleId}");
                        Console.WriteLine($"Make: {car.Make}");
                        Console.WriteLine($"Model: {car.Model}");
                        Console.WriteLine($"Year: {car.Year}");
                        Console.WriteLine($"DailyRate: {car.DailyRate}");


                    }
                    else
                    {
                        Console.WriteLine("Car not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        public void RemoveCarService()
        {
            Console.WriteLine("Enter the car ID you wish to remove");
            int carId;
            if (int.TryParse(Console.ReadLine(), out carId))
            {

                try
                {
                    carLease.RemoveCar(carId);
                    Console.WriteLine($"Car with ID {carId} has been successfully removed.");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        }

        public void ListAvailableCarsService()
        {
            try
            {
                var availableCars = carLease.ListAvailableCars();
                if (availableCars.Count > 0)
                {
                    foreach (var car in availableCars)
                    {
                        Console.WriteLine($"ID:{car.VehicleId},  Make: {car.Make}, Model: {car.Model}, " +
                            $"Year: {car.Year}, DailyRate: {car.DailyRate}, Availability: {car.IsAvailable} ");
                    }
                }

                else
                {
                    Console.WriteLine("No cars available");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListRentedCarsService()
        {
            try
            {
                var rentedCars = carLease.ListRentedCars();
                if (rentedCars.Count > 0)
                {
                    foreach (var car in rentedCars)
                    {
                        Console.WriteLine($"ID:{car.VehicleId},  Make: {car.Make}, Model: {car.Model}, " +
                            $"Year: {car.Year}, DailyRate: {car.DailyRate}, Availability: {car.IsAvailable} ");
                    }
                }

                else
                {
                    Console.WriteLine("No cars rented at the moment");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }


        public void AddCustomerService()
        {
            try
            {
                Console.WriteLine("Enter Customer Details:");
                Console.WriteLine("Enter Customer Id");
                int customerId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter First Name");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Phone Number");
                string mobile = Console.ReadLine();

                Customer newCustomer = new Customer
                {
                    CustomerID = customerId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = mobile

                };
                carLease.AddCustomer(newCustomer);
                Console.WriteLine("Customer Added Successfully");

            }

            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }

        }

        public void CreateLeaseService()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());

                Console.Write("Enter Car ID: ");
                int carId = int.Parse(Console.ReadLine());

                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter End Date (yyyy-mm-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Lease lease = carLease.CreateLease(customerId, carId, startDate, endDate);

                Console.WriteLine($"Lease created successfully with Lease ID: {lease.LeaseID}");


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error:{e.Message}");
            }



        }

        public void GetCustomerByIdService()
        {
            Console.WriteLine("Enter the customer ID you wish to find");
            int customerId;
            if(int.TryParse(Console.ReadLine(),out customerId))
            {
                try
                {
                    Customer customer = carLease.FindCustomerById(customerId);
                    if (customer != null)
                    {
                        Console.WriteLine($"Customer ID: {customer.CustomerID}");
                        Console.WriteLine($"Customer First Name: {customer.FirstName}");
                        Console.WriteLine($"Customer Last Name: {customer.LastName}");
                        Console.WriteLine($"Customer Email: {customer.Email}");
                        Console.WriteLine($"Customer Phone: {customer.PhoneNumber}");


                    }
                    else { Console.WriteLine($"Customer not found"); }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}");
                }
            }

            else
            {
                Console.WriteLine($"Invalid input");
            }
        }

        public void RemoveCustomerService()
        {
            Console.WriteLine($"Enter the ID of the customer you want to remove");
            int customerId;
            if(int.TryParse(Console.ReadLine(), out customerId))
            {
                try
                {
                    carLease.RemoveCustomer(customerId);
                        Console.WriteLine($"Customer with ID {customerId} has been removed ");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}");

                }
            }
            else
            {
                Console.WriteLine($"Invalid input");

            }
        }

        public void ListCustomerService()
        {
            try
            {
                var customers = carLease.ListCustomers();
                if (customers.Count > 0)
                {
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID:{customer.CustomerID},  First Name: {customer.FirstName}, Last Name: {customer.LastName}, " +
                            $"Email: {customer.Email}, Phone: {customer.PhoneNumber}");
                    }
                }

                else
                {
                    Console.WriteLine("No cars rented at the moment");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ReturnCarService()
        {
            Console.WriteLine($"Enter the Lease ID of the car you want to return");
            int leaseId;
            if(int.TryParse(Console.ReadLine(),out leaseId))
            {
                try
                {
                    if (leaseId != null)
                    {
                        carLease.ReturnCar(leaseId);
                        Console.WriteLine("Car returned succesfully");
                     }
                    else
                    {
                        Console.WriteLine("Lease not found");
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
               
            }
            else
            {
                Console.WriteLine($"Invalid input");
            }
        }

        public void ListActiveLeaseHistory()
        {
            try
            {
                var activeLease = carLease.ListActiveLeases();
                if (activeLease.Count > 0)
                {
                    foreach (var lease in activeLease)
                    {
                        Console.WriteLine($"Lease ID: {lease.LeaseID}, Customer ID: {lease.Customer.CustomerID}, " +
                            $"Car ID: {lease.Vehicle.VehicleId}, Start Date: {lease.StartDate.ToShortDateString()}, " +
                            $"End Date: {lease.EndDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No active leases found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListLeaseHistoryService()
        {
            try
            {
                var leaseHistory = carLease.ListLeaseHistory();
                if (leaseHistory.Count > 0)
                {
                    Console.WriteLine("Lease History:");
                    foreach (var lease in leaseHistory)
                    {
                        Console.WriteLine($"Lease ID: {lease.LeaseID}, Customer ID: {lease.Customer.CustomerID}, " +
                            $"Car ID: {lease.Vehicle.VehicleId}, Start Date: {lease.StartDate.ToShortDateString()}, " +
                            $"End Date: {lease.EndDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No lease history available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        
        
            public void RecordPaymentService()
            {
                Console.WriteLine("Enter Lease ID for payment:");
                int leaseId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter payment amount:");
                double amount = double.Parse(Console.ReadLine());

                try
                {
                    var allLeases = carLease.ListActiveLeases().Concat(carLease.ListLeaseHistory()).ToList();
                    var lease = allLeases.FirstOrDefault(l => l.LeaseID == leaseId);

                    if (lease != null)
                    {
                        carLease.RecordPayment(lease, amount);
                        Console.WriteLine($"Payment of {amount} recorded for Lease ID: {leaseId}");
                    }
                    else
                    {
                        Console.WriteLine("Lease not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        public void ListPaymentService()
        {
            Console.WriteLine("Enter Lease ID to list payments:");
            int leaseId = int.Parse(Console.ReadLine());

            try
            {
                var allLeases = carLease.ListActiveLeases().Concat(carLease.ListLeaseHistory()).ToList();
                var lease = allLeases.FirstOrDefault(l => l.LeaseID == leaseId);

                if (lease != null)
                {
                    var payments = carLease.ListPayments(lease);
                    foreach (var payment in payments)
                    {
                        Console.WriteLine($"Payment ID: {payment.PaymentID}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
                    }
                }
                else
                {
                    Console.WriteLine("Lease not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }
}

