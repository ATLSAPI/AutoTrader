using AutoServices.AutoService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public class SearchRepository : ISearchRepository
    {

        private List<Search> search = new List<Search>();
        private int _nextId = 1;

        GetAutorTraderURI uf = new GetAutorTraderURI();
        public SearchRepository()
        {

            string AccessKey = uf.GetPage("https://staging-cws.autotrader.co.uk/CoordinatedWebService/application/crs/connect/hacks/zDk2wtYF");

            //  textBox1.Text = AccessKey + "\r\n\r\n";
            /* collect it from Roy&Kuma and refine it */

            // string json = GetPage("https://staging-cws.autotrader.co.uk/CoordinatedWebService/application/crs/sss/classified-adverts/refinements?Refinement=Make", AccessKey);
            string uri = "";
            uri = "https://staging-cws.autotrader.co.uk/CoordinatedWebService/application/crs/sss/classified-adverts?Page_Size=200";
            /*   if (outputList.Items.Count > 0)
               {

                   foreach (string addo in outputList.Items)
                   {
                       uri = uri + "&";
                       uri = uri + "" + addo;
                   }
               }
               MessageBox.Show(uri);*/
            string json = uf.GetPageWithParam(uri, AccessKey);

            // textBox1.Text += json;
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AutoTraderJSON_CsharpClass));

            AutoTraderJSON_CsharpClass dsjson = ser.ReadObject(ms) as AutoTraderJSON_CsharpClass;

            // outputList.Items.Add("# results: " + dsjson.refinements[0].options.Count.ToString());
            // outputList.Items.Clear();
            //outputList.Items.Add("# results: " + dsjson.searchResults.totalResults.ToString());

            // outputList.Items.Add("# list: " + dsjson.searchResults.classifiedAdverts.Count.ToString());

            for (int i = 0; i < dsjson.searchResults.classifiedAdverts.Count; i++) // Loop through List with for
            {
                try
                {
                    int co2 = Convert.ToInt32(dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.co2Emissions);
                    /*   if (co2 > 100)
                       {*/
                    /*    outputList.Items.Add("# advert id: " + dsjson.searchResults.classifiedAdverts[i].id.ToString() + " " + i.ToString());
                        outputList.Items.Add("#  make: " + dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.make.ToString());
                        outputList.Items.Add("#  model: " + dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.model.ToString());
                        outputList.Items.Add("#  colour: " + dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.colour.ToString());
                        outputList.Items.Add("#  MY: " + dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.vehicleRegistrationDate.ToString());
                        outputList.Items.Add("#  c02Emission: " + dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.co2Emissions.ToString());
                        //  }*/
                    // Add(  new Search { Id = Convert.ToInt32(dsjson.searchResults.classifiedAdverts[i].id), Make = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.make.ToString(), Model = "A5", CO2_Emission = Convert.ToInt32(dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.co2Emissions) });
                    string Description = dsjson.searchResults.classifiedAdverts[i].advertAttributes.description.ToString().ToLower();
                    Double? AnnualTax = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.annualTax;
                    Add(new Search { Id = 0, Make = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.make.ToString(), Model = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.model.ToString(), CO2_Emission = Convert.ToInt32(dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.co2Emissions), Transmission = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.transmission.ToString(), Age = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.age.ToString(), Description = dsjson.searchResults.classifiedAdverts[i].advertAttributes.description.ToString(), MileageFormatted = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.mileageFormatted.ToString(), Seats = GetSeats(Description), AdvertTitle = dsjson.searchResults.classifiedAdverts[i].advertAttributes.advertTitle, AttentionGrabber = dsjson.searchResults.classifiedAdverts[i].advertAttributes.attentionGrabber, AnnualTax = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.annualTax.ToString(),BodyType= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.bodyType.ToString(),Colour= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.colour.ToString(),FuelType= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.fuelType.ToString(),NumberOfDoors= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.numDoors.ToString(),PriceFormatted= dsjson.searchResults.classifiedAdverts[i].advertAttributes.priceFormatted.ToString(),YearOfManufacture= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.yearOfManufacture.ToString(),RunningCosts=RunningCost(AnnualTax) });
                }

                catch
                {

                }
                //outputList.Items.Add("-----");

            }


        
            Add(new Search { Id = 2, Make = "Audi", Model = "A5", CO2_Emission = 120 });
            Add(new Search { Id = 2, Make = "F", Model = "F", CO2_Emission = 0 });
            Add(new Search { Id = 3, Make = "AUDI", Model = "A6", CO2_Emission = 135 });

        }

        public IEnumerable<Search> GetAll()
        {
            return search;
        }

        public Search Get(int id)
        {
            return search.Find(p => p.Id == id);
        }

        public Search Add(Search item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            search.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            search.RemoveAll(p => p.Id == id);
        }

        public bool Update(Search item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = search.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            search.RemoveAt(index);
            search.Add(item);
            return true;
        }

        public string GetSeats(string description)
            {

            if (description.Contains("one seat"))
                {

                

                return "1";
                }

            if (description.Contains("two seat"))
                {

               

                return "2";
                }

            if (description.Contains("three seat"))
                {

                

                return "3";
                }

            if (description.Contains("four seat"))
                {

               

                return "4";
                }

            if (description.Contains("five seat"))
                {

                

                return "5";
                }

            if (description.Contains("six seat"))
                {

                

                return "6";
                }

            if (description.Contains("seven seat"))
                {

                

                return "7";
                }

            if (description.Contains("eight seat"))
                {

                

                return "8";
                }

            if (description.Contains("nine seat"))
                {

                

                return "9";
                }

            if (description.Contains("ten seat"))
                {

               

                return "10";
                }

            if (description.Contains("eleven seat"))
                {

               

                return "11";
                }

            if (description.Contains("1 seat"))
                {

              

                return "1";
                }

            if (description.Contains("2 seat"))
                {

               

                return "2";
                }

            if (description.Contains("3 seat"))
                {

                

                return "3";
                }

            if (description.Contains("4 seat"))
                {

                

                return "4";
                }

            if (description.Contains("5 seat"))
                {

              

                return "5";
                }

            if (description.Contains("6 seat"))
                {

                

                return "6";
                }

            if (description.Contains("7 seat"))
                {

                

                return "7";
                }

            if (description.Contains("8 seat"))
                {

             

                return "8";
                }

            if (description.Contains("9 seat"))
                {

                

                return "9";
                }

            if (description.Contains("10 seat"))
                {

               

                return "10";
                }

            else

                {
                return "Unavailable";
                }
            }
        public string RunningCost(Double? annualtax)
            {

            return "Unavailable";
            }
    }
}
