using System;
namespace Application
{
	public class DailyLease: Lease
	{
        public override decimal CalculateLeaseCost()
        {
            TimeSpan leaseDuration = EndDate - StartDate;
            return leaseDuration.Days * Vehicle.DailyRate;

        }

        public override string GenerateLeaseAgreement()
        {
            return $"Lease Agreement: Lease ID {LeaseID}, Vehicle ID {Vehicle.VehicleId}, " +
                $"Customer ID {Customer.CustomerID}, Start Date {StartDate.ToShortDateString()}, End Date {EndDate.ToShortDateString()}";

        }
    }
}

