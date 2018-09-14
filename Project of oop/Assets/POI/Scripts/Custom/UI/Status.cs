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

public class Status : MonoBehaviour
{
    public int lobby_id;
    public UILabel player1;
    public UILabel player2;
    public UILabel player3;
    public UILabel player4;
    public UILabel player5;
    public UILabel player6;
    public UILabel player7;
    public UIToggle player1R;
    public UIToggle player2R;
    public UIToggle player3R;
    public UIToggle player4R;
    public UIToggle player5R;
    public UIToggle player6R;
    public UIToggle player7R;

    public UILabel LobbyNum;

    public GameObject notAHost;


    public static Status Instance { get; private set; }

    public void Start()
    {
        for (int i = 1; i < 8; i++)
        {
            if (player1.text != "Unknown")
            {
                player1.text = "Unknown";
            }
            else if (player2.text != "Unknown")
            {
                player2.text = "Unknown";
            }
            else if (player3.text != "Unknown")
            {
                player3.text = "Unknown";
            }
            else if (player4.text != "Unknown")
            {
                player4.text = "Unknown";
            }
            else if (player5.text != "Unknown")
            {
                player5.text = "Unknown";
            }
            else if (player6.text != "Unknown")
            {
                player6.text = "Unknown";
            }
            else if (player7.text != "Unknown")
            {
                player7.text = "Unknown";
            }
        }
        string lobby = "Lobby " + PlayerPrefs.GetInt("Lobby", 0).ToString();
        LobbyNum.text = lobby;
        Invoke("CheckStatus",30);

    }

    public void CheckStatus()
    {
        // LOBBY ID 
        lobby_id = PlayerPrefs.GetInt("Lobby", 0);

        // PREPARE JSON
        string jsonPayload = JsonConvert.SerializeObject(new JsonInput(lobby_id));

        string result;

        LobbyNum.text = "Lobby" + lobby_id.ToString();

        // PREPARE WEBSITE
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/LobbyStatus.php") as HttpWebRequest;


        // PREPARE REQUESTS
        request.ContentType = "application/json";
        request.Method = "POST";

        // SEND JSON
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {

            streamWriter.Write(jsonPayload);
            streamWriter.Flush();
            streamWriter.Close();
        }

        // BREAKS HERE AFTER A FEW ITERATIONS, WHEN REQUESTING FROM DB
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        ResultSet resultSet = JsonConvert.DeserializeObject<ResultSet>(result);
        resultSet.ParseResults();

        for (int i = 0; i < resultSet.userNames.Count; i++)
        {
            if (player1.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player1.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player1R.value = true;
                Debug.Log(player1.text);
                Debug.Log(player1R.value);
            }
            else if (player2.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player2.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player2R.value = true;
            }
            else if (player3.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player3.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player3R.value = true;
            }
            else if (player4.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player4.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player4R.value = true;
            }
            else if (player5.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player5.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player5R.value = true;
            }
            else if (player6.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player6.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player6R.value = true;
            }
            else if (player7.text == "Unknown" && (string)resultSet.userNames[i] != PlayerProfile.playerName)
            {
                player7.text = (string)resultSet.userNames[i];
                if (resultSet.readyStates[i].ToString() == "1")
                    player7R.value = true;
            }
        }


        if (resultSet.start == 1)
            {
             notAHost.SendMessage("OnClick");
            }

    }

    public class JsonInput
    {
        public int lobby_id;

        public JsonInput(int lobby_id)
        {
            this.lobby_id = lobby_id;
        }
    }

	public class ResultSet
    {
        public List<string> userNames = new List<string>();

        public ArrayList readyStates = new ArrayList();
        public int start = 0;


        public string[] results = new string[18];
        

        public void ParseResults()
        {
            int i = 0;

            int length = int.Parse(results[0]);

            for (i = 1; i < (length + 1); i++)
            {
                    if (i == length)
                    {
                        start = int.Parse(results[i]);
                    }
                    else if (i % 2 == 0)
                    {
                        readyStates.Add(results[i]);
                    }
                    else
                    {
                        userNames.Add(results[i]);
                    }

            }

        }
    }







}