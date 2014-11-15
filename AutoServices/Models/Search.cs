using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServices.Models
{
    public class Search
    {
        public int Id { get; set; }
        public string AttentionGrabber { get; set; }
        public string YearOfManufacture { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string AdvertTitle { get; set; }
        public string Colour { get; set; }
        public string NumberOfDoors { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal CO2_Emission { get; set; }
        public string Transmission { get; set; }
        public string Seats { get; set; }
        public string Description { get; set; }
        public string Age { get; set; }
        public string MileageFormatted { get; set; }
        public string PriceFormatted { get; set; }
        public string AnnualTax { get; set; }
        public string RunningCosts { get; set; }
        }
}