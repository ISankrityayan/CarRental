using System;
namespace CarRentalAssignment.Exceptions
{
	public class LeaseNotFoundException: Exception
	{
        public LeaseNotFoundException(string message) : base(message) { }
    }
}


