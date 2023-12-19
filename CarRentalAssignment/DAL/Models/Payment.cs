using System;


namespace Application
{
	public class Payment
	{
        public int PaymentID { get; set; }
        public Lease Rental { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public void RecordPayment()
        {
            Console.WriteLine($"Payment Recorded: Payment ID {PaymentID}, Lease ID {Rental.LeaseID}, " +
                $"Amount {Amount:C}, Date {PaymentDate.ToShortDateString()}");

        }
    }
}

