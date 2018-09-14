using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public class UILeaveLobby : MonoBehaviour
{
    public int lobby_id;

    public void LeaveLobby()
    {
        string extension;

        Info info;

        lobby_id = PlayerPrefs.GetInt("Lobby", 0);

        // If user is the host, send the lobby id
        if (PlayerPrefs.GetInt("Host", 0) == 1)
        {
            info = new Info(lobby_id, PlayerPrefs.GetInt("ID", 0));
            extension = "DelLobbies.php";
        }
        // Else, they aren't host, send user id 
        else
        {
            info = new Info(0, PlayerPrefs.GetInt("ID", 0));
            extension = "DelLobby.php";
        }

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

       // string result;

        // Make HttpWebRequest to Login page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/" + extension) as HttpWebRequest;

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

      /*  // Response variable holds response from JSON
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        // Convert JSON into instance of UserInfo type
        Error error = JsonConvert.DeserializeObject<Error>(result);*/
    }

    // Class to hold info that will be turned into JSON
    public class Info
    {
        public int lobby_id;
        public int parent_id;

        public Info(int lobby_id, int parent_id)
        {
            this.parent_id = parent_id;
            this.lobby_id = lobby_id;
        }
    }

    // Class to hold Error that will come from a JSON 
    public class Error
    {
        public string error;
    }
}