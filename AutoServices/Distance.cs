using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace AutoServices
{
    public class DistanceFromCar
    {
        public static string GetPostCode4rmUri(string uri)
        {
            string[] splitquery = uri.Split('&');
            string postcode = "";
            foreach (string str in splitquery)
            {
                string[] splitstr = str.Split('=');
                switch (splitstr[0].ToLower())
                {

                    /* case "rc":
                         request[0] = splitquery[1];
                         break;*/
                    case "postcode":
                        postcode = splitstr[1];
                        break;
                }
            }
            return postcode;
        }


        public static string GetDistance(string Source, string Destination)
        {
            string responseText;
            string pageURL = null;
            HttpWebRequest request;

            pageURL = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + Source + "&destinations=" + Destination + "&mode=driving&units=imperial";

            request = (HttpWebRequest)
                        WebRequest.Create(pageURL);
            try
            {
                HttpWebResponse result = (HttpWebResponse)request.GetResponse();
                StreamReader reader = null;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    reader = new StreamReader(result.GetResponseStream());
                    string json2 = reader.ReadToEnd();
                    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json2));

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
                    RootObject dsjson = ser.ReadObject(ms) as RootObject;
                    //return dsjson.distance.text ;
                    string distance = dsjson.rows[0].elements[0].distance.text;
                   
                    return distance;

                }
                else
                {
                    return "Unavailable";
                }
                reader.Close();
                result.Close();
            }
            catch
            {
                return "Unavailable";
            }
        }


        public static string GetTravelTime(string Source, string Destination)
        {
            string responseText;
            string pageURL = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + Source + "&destinations=" + Destination + "&mode=driving&units=imperial";

            HttpWebRequest request = (HttpWebRequest)
                         WebRequest.Create(pageURL);
            try
            {
                HttpWebResponse result = (HttpWebResponse)request.GetResponse();
                StreamReader reader = null;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    reader = new StreamReader(result.GetResponseStream());
                    string json2 = reader.ReadToEnd();
                    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json2));

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
                    RootObject dsjson = ser.ReadObject(ms) as RootObject;

                    return dsjson.rows[0].elements[0].duration.text;
                }
                else
                {
                    return "Unavailable";
                }
                reader.Close();
                result.Close();
            }
            catch
            {
                return "Unavailable";
            }
        }


        public static string posttolatlong(string source)
        {

            string pageURL = null;
            HttpWebRequest request;

            pageURL = "https://maps.googleapis.com/maps/api/geocode/json?address=" + source;
            request = (HttpWebRequest)
                    WebRequest.Create(pageURL);
            try
            {
                HttpWebResponse result = (HttpWebResponse)request.GetResponse();
                StreamReader reader = null;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    reader = new StreamReader(result.GetResponseStream());
                    string json2 = reader.ReadToEnd();
                    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json2));

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Rootobject2));

                    Rootobject2 dsjson = ser.ReadObject(ms) as Rootobject2;

                    //return dsjson.distance.text ;
                    //Source = dsjson.location.lat.ToString() + dsjson.location.lng.ToString();

                    float lati = dsjson.results[0].geometry.location.lat;
                    float longi = dsjson.results[0].geometry.location.lng;
                    // Debug.WriteLine("Source" + Source);
                    source = lati.ToString() + "," + longi.ToString();
                  
                    string source1 = source;
                }

            }
            catch (Exception ex)
            {
                return "Unavailable";

            }
            return source;



        }

    }
}