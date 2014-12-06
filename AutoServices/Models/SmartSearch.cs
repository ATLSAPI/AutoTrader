using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServices.Models
{
    public class ResultCount
    {
        public int SearchResultCount { get; set; }
        public int TotalResultCount { get; set; }        

    }
    public class SearchAttributes
    {
      //  public int Id { get; set; }
        public string Advert_Id { get; set; }       
        public string FuelType { get; set; }
      //  public string BodyType { get; set; }
        public string AdvertTitle { get; set; }
        public string Colour { get; set; }
       // public string NumberOfDoors { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }        
        public string Transmission { get; set; }
        public string Seats { get; set; }       
        public string PriceFormatted { get; set; }       
       // public string RunningCosts { get; set; }
        //public string TopSpeed { get; set; }
       // public string Town { get; set; }
        public string Image { get; set; }
       // public string VehicleLatLong { get; set; }
        public string Time { get; set; }
        //  public string AttentionGrabber { get; set; }
        public string YearOfManufacture { get; set; }
        //public decimal CO2_Emission { get; set; }
        //public string Description { get; set; }
        // public string Age { get; set; }
         public string MileageFormatted { get; set; }
        public int Price { get; set; }
        public string Distance { get; set; }
        // public double? AnnualTax { get; set; }
        //public string MPG { get; set; }
      


    }

    public class AllSearchAttributes
    {
        //  public int Id { get; set; }
        public string Advert_Id { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public string AdvertTitle { get; set; }
        public string Colour { get; set; }
        public string NumberOfDoors { get; set; }
        public string Make { get; set; }
         public string Model { get; set; }        
        public string Transmission { get; set; }
        public string Seats { get; set; }
        public string PriceFormatted { get; set; }
        public string RunningCosts { get; set; }
        public string TopSpeed { get; set; }
        public string Town { get; set; }
        public string Image { get; set; }
        public string VehicleLatLong { get; set; }
        public string Distance { get; set; }
        public string Time { get; set; }
         public string AttentionGrabber { get; set; }
         public string YearOfManufacture { get; set; }
        public decimal CO2_Emission { get; set; }
        public string Description { get; set; }
         public string Age { get; set; }
          public string MileageFormatted { get; set; }
        public int Price { get; set; }
         public double? AnnualTax { get; set; }
        public string MPG { get; set; }



    }

    public class SmartSearch
    {
        public List<SearchAttributes> SearchAttributes { get; set; }
        public List<AllSearchAttributes> AllSearchAttributes { get; set; }
        public ResultCount ResultCount { get; set; }

    }
    
}