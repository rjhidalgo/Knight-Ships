using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

public class Start : MonoBehaviour
{

    public int lobby_id;


    public void CheckLogin()
    {

        lobby_id = PlayerPrefs.GetInt("Lobby", 0);

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(lobby_id);

        // string result;
        
        // Make HttpWebRequest to Login page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/SetStart.php") as HttpWebRequest;

        // Set type to JSON and method to post
        request.ContentType = "application/json";
        request.Method = "POST";

        // Send JSON to php file 
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
    
            streamWriter.Write(jsonPayload);
            streamWriter.Flush();
            streamWriter.Close();
        }

        // Response variable holds response from JSON
        // HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
      /*  using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        } */

        // Convert JSON into instance of StartInfo type
        // StartInfo startInfo = JsonConvert.DeserializeObject<StartInfo>(result);

        
    }
    

    // Error info
    public class StartInfo
    {
       
        public string Status;

    }

    


}
