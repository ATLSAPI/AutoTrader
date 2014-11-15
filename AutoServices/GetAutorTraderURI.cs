using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AutoServices
{
    public class GetAutorTraderURI
    {



        public string GetPage(string pageURL)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(pageURL); //page URL

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
                reader.Close();
                result.Close();
            }
            catch
            {
                return "Failed";
            }
        }
        public string GetPageWithParam(string pageURL, string accessToken)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(pageURL); //page URL


            //Get the headers associated with the request.
            WebHeaderCollection myWebHeaderCollection = req.Headers;

            //Add the Accept-Language header (for Danish) in the request.
            myWebHeaderCollection.Add("Access-Token:" + accessToken);


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
                reader.Close();
                result.Close();
            }
            catch
            {
                return "Failed";
            }
        }

    }
}