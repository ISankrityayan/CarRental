using System;
using Application;

namespace CarRentalAssignment.DAL.Models
{
    public class MonthlyLease : Lease
    {
        public override decimal CalculateLeaseCost()
        {
            TimeSpan leaseDuration = EndDate - StartDate;
            int months = (int)Math.Ceiling(leaseDuration.TotalDays / 30.0); 
            return months * Vehicle.DailyRate * 30; 
        }

        public override string GenerateLeaseAgreement()
        {
            return $"Lease Agreement: Lease ID {LeaseID}, Vehicle ID {Vehicle.VehicleId}, " +
                   $"Customer ID {Customer.CustomerID}, Start Date {StartDate.ToShortDateString()}, " +
                   $"End Date {EndDate.ToShortDateString()}, Total Cost {CalculateLeaseCost()}";
        }
    }
}

