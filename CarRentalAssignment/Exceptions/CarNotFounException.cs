using System;
namespace CarRentalAssignment.Exceptions
{
	public class CarNotFounException :ApplicationException
	{
        
        
       public CarNotFounException(string message) : base(message) { }
        

    }
}

