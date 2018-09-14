using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public class PlaceShip : MonoBehaviour
{
    public int lobby_id;
    public int parent_id;

    public void Place(ArrayList blocks)
    {
        Info info;
        lobby_id = PlayerPrefs.GetInt("Lobby", 0);
        parent_id = PlayerPrefs.GetInt("ID", 0);

        // Create info instance to create JSON, based on block count
        if (blocks.Count == 2) 
            info = new Info(lobby_id, parent_id, (string)blocks[0], (string)blocks[1], "0", "0", blocks.Count);
        else if (blocks.Count == 3) 
            info = new Info(lobby_id, parent_id, (string)blocks[0], (string)blocks[1], (string)blocks[2], "0", blocks.Count);
        else 
            info = new Info(lobby_id, parent_id, (string)blocks[0], (string)blocks[1], (string)blocks[2], (string)blocks[3], blocks.Count);
            
        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        string result;

        // Make HttpWebRequest to AddShip page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/AddShip.php") as HttpWebRequest;

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
        Status status = JsonConvert.DeserializeObject<Status>(result);
        

    }

    // Class to hold info that will be turned into JSON
    public class Info
    {
        public int parent_id;
        public int lobby_id;
        public string Block1;
        public string Block2;
        public string Block3;
        public string Block4;
        public int blocks;

        public Info(int lobby_id, int parent_id, string Block1, string Block2, string Block3, string Block4, int blocks)
        {
            this.parent_id = parent_id;
            this.lobby_id = lobby_id;
            this.Block1 = Block1;
            this.Block2 = Block2;
            this.Block3 = Block3;
            this.Block4 = Block4;
            this.blocks = blocks;

        }
    }
    
    // Class to hold Status that will come from a JSON 
    public class Status
    {
        public string status;
    }
    
}