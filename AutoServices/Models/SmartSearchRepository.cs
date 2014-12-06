using AutoServices.AutoService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AutoServices.Models
{
    public class SmartSearchRepository : ISmartSearchRepository
    {
/* to get more pages request for pagenum */
        // static readonly ISearchRepository repository = new SearchRepository();

        private List<SmartSearch> search = new List<SmartSearch>();

       private List<AllSearchAttributes> allattr = new List<AllSearchAttributes>();
       private List<SearchAttributes> items = new List<SearchAttributes>();
        private List<SearchAttributes> searchattr = new List<SearchAttributes>();
        private ResultCount ResultCount = new ResultCount();
        
        int GettotalResults = 0;

       // string all = "";
        public SmartSearchRepository(string request, string other)
        {
            string request_uri = "classified-adverts?" + request.ToString();
            
            mainload();


            if (request == "")
            { }

            if (request.ToLower().Contains("compare"))
            {

                string[] value = request.Split('-');

                Compare(value[0], value[1]);

            }
            else if (request.ToLower().Contains("&count"))
            {
                request_uri = Regex.Replace(request_uri, "&count", string.Empty, RegexOptions.IgnoreCase);

                search[0].ResultCount.TotalResultCount = Get_TotalNumOfResult(request_uri);
            }

            else
            {
                GetATData(request_uri, other);

            }
        }

        public void mainload()
        {
            search.Add(new SmartSearch
            {
                ResultCount = ResultCount,
                SearchAttributes = searchattr,
                AllSearchAttributes = allattr,
                

            });

        }




        public void GetATData(string uri, string postcode)
        {

             string AccessKey = ApiConnect.getTokenText();
   
    
            string json = ApiConnect.request(uri, AccessKey);
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AutoTraderJSON_CsharpClass));
            AutoTraderJSON_CsharpClass dsjson = ser.ReadObject(ms) as AutoTraderJSON_CsharpClass;

           search[0].ResultCount.TotalResultCount = dsjson.searchResults.totalResults;
           search[0].ResultCount.SearchResultCount = dsjson.searchResults.classifiedAdverts.Count;
            int CLA_count = dsjson.searchResults.classifiedAdverts.Count;
            
        for (int i = 0; i < dsjson.searchResults.classifiedAdverts.Count; i++) // Loop through List with for
            {
             try
                {
                                         
                   // int co2 = 0; try { co2 = Convert.ToInt32(dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.co2Emissions); } catch { }
                    string make = ""; try { make = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.make.ToString();  } catch { }                  
                    string advert_id = dsjson.searchResults.classifiedAdverts[i].id;
                    string model = ""; try { model =dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.model.ToString();} catch { }
                    string transmission = ""; try { transmission = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.transmission.ToString();} catch { }
                    string age = "0"; try { age= dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.age.ToString(); }  catch { }
                    string desc = ""; try { desc = dsjson.searchResults.classifiedAdverts[i].advertAttributes.description.ToString();} catch { }
                    string mileage_formatted = ""; try { mileage_formatted = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.mileageFormatted.ToString(); }
                    catch { }
                    string seat = CreateSearchParam.GetSeats(desc);
                    string advert_title = ""; try { advert_title = dsjson.searchResults.classifiedAdverts[i].advertAttributes.advertTitle;} catch { }
                  //  string attention_grabber = dsjson.searchResults.classifiedAdverts[i].advertAttributes.attentionGrabber;
                  //  double? annualtax = 0; try { annualtax = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.annualTax; if (annualtax == null) { annualtax = 0; } }  catch { }
                  //  string bodytype = ""; try { bodytype = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.bodyType.ToString();} catch { }
                    string colour = ""; try { colour = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.colour.ToString();} catch { }
                   string fueltype = ""; try { fueltype = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.fuelType.ToString();} catch { }
                  //  string numdoors = ""; try { numdoors = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.numDoors;} catch { }
                    string priceFormatted = ""; try { priceFormatted = dsjson.searchResults.classifiedAdverts[i].advertAttributes.priceFormatted.ToString();} catch { }
                    int price = 0; try { price = dsjson.searchResults.classifiedAdverts[i].advertAttributes.price;} catch { }
                   // string town = "unknown"; try { town = dsjson.searchResults.classifiedAdverts[i].advertAttributes.town.name.ToString();} catch { }
                   string YOfM = ""; try { YOfM = dsjson.searchResults.classifiedAdverts[i].vehicleAttributes.yearOfManufacture.ToString();} catch { }
                   //int miles = 13500;
                  // string fuel_level = "Average";
                   string image = ""; try { image = dsjson.searchResults.classifiedAdverts[i].advertAttributes.imageInfo.advertImages[0].imageUrlTemplate; }  catch { }
                   //string mpg = "0"; try { mpg = dsjsonID.techSpec.fuelConsumptionCombined; if (mpg == null) { mpg = "0"; } }   catch { }
                  // string speed = "0 mph"; try { speed = dsjsonID.techSpec.topSpeed; if (speed == null) { speed = "0 mph"; } }   catch { }
                   string latlong =""; try { latlong = dsjson.searchResults.classifiedAdverts[i].advertAttributes.vehicleLatLong[0].ToString(); if (latlong == null) { latlong = "0"; } }    catch { }
                   string latlong1 = ""; try { latlong1 = dsjson.searchResults.classifiedAdverts[i].advertAttributes.vehicleLatLong[1].ToString(); if (latlong == null) { latlong1 = "0"; } }
                   catch { }
                   
                   string distance = DistanceFromCar.GetDistance(postcode, latlong + "," + latlong1);
                   string time = DistanceFromCar.GetTravelTime(postcode, latlong + "," + latlong1);
                


                    search[0].SearchAttributes.Add(new SearchAttributes
                    {
                        //Id = 0,
                        Advert_Id = advert_id,
                        Make = make,
                        Model = model,                        
                        Transmission = transmission,                        
                        Seats = seat,
                        AdvertTitle = advert_title,                        
                       // BodyType = bodytype,
                        Time = time,
                        Colour = colour,
                        FuelType = fueltype,
                        //NumberOfDoors = numdoors,
                        PriceFormatted = priceFormatted,
                        Price = price,
                        Distance = distance,
                       // TopSpeed = speed,                      
                       // Town = town,
                        Image = image,
                        //VehicleLatLong = latlong
                        //CO2_Emission = co2,
                        // Age = age,
                        //Description = desc,
                        MileageFormatted = mileage_formatted,
                        // AttentionGrabber = attention_grabber,
                        //AnnualTax = annualtax,
                        // Price = price,
                         YearOfManufacture = YOfM,
                        // MPG = mpg,
                    });
                }


                catch (Exception os)
                {
                    //Add(new SmartSearch { Advert_Id = dsjson.searchResults.classifiedAdverts[0].id, Make = os.ToString() });
                }
           
            }
        
            
        }

        
        public int Get_TotalNumOfResult(string uri)
        {

            string AccessKey = ApiConnect.getTokenText();


            string json = ApiConnect.request(uri, AccessKey);
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AutoTraderJSON_CsharpClass));
            AutoTraderJSON_CsharpClass dsjson = ser.ReadObject(ms) as AutoTraderJSON_CsharpClass;
            search[0].ResultCount.TotalResultCount = dsjson.searchResults.totalResults;

            return dsjson.searchResults.totalResults;
        }


        public IEnumerable<SmartSearch> GetAll()
        {
           // search.RemoveAll(item => item == null);
            return search;
        }




      
        public SmartSearch Get(string id)
        {
            ThisCarDetails(id.ToString());
            return search[0];
        }

        /* public IEnumerable<SmartSearch> GetFrom(int id)
         {
             return search.FindAll(p => p.Id < id); //&& p.Id >= id);
         }*/
        public SearchAttributes Add(SearchAttributes item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //item.Id = _nextId++;
            searchattr.Add(item);
            return item;
        }

        public SmartSearch AddAll(SmartSearch item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            ResultCount.SearchResultCount = searchattr.Count;
            search.Add(item);
            return item;
        }

     


        public /*IEnumerable<SmartSearch>*/ void Compare(string FirstCar, string SecondCar)
        {
            string[] Cars = new string[2] { FirstCar, SecondCar };

            foreach (string ids in Cars)
            {


                ThisCarDetails(ids);
           
            }



           // return search;
        }
    
     
        public void ThisCarDetails(string advertid)
        {

            string AccessKey = ApiConnect.getTokenText();
            string uri = "classified-advert/" + advertid;
            // string jsonID = access.GetPageWithParam(url, AccessKey);
            string json = ApiConnect.request(uri, AccessKey);
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AutoTraderJSON_CsharpClass));
            AutoTraderJSON_CsharpClass dsjson = ser.ReadObject(ms) as AutoTraderJSON_CsharpClass;
           
            int co2 = 0; try { co2 = Convert.ToInt32(dsjson.vehicleAttributes.co2Emissions); }
            catch { }
            string make = ""; try { make = dsjson.vehicleAttributes.make.ToString(); }
            catch { }
            string advert_id = advertid;// dsjson.searchResults.classifiedAdverts[id].id;
            string model = ""; try { model = dsjson.vehicleAttributes.model.ToString(); }
            catch { }
            string transmission = ""; try { transmission = dsjson.vehicleAttributes.transmission.ToString(); }
            catch { }
            string age = "0"; try { age = dsjson.vehicleAttributes.age.ToString(); }
            catch { }
            string desc = ""; try { desc = dsjson.advertAttributes.description.ToString(); }
            catch { }
            string mileage_formatted = ""; try { mileage_formatted = dsjson.vehicleAttributes.mileageFormatted.ToString(); }
            catch { }
            string seat = CreateSearchParam.GetSeats(desc);
            string advert_title = ""; try { advert_title = dsjson.advertAttributes.advertTitle; }
            catch { }
            string attention_grabber = ""; try { attention_grabber = dsjson.advertAttributes.attentionGrabber; }
            catch { }
            double? annualtax = 0; try { annualtax = dsjson.vehicleAttributes.annualTax; if (annualtax == null) { annualtax = 0; } }
            catch { }
            string bodytype = ""; try { bodytype = dsjson.vehicleAttributes.bodyType.ToString(); }
            catch { }
            string colour = ""; try { colour = dsjson.vehicleAttributes.colour.ToString(); }
            catch { }
            string fueltype = ""; try { fueltype = dsjson.vehicleAttributes.fuelType.ToString(); }
            catch { }
            string numdoors = ""; try { numdoors = dsjson.vehicleAttributes.numDoors; }
            catch { }
            string priceFormatted = ""; try { priceFormatted = dsjson.advertAttributes.priceFormatted.ToString(); }
            catch { }
            int price = 0; try { price = dsjson.advertAttributes.price; }
            catch { }
            string town = "unknown"; try { town = dsjson.advertAttributes.town.name.ToString(); }
            catch { }
            string YOfM = ""; try { YOfM = dsjson.vehicleAttributes.yearOfManufacture.ToString(); }
            catch { }
            int miles = 13500;
            string fuel_level = "Average";
            string image = ""; try { image = dsjson.advertAttributes.imageInfo.advertImages[0].imageUrlTemplate; }
            catch { }
            string mpg = "0"; try { mpg = dsjson.techSpec.fuelConsumptionCombined; if (mpg == null) { mpg = "0"; } }
            catch { }
            string speed = "0 mph"; try { speed = dsjson.techSpec.topSpeed; if (speed == null) { speed = "0 mph"; } }
            catch { }
            string latlong = ""; try { latlong = dsjson.advertAttributes.vehicleLatLong[0].ToString(); if (latlong == null) { latlong = "0"; } }
            catch { }
           


            search[0].AllSearchAttributes.Add(new AllSearchAttributes
            {

                Advert_Id = advert_id,
                Make = make,
                Model = model,
                Transmission = transmission,
                Seats = seat,
                AdvertTitle = advert_title,
                BodyType = bodytype,
                Colour = colour,
                FuelType = fueltype,
                NumberOfDoors = numdoors,
                PriceFormatted = priceFormatted,
                
                TopSpeed = speed,
                Town = town,
                Image = image,
                VehicleLatLong = latlong,
                CO2_Emission = co2,
                Age = age,
                Description = desc,
                MileageFormatted = mileage_formatted,
                AttentionGrabber = attention_grabber,
                AnnualTax = annualtax,
                Price = price,
                YearOfManufacture = YOfM,
                MPG = mpg
            });

        }
  

    }

    }
