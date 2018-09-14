using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System;

public class UIJoinLobby : MonoBehaviour
{
    public UIInput lobby;
    public string Login;
    public int parent_id;
    public int ready = 0;
    public UILabel message;

    public void JoinLobby()
    {
        Login = PlayerPrefs.GetString("Name", "guest");
        parent_id = PlayerPrefs.GetInt("ID", 0);
        string lobbyNum = CleanForJSON(lobby.value);
        int lob = Int32.Parse(lobbyNum);
        PlayerPrefs.SetInt("Lobby", lob);

        // Create JoinInfo instance with username and hashed password
        JoinInfo info = new JoinInfo(lob, Login, parent_id, ready);

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);
        Debug.Log(jsonPayload);
        message.text = jsonPayload;

        string result;
        
        // Make HttpWebRequest to Login page
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

        // Response variable holds response from JSON
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        // Save string from JSON to result
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        // Convert JSON into instance of ReturnInfo type
        ReturnInfo ReturnInfo = JsonConvert.DeserializeObject<ReturnInfo>(result);

        message.text = ReturnInfo.error;

        PlayerPrefs.SetFloat("HLat", ReturnInfo.Lat_lon[0]);
        PlayerPrefs.SetFloat("HLong", ReturnInfo.Lat_lon[1]);
        PlayerPrefs.SetString("Time Limit", ReturnInfo.Lat_lon[2].ToString());

        GPS.latCenter = ReturnInfo.Lat_lon[0];
        GPS.lonCenter = ReturnInfo.Lat_lon[1];
        Debug.Log(ReturnInfo.Lat_lon[0] + " " + ReturnInfo.Lat_lon[1]);
        GPS.centerFound = true;
    }

    // Clas to hold Login info that will be turned into JSON
    public class JoinInfo
    {
        public string login;
        public int lobby_id;
        public int parent_id;
        public int ready;

        public JoinInfo(int lobby_id, string Login, int parent_id, int rdy)
        {
            this.login = Login;
            this.lobby_id = lobby_id;
            this.parent_id = parent_id;
            this.ready = rdy;
        }
    }

    // Class to hold ReturnInfo that will come from a JSON 
    public class ReturnInfo
    {
        public float[] Lat_lon = new float[3];
        public string error;
    }


    public static string CleanForJSON(string s)
    {
        if (s == null || s.Length == 0)
        {
            return "";
        }

        char c = '\0';
        int i;
        int len = s.Length;
        StringBuilder sb = new StringBuilder(len + 4);
        string t;

        for (i = 0; i < len; i += 1)
        {
            c = s[i];
            switch (c)
            {
                case '\\':
                case '"':
                    sb.Append('\\');
                    sb.Append(c);
                    break;
                case '/':
                    sb.Append('\\');
                    sb.Append(c);
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                default:
                    if (c < ' ')
                    {
                        t = "000" + string.Format("X", c);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }

}
