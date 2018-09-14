using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public class AttackShip : MonoBehaviour
{
    public int lobby_id;
    public int parent_id;
    public Status status;
    public string winner;

    // Returns 0 for miss, 1 for hit, and 2 to end the game.  If return is 2, check AttackShip.winner field for winner's name
    public bool Attack(string att_location)
    {
        do
        {
            AttackHelper(att_location);

            if (status.status.Equals("miss"))
                return false;
            else if (status.status.Equals("hit"))
                return true;
            else
                continue;
            
        } while (status.status.Equals("locked_please_try_again"));

        return false;
    }

    public void AttackHelper(string att_location)
    {

        lobby_id = PlayerPrefs.GetInt("Lobby", 1);
        parent_id = PlayerPrefs.GetInt("ID", 0);


        // Create info instance to create JSON
        Info info = new Info(lobby_id, parent_id, att_location);

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        string result;

        // Make HttpWebRequest to AddShip page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/AttackShip.php") as HttpWebRequest;

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

        // Convert JSON into instance of status type
        status = JsonConvert.DeserializeObject<Status>(result);
    }

    // Class to hold info that will be turned into JSON
    public class Info
    {
        public int lobby_id;
        public int parent_id;
        public string att_location;

        public Info(int lobby_id, int parent_id, string att_location)
        {
            this.parent_id = parent_id;
            this.lobby_id = lobby_id;
            this.att_location = att_location;

        }
    }

    // Class to hold Status that will come from a JSON 
    public class Status
    {
        public string status;
    }
}