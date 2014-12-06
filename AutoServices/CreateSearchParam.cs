using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServices
{
    public class CreateSearchParam
    {
        
        public static string ReturnQuery(string query)
        {
            string[] splitquery = query.Split('&');
            string AtQuery = "";
            foreach (string str in splitquery)
            {
                string[] splitstr = str.Split('=');
                switch (splitstr[0].ToLower())
                {

                   /* case "rc":
                        request[0] = splitquery[1];
                        break;*/
                   
                    case "driving":
                        AtQuery = AtQuery + Driving(splitstr[1]);
                        break;
                    case "size":
                        AtQuery = AtQuery +Size(splitstr[1]);
                        break;
                    case "seat":
                        AtQuery = AtQuery + "&Keywords=" + splitstr[1];
                        break;
                    case "speed":
                        AtQuery = AtQuery + Speed(splitstr[1]);
                    
                        break;
                    case "range":
                        if (query.Contains("budget_type=ls"))
                        {
                            string[] range = splitstr[1].Split('-');
                            AtQuery = AtQuery + "AT2_Minimum_Price_GBP=" + range[0] + "&AT2_Maximum_Price_GBP=" + range[1];
                        }
                        else if (query.Contains("budget_type=mr"))
                        {
                            string mr = budgettype(splitstr[1]);
                            string[] range = mr.Split('-');
                            AtQuery = AtQuery + "AT2_Minimum_Price_GBP=" + range[0] + "&AT2_Maximum_Price_GBP=" + range[1];
                        }

                        break;
                    case "colour":
                        if (splitstr[1].ToLower() == "any")
                        { }
                        else
                        {
                            AtQuery = AtQuery + "&Colour=" + splitstr[1];
                        }
                        break;
                    case "page_number":
                        AtQuery = AtQuery + "&Page_Number=" + splitstr[1];
                        break;
                    case "page_size":
                       AtQuery = AtQuery + "&Page_Size=" + splitstr[1];
                        break;
                    
                }
            }

            return AtQuery;
        }
        public static string budgettype(string value)
        {
            string[] fromto = value.Split('-');
            int minprice = Convert.ToInt32(fromto[0]);
            int maxprice = Convert.ToInt32(fromto[1]);
            string returnstr = "";//"AT2_Minimum_Price_GBP=" + minprice.ToString() + "&AT2_Maximum_Price_GBP=" + maxprice.ToString();


            minprice = MR(minprice);
            maxprice = MR(maxprice);

            returnstr = minprice.ToString() + "-" + maxprice.ToString();


            return returnstr;

        }
        /*The formula is M = P * ( J / (1 - (1 + J)^ -N)).
M: monthly payment
P: principal or amount of loan
J: monthly interest; annual interest divided by 100, then divided by 12.
N: number of months of amortization, determined by length in years of loan.
        */
        public static int MR(int P)
        {
            double J = 0.005;
            double N = -24;
            double addJ = 1 + J;
            double RP = Math.Pow(addJ, N);
            double SRP = 1 - RP;
            double JpRP = J / SRP;
            double MRP = P / JpRP;
            return Convert.ToInt32(MRP);

        }

        public static string Driving(string condition)
        {
            string query = "";
            switch (condition)
            {
                case "city":
                    query = query + city_Driving();
                    break;
                case "motorway":
                    query = query + motorway_Driving();
                    break;
                case "offroad":
                    query = query + offroad_Driving();
                    break;

            }
            return query;
   
        }

        public static string offroad_Driving()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=suv");
            

            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }

        public static string motorway_Driving()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=saloon");
            cars.Add("&Body_Type=coupe");
            cars.Add("&Body_Type=convertible");
            

            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }

        public static string city_Driving()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=hatchback");
            cars.Add("&Engine_Size_(Cars)=1L - 1.3L");
            cars.Add("&Engine_Size_(Cars)=Less than 1L");
            cars.Add("&CO2_Emissions=Up to 100 g / km CO2");
            cars.Add("&CO2_Emissions=Up to 110 g / km CO2");

            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }

        public static string Size(string condition)
        {
            string query = "";
            switch (condition)
            {
                case "small":
                    query = query + small_Size();
                    break;
                case "medium":
                    query = query + medium_Size();
                    break;
                case "big":
                    query = query + big_Size();
                    break;

            }
            return query;

        }

        public static string small_Size()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=hatchback");
            cars.Add("&Engine_Size_(Cars)=1L - 1.3L");
            cars.Add("&Engine_Size_(Cars)=Less than 1L");
           
            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }

        public static string medium_Size()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=saloon");
            cars.Add("&Body_Type=coupe");
            cars.Add("&Body_Type=convertible");
            cars.Add("&Engine_Size_(Cars)=1.4L - 1.6L");
            cars.Add("&Engine_Size_(Cars)=1.7L - 1.9L");
            cars.Add("&Engine_Size_(Cars)=2L - 2.5L");

            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }
        public static string big_Size()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("&Body_Type=estate");
            cars.Add("&Body_Type=mpv");
            cars.Add("&Body_Type=suv");           
            cars.Add("&Body_Type=estate");

            foreach (string car in cars)
            {
                query = query + car;
            }
            return query;
        }
        public static string Speed(string condition)
        {
            string query = "";
            switch(condition.ToLower())
            {
                case "nfs":
                  query = query +  NFS_Speed();
                    break;
                case "fast":
                    query = query + Fast_Speed();
                    break;
                case "average":
                    query = query + Fast_Speed();
                    break;
                case "any":
                    query = "";
                    break;
            }
            return query;
        }
        public static string NFS_Speed()
        {
            List <string> cars = new List<string>();
            string query = "";
            cars.Add("Aston Martin");
            cars.Add("Bugatti");
            cars.Add("Corvette");
            cars.Add("Ferrari");
            cars.Add("Lamborghini");
            cars.Add("Lotus");
            cars.Add("McLaren");
            cars.Add("Porsche");
            cars.Add("TVR");
            /*/Addition begins here (Specifying MAke/Model)
            List<string> cars2 = new List<string>();
            string query2 = "";
            cars2.Add("BMW_I8");
            cars2.Add("BMW_M ROADSTER");
            cars2.Add("ALFA ROMEO_4C");
            cars2.Add("ALFA ROMEO_8C");
            cars2.Add("AUDI_R8");
            cars2.Add("CHEVROLET_CORVETTE");
            cars2.Add("CHEVROLET_CAMARO");
            cars2.Add("FORD_MUSTANG");
            cars2.Add("FORD_GT");
            cars2.Add("JAGUAR_E-TYPE");
            cars2.Add("JAGUAR_F-TYPE");
            cars2.Add("JAGUAR_XK");
            cars2.Add("JAGUAR_XK8");
            cars2.Add("JAGUAR_XKR-S");
            cars2.Add("JAGUAR_XKR");
            cars2.Add("JAGUAR_XJ220");
            cars2.Add("MASERATI_GRANTURISMO");
            cars2.Add("MERCEDES-BENZ_SLR");
            cars2.Add("MERCEDES-BENZ_SLK");
            cars2.Add("MERCEDES-BENZ_SLS");
            cars2.Add("NISSAN_GT-R");
            cars2.Add("NISSAN_SKYLINE");
            cars2.Add("DODGE_CHALLENGER");
            cars2.Add("DODGE_VIPER");
            foreach (string car in cars2)
            {
                query2 = query2 + "&Make_Model=" + car;
            }
            //Addition ends here*/
            foreach(string car in cars)
            {
                query = query + "&Make=" + car;
            }

                return query;
        }

        public static string Fast_Speed()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("1.7L - 1.9L");
            cars.Add("2L - 2.5L");
          
            foreach (string car in cars)
            {
                query = query + "&Engine_Size_(Cars)=" + car;
            }

            return query;
        }
        public static string Average_Speed()
        {
            List<string> cars = new List<string>();
            string query = "";
            cars.Add("Less than 1L");
            cars.Add("1L - 1.3L");
            cars.Add("1.4L - 1.6L");

            foreach (string car in cars)
            {
                query = query + "&Engine_Size_(Cars)=" + car;
            }

            return query;
        }


        public static string GetSeats(string description)
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

    }
}