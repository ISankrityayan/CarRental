using System;
using CarRentalAssignment;
using CarRentalAssignment.BLL.Interface;

namespace Application
{
	public class Vehicle
	{
		public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsAvailable { get; set; }
     }
}

