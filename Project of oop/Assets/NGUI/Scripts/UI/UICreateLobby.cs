using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;


public class UICreateLobby: MonoBehaviour
{
    static public int lobby;
    static public string username;
    static public string timer;
    static public string player_count;
    static public float latitude;
    static public float longitude;
    
    public void CreateLobby()
    {
        // Get center of lobby

        GPS.getCenter();

        GPS.centerFound = true;

        Players();
        TimeLimit();

        username = PlayerPrefs.GetString("Name", "Guest");
        latitude = GPS.latCenter;
        longitude = GPS.lonCenter;

        PlayerPrefs.SetFloat("HLong", longitude);
        PlayerPrefs.SetFloat("HLat", latitude);
        PlayerPrefs.SetInt("Host", 1);

        LobbiesInfo lobbyInfo = new LobbiesInfo(username, timer, player_count, latitude, longitude);

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(lobbyInfo);
        // Make HttpWebRequest to AddUser page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/AddLobbies.php") as HttpWebRequest;

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

        string result;

        // Response variable holds response from JSON
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        // Convert JSON into instance of UserInfo type
        UserIdInfo userInfo = JsonConvert.DeserializeObject<UserIdInfo>(result);
        PlayerPrefs.SetInt("Lobby", userInfo.Host_Lobby_id[0]);



        // NOW JOIN THE LOBBY

        // Create JoinInfo instance with username and hashed password
        JoinInfo jsonInfo = new JoinInfo(userInfo.Host_Lobby_id[0].ToString(), PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("ID"), 0);
        
        // Create JSON out of info
        string jsonPayload2 = JsonConvert.SerializeObject(jsonInfo);

        // Make HttpWebRequest to Login page
        HttpWebRequest request2 = WebRequest.Create("http://cop4331project.com/AddLobby.php") as HttpWebRequest;

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


    }

    public class JoinInfo
    {
        public string login;
        public string lobby_id;
        public int parent_id;
        public int ready;

        public JoinInfo(string lobby_id, string Login, int parent_id, int rdy)
        {
            this.login = Login;
            this.lobby_id = lobby_id;
            this.parent_id = parent_id;
            this.ready = rdy;
        }
    }

    public class LobbiesInfo
    {
        public string time;
        public string player_limit;
        public float host_lat;
        public float host_lon;
        public string gameName;

        public LobbiesInfo(string user, string timer, string player_limit, float latitude, float longitude)
        {
            gameName = user;
            time = timer;
            this.player_limit = player_limit;
            host_lat = latitude;
            host_lon = longitude;
        }

    }

    // Class to hold UserInfo that will come from a JSON 
    public class UserIdInfo
    {
        public int[] Host_Lobby_id = new int[1];
    }

    public void Players()
    {
      if (UIPopupList.current != null)
		{
			player_count = UIPopupList.current.isLocalized?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
		}
    }

    public void TimeLimit()
    {
        if (UIPopupList.current != null)
        {
            timer = UIPopupList.current.isLocalized ?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
        }
    }
}