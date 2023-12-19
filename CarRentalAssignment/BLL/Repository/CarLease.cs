using System;
using Application;
using CarRentalAssignment.BLL.Interface;
using CarRentalAssignment.Exceptions;
using System.Data.SqlClient;
using CarRentalAssignment.Utilities;
using System.Runtime.ConstrainedExecution;
using CarRentalAssignment.DAL.Models;

namespace CarRentalAssignment.BLL.Repository
{
    public class CarLease : ICarLease

    {

        SqlCommand command = null;
        public string connectionString;

        public CarLease()
        {
            connectionString = ConnectionStringUtility.GetConnectionString("MyDBConnectionString");
            command = new SqlCommand();
        }

        public void AddCar(Vehicle car)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Vehicle (VehicleId,Make, Model, Year, DailyRate, IsAvailable) " +
                        "VALUES (@VehicleId, @Make, @Model, @Year, @DailyRate, @IsAvailable)", connection))
                    {
                        command.Parameters.AddWithValue("@VehicleId", car.VehicleId);
                        command.Parameters.AddWithValue("@Make", car.Make);
                        command.Parameters.AddWithValue("@Model", car.Model);
                        command.Parameters.AddWithValue("@Year", car.Year);
                        command.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                        command.Parameters.AddWithValue("@IsAvailable", car.IsAvailable);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }


        public Vehicle FindCarById(int carID)
        {
            Vehicle vehicle = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleId = @VehicleId",
                        connection))
                    {
                        command.Parameters.AddWithValue("@VehicleId", carID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vehicle = new Vehicle
                                {
                                    VehicleId = (int)reader["VehicleId"],
                                    Make = reader["Make"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    Year = (int)reader["Year"],
                                    DailyRate = (decimal)reader["DailyRate"],
                                    IsAvailable = (bool)reader["IsAvailable"]
                                };
                            }
                        }
                    }
                }

                if (vehicle == null)
                {
                    throw new CarNotFounException($"Car with id: {carID} not found");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return vehicle;
        }


        public List<Vehicle> ListAvailableCars()
        {
            List<Vehicle> availableCars = new List<Vehicle>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Vehicle WHERE IsAvailable = 1", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Vehicle vehicle = new Vehicle
                                {
                                    VehicleId = (int)reader["VehicleId"],
                                    Make = reader["Make"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    Year = (int)reader["Year"],
                                    DailyRate = (decimal)reader["DailyRate"],
                                    IsAvailable = (bool)reader["IsAvailable"]
                                };
                                availableCars.Add(vehicle);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return availableCars;
        }




        public List<Vehicle> ListRentedCars()
        {
            List<Vehicle> rentedCars = new List<Vehicle>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT v.* FROM Vehicle v INNER JOIN Lease l ON v.VehicleId = l.VehicleId" +
                        " WHERE l.EndDate > @CurrentDate", connection))
                    {
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Vehicle vehicle = new Vehicle
                                {
                                    VehicleId = (int)reader["VehicleId"],
                                    Make = reader["Make"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    Year = (int)reader["Year"],
                                    DailyRate = (decimal)reader["DailyRate"],
                                    IsAvailable = (bool)reader["IsAvailable"]
                                };
                                rentedCars.Add(vehicle);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return rentedCars;
        }

        public void RemoveCar(int carID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Vehicle WHERE VehicleId = @VehicleId", connection))
                    {
                        command.Parameters.AddWithValue("@VehicleId", carID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new CarNotFounException($"No car found with id: {carID}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }



        public void ReturnCar(int leaseID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Lease SET EndDate = @CurrentDate WHERE LeaseID = @LeaseID", connection))
                    {
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
                        command.Parameters.AddWithValue("@LeaseID", leaseID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new LeaseNotFoundException($"No lease found with ID: {leaseID}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }



        public void AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Customer (CustomerId, FirstName, LastName, Email, PhoneNumber) VALUES (@CustomerId,@FirstName, @LastName, @Email, @PhoneNumber)", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerID);

                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("The operation did not affect any rows.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }


        public Customer FindCustomerById(int customerID)
        {
            Customer customer = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new Customer
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                };
                            }
                        }
                    }
                }

                if (customer == null)
                {
                    throw new CustomerNotFoundException($"Customer with id: {customerID} not found");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return customer;
        }




        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString()
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return customers;
        }


        public void RemoveCustomer(int customerID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @CustomerID", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new CustomerNotFoundException($"No customer found with ID: {customerID}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }




        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            Lease lease;
            TimeSpan duration = endDate - startDate;
            int leaseId;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Lease (CustomerId, VehicleId, StartDate, EndDate) OUTPUT INSERTED.LeaseID VALUES (@CustomerId, @VehicleId, @StartDate, @EndDate)", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerID);
                        command.Parameters.AddWithValue("@VehicleId", carID);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        leaseId = (int)command.ExecuteScalar();
                    }


                    if (duration.TotalDays > 30)
                    {
                        lease = new MonthlyLease
                        {
                            LeaseID = leaseId,
                            Vehicle = FindCarById(carID),
                            Customer = FindCustomerById(customerID),
                            StartDate = startDate,
                            EndDate = endDate
                        };
                    }
                    else
                    {
                        lease = new DailyLease
                        {
                            LeaseID = leaseId,
                            Vehicle = FindCarById(carID),
                            Customer = FindCustomerById(customerID),
                            StartDate = startDate,
                            EndDate = endDate
                        };
                    }

                    return lease;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }




        public List<Lease> ListActiveLeases()
        {
            List<Lease> activeLeases = new List<Lease>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Lease WHERE EndDate > @CurrentDate", connection))
                    {
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int leaseId = (int)reader["LeaseID"];
                                int customerId = (int)reader["CustomerId"];
                                int vehicleId = (int)reader["VehicleId"];
                                DateTime startDate = (DateTime)reader["StartDate"];
                                DateTime endDate = (DateTime)reader["EndDate"];

                                Lease lease;

                                if (endDate.Subtract(startDate).TotalDays > 30)
                                {
                                    lease = new MonthlyLease
                                    {
                                        LeaseID = leaseId,
                                        Vehicle = FindCarById(vehicleId),
                                        Customer = FindCustomerById(customerId),
                                        StartDate = startDate,
                                        EndDate = endDate
                                    };
                                }
                                else
                                {
                                    lease = new DailyLease
                                    {
                                        LeaseID = leaseId,
                                        Vehicle = FindCarById(vehicleId),
                                        Customer = FindCustomerById(customerId),
                                        StartDate = startDate,
                                        EndDate = endDate
                                    };
                                }

                                activeLeases.Add(lease);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return activeLeases;
        }

        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leaseHistory = new List<Lease>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Lease", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int leaseId = (int)reader["LeaseID"];
                                int customerId = (int)reader["CustomerId"];
                                int vehicleId = (int)reader["VehicleId"];
                                DateTime startDate = (DateTime)reader["StartDate"];
                                DateTime endDate = (DateTime)reader["EndDate"];

                                Lease lease;
                                if (endDate.Subtract(startDate).TotalDays > 30)
                                {
                                    lease = new MonthlyLease
                                    {
                                        LeaseID = leaseId,
                                        Vehicle = FindCarById(vehicleId),
                                        Customer = FindCustomerById(customerId),
                                        StartDate = startDate,
                                        EndDate = endDate
                                    };
                                }
                                else
                                {
                                    lease = new DailyLease
                                    {
                                        LeaseID = leaseId,
                                        Vehicle = FindCarById(vehicleId),
                                        Customer = FindCustomerById(customerId),
                                        StartDate = startDate,
                                        EndDate = endDate
                                    };
                                }

                                leaseHistory.Add(lease);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return leaseHistory;
        }



        public List<Payment> ListPayments(Lease lease)
        {
            List<Payment> payments = new List<Payment>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Payment WHERE LeaseID = @LeaseID", connection))
                    {
                        command.Parameters.AddWithValue("@LeaseID", lease.LeaseID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Payment payment = new Payment
                                {
                                    PaymentID = (int)reader["PaymentID"],
                                    Rental = lease,
                                    PaymentDate = (DateTime)reader["PaymentDate"],
                                    Amount = (decimal)reader["Amount"]
                                };
                                payments.Add(payment);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }

            return payments;
        }


        public void RecordPayment(Lease lease, double amount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Payment (LeaseID, PaymentDate, Amount) VALUES (@LeaseID, @PaymentDate, @Amount)", connection))
                    {
                        command.Parameters.AddWithValue("@LeaseID", lease.LeaseID);
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now); 
                        command.Parameters.AddWithValue("@Amount", amount);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("The operation did not affect any rows.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                throw;
            }
        }

    }
}

