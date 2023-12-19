using System;
namespace Application
{
	public abstract class Lease
	{

        public int LeaseID { get; set; }
        public Vehicle Vehicle { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public abstract decimal CalculateLeaseCost();
        public abstract string GenerateLeaseAgreement();
    }

}

