using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace AutoServices
{
    public class ConnectJSON
    {
        public string access_token { get; set; }
        public string expires { get; set; }
    }
    public class ApiConnect
    {
        public static string key()
        {
            return "autotraderstudent";
        }
        public static string AccessToken { get; set; }

        public static string secret()
        {
            return "OCyXDdPmgpixKHmllKkzQtCCAs9eDK";
        }
        public static string requesturi()
        {
            return "https://api.autotrader.co.uk/service/search/";
        }
        public static string getTokenText()
        {
           
            Dictionary<string, string> students = new Dictionary<string, string>()
{
    { "key", ApiConnect.key()},
    { "secret", ApiConnect.secret()}
};
            string AccessKey = ApiConnect.HttpPostRequest("https://api.autotrader.co.uk/authenticate", students);
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(AccessKey));
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AutoTraderJSON_CsharpClass));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ConnectJSON));
            ConnectJSON dsjson = ser.ReadObject(ms) as ConnectJSON;
            string txtAccessKey = dsjson.access_token;
            Debug.WriteLine(txtAccessKey);
            return txtAccessKey;
        }
        public static string HttpPostRequest(string url, Dictionary<string, string> postParameters)
        {
            string postData = "";

            foreach (string key in postParameters.Keys)
            {
                postData += HttpUtility.UrlEncode(key) + "="
                      + HttpUtility.UrlEncode(postParameters[key]) + "&";
            }

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();

            return pageContent;
        }
        public static string getTokenJSON()
        {
            string uriString;

            uriString = "https://api.autotrader.co.uk/authenticate";
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            //Console.WriteLine("\nPlease enter the data to be posted to the URI {0}:", uriString);
            string postData = ApiConnect.key() + "&" + ApiConnect.secret();
            myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            // Display the headers in the request
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            // Upload the input string using the HTTP 1.0 POST method. 
            byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);

            // Decode and display the response.
            Encoding.ASCII.GetString(responseArray);
            Debug.WriteLine(responseArray);
            return "" + Encoding.ASCII.GetString(responseArray);
        }
        public static string request(string uri, string token)
        {
            string requri = ApiConnect.requesturi() + "" + uri.ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requri);
            //Get the headers associated with the request.
            WebHeaderCollection myWebHeaderCollection = req.Headers;

            //Add the Accept-Language header (for Danish) in the request.
            myWebHeaderCollection.Add("Authorization:" + "Bearer " + token);


            //Print the headers for the request.
            //    printHeaders(myWebHeaderCollection);



            try
            {
                HttpWebResponse result = (HttpWebResponse)req.GetResponse();
                StreamReader reader = null;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    reader = new StreamReader(result.GetResponseStream());
                    return reader.ReadToEnd();
                }
                else
                {
                    return "Failed";
                }

                result.Close();
            }
            catch
            {
                return "Failed";
            }
        }

    }
}