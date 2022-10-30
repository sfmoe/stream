using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

public class CPHInline
{
    public bool Execute()
    {

        var goveeAPI = "GOVEE_API_KEY";
        var goveeAPIURL = "https://developer-api.govee.com/v1/devices/control";
        CPH.LogInfo("Changing Govee Lights....");


		//colors r,g,b
        int[] purple = {111,0,255};
        //array of devices to control {"HEX_ID", "DEVICETYPE"}
        string[][] devices =
        {
            new string[] { "11:11:11:11:11:11:11:AA", "H6110"},
            new string[] { "22:22:22:22:22:22:22:BB", "H6003"},
            new string[] { "33:33:33:33:33:33:33:CC", "H6003"}

        };
		var suerSelectedColor =  args["colorJSON"].ToString().ToLower();
		CPH.LogInfo(suerSelectedColor.ToString());

        
	var selectedColor =  JObject.Parse(suerSelectedColor);
CPH.LogInfo(selectedColor.ToString());

               foreach(string[] dev in devices)
            {
                CPH.LogInfo(dev[0]);

        using (HttpClient client = new HttpClient())
        {
                      var webRequest = (HttpWebRequest)WebRequest.Create(goveeAPIURL);
            webRequest.ContentType = "application/json";
            webRequest.Method = "PUT";
            webRequest.Headers.Add("Govee-API-Key", goveeAPI);
    
  
            var serializer = new JsonSerializer();
            TextWriter goveeJSON = new StringWriter();
            serializer.Serialize(goveeJSON, new
                                    {
                                    device = dev[0], model = dev[1], cmd = new
                                    {
                                    name = "color", value = selectedColor
                                    }
                                    }
                                );
            using (var streamWriter = new System.IO.StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(goveeJSON);
            }
            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
			CPH.LogInfo("wait .5");
			CPH.Wait(500);
			CPH.LogInfo("end wait .5");
			httpResponse.Close();
        }



    }
        return true;
    }

}