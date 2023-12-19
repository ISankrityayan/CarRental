using Application;
using CarRentalAssignment.BLL.Repository;
using CarRentalAssignment.Exceptions;

namespace CarRentalAssignment.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void AddCar_ShouldCreateCarSuccessfully()
    {
        // Arrange
        var carLease = new CarLease();
        var random = new Random();
        var uniqueId = random.Next(100000, 999999);
        var testCar = new Vehicle
        {
            VehicleId = uniqueId,
            Make = "TestMake",
            Model = "TestModel",
            Year = 2020,
            DailyRate = 100,
            IsAvailable = true
        };

        // Act
        Assert.DoesNotThrow(() => carLease.AddCar(testCar));

    }
    [Test]
    public void CreateLease_ShouldCreateLeaseSuccessfully()
    {
        // Arrange
        var carLease = new CarLease();
        var customerId = 1;
        var carId = 1;
        var startDate = DateTime.Now;
        var endDate = DateTime.Now.AddDays(10);

        // Act
        Lease result = null;
        Assert.DoesNotThrow(() => result = carLease.CreateLease(customerId, carId, startDate, endDate));

        // Assert
        Assert.That(result, Is.Not.Null);

    }
    [Test]

    public void RetrieveLeases_ShouldRetrieveLeaseHistoryAndActiveLeasesSuccessfully()
    {
        // Arrange
        var carLease = new CarLease();

        // Act & Assert - Retrieve Lease History
        List<Lease> leaseHistory = null;
        Assert.DoesNotThrow(() => leaseHistory = carLease.ListLeaseHistory(), "Retrieving lease history should not throw an exception");
        Assert.That(leaseHistory, Is.Not.Null, "Lease history should not be null");

        // Act & Assert - Retrieve Active Leases
        List<Lease> activeLeases = null;
        Assert.DoesNotThrow(() => activeLeases = carLease.ListActiveLeases(), "Retrieving active leases should not throw an exception");
        Assert.That(activeLeases, Is.Not.Null, "Active leases list should not be null");


    }


    [Test]
    public void FindCarById_WhenCarIdNotFound_ShouldThrowException()
    {
        // Arrange
        var carLease = new CarLease();
        var nonExistingId = 21;

        // Act & Assert
        var ex = Assert.Throws<CarNotFounException>(() => carLease.FindCarById(nonExistingId));
        Assert.That(ex.Message, Does.Contain($"Car with id: {nonExistingId} not found"));
    }

}
