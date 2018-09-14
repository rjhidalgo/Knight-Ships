using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public class TimeUp : MonoBehaviour
{
    int lobby_id;

    public ArrayList End()
    {

        lobby_id = PlayerPrefs.GetInt("Lobby", 10);

        Info info = new Info(lobby_id);
		
        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        string result;

        // Make HttpWebRequest to php page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/TimeUp.php") as HttpWebRequest;

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
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        // Convert JSON into instance of UserInfo type
        JsonReturn jsonReturn = JsonConvert.DeserializeObject<JsonReturn>(result);

        return new ArrayList(jsonReturn.Players_NumberBlocks);
    }

    // Class to hold info that will be turned into JSON
    public class Info
    {
        public int lobby_id;

        public Info(int lobby_id)
        {
            this.lobby_id = lobby_id;
        }
    }

    // Class to hold JsonReturn that will come from a JSON 
    public class JsonReturn
    {
        public string [] Players_NumberBlocks;
    }
}