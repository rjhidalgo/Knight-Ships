using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;


public class Create: MonoBehaviour
{
    static public int lobby;
    static public int id;
    static public string username;
    static int freddy;
    static public int timer;
    static public int player_count;
    static public float latitude;
    static public float longitude;
    public Text message;
    

    public void CreateLobby()
    {
        // Get center of lobby
        //GPS.Instance.getCenter();
        //GPS.Instance.centerFound = true;


        lobby = PlayerPrefs.GetInt("Lobby", 0);
        id = PlayerPrefs.GetInt("ID", 0);
        username = PlayerPrefs.GetString("Name", "Guest");
        timer = PlayerPrefs.GetInt("Time Limit", 15);
        latitude = PlayerPrefs.GetFloat("HLat", 0);
        longitude = PlayerPrefs.GetFloat("HLong", 0);

        //timer = 15;
        player_count = PlayerPrefs.GetInt("", 0);

        //LobbiesInfo info2 = new LobbiesInfo(user, timer, player_count, GPS.Instance.latCenter, GPS.Instance.lonCenter);

        LobbiesInfo info2 = new LobbiesInfo(username, timer, player_count, latitude, longitude);

        // Create JSON out of info
        string jsonPayload2 = JsonConvert.SerializeObject(info2);

        // Make HttpWebRequest to AddUser page
        HttpWebRequest request2 = WebRequest.Create("http://cop4331project.com/AddLobbies.php") as HttpWebRequest;

        // Set type to JSON and method to post
        request2.ContentType = "application/json";
        request2.Method = "POST";

        // Send JSON to php file 
        using (var streamWriter2 = new StreamWriter(request2.GetRequestStream()))
        {

            streamWriter2.Write(jsonPayload2);
            streamWriter2.Flush();
            streamWriter2.Close();
        }

        string result2;

        // Response variable holds response from JSON
        HttpWebResponse response2 = request2.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
        using (var streamReader2 = new StreamReader(response2.GetResponseStream()))
        {
            result2 = streamReader2.ReadToEnd();
        }

        // Convert JSON into instance of UserInfo type
        UserIdInfo userInfo = JsonConvert.DeserializeObject<UserIdInfo>(result2);
        lobby = userInfo.Host_Lobby_id[0];
        freddy = 0;

        // Create UserInfo instance with username and hashed password
        UserInfo info = new UserInfo(lobby, id, username, freddy);

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        // Make HttpWebRequest to AddUser page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/AddLobby.php") as HttpWebRequest;

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
    }

    public class UserInfo
    {
        public int lobby_id;
        public int parent_id;
        public string login;
        public int ready;
        public UserInfo(int lobby, int id, string username, int freddy)
        {
            lobby_id = lobby;
            parent_id = id;
            login = username;
            ready = freddy;
        }

    }

    public class LobbiesInfo
    {
        //public int lobby_id;
        public string login;
        public int time;
        public int player_limit;
        public float host_lat;
        public float host_lon;
        public string gameName;
        //public LobbiesInfo(int lobby, string playName, int timer, int player_count, float latitude, float longitude)
        public LobbiesInfo(string user, int timer, int player_count, float latitude, float longitude)
        {
            //lobby_id = lobby;
            gameName = user;
            //login = user;
            time = timer;
            player_limit = player_count;
            host_lat = latitude;
            host_lon = longitude;
        }

    }

    public class Error
    {
        public string error;
    }

    // Class to hold UserInfo that will come from a JSON 
    public class UserIdInfo
    {
        //  public int searchCount;
        //  public int searchResults;
        //  public string error;

        public int[] Host_Lobby_id = new int[2];
    }


}