using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public class UIReady : MonoBehaviour
{
    int lobby_id;
    public UIToggle readyFred;
    public Text message;

    public void ToggleReady()
    {

        lobby_id = PlayerPrefs.GetInt("Lobby", 0);

        Info info;
        // Create Instance of type Info
        if (readyFred.value)
        {
            info = new Info(lobby_id, PlayerPrefs.GetInt("ID", 0), 1);
        }
        else
        {
            info = new Info(lobby_id, PlayerPrefs.GetInt("ID", 0), 0);
        }

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        string result;

        // Make HttpWebRequest to Login page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/ToggleReady.php") as HttpWebRequest;

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
        Error error = JsonConvert.DeserializeObject<Error>(result);

        message.text = error.error;
    }

    // Class to hold info that will be turned into JSON
    public class Info
    {
        public int lobby_id;
        public int parent_id;
        public int ready;

        public Info(int lobby_id, int parent_id, int ready)
        {
            this.parent_id = parent_id;
            this.lobby_id = lobby_id;
            this.ready = ready;
        }
    }

    // Class to hold Error that will come from a JSON 
    public class Error
    {
        public string error;
    }
}