using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

public class Login : MonoBehaviour
{

    public UIInput usernameInput;
    public UIInput passwordInput;
    public UILabel message;

    public void CheckLogin()
    {
        // Create LoginInfo instance with username and hashed password
        string usernameString = CleanForJSON(usernameInput.value);
        string passwordString = CleanForJSON(passwordInput.value);

        LoginInfo info = new LoginInfo(usernameString, CalculateMD5Hash(passwordString));

        // Create JSON out of info
        string jsonPayload = JsonConvert.SerializeObject(info);

        string result;
        
        // Make HttpWebRequest to Login page
        HttpWebRequest request = WebRequest.Create("http://cop4331project.com/Login.php") as HttpWebRequest;

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
        UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(result);

        // If username is in database, username id would be greater than or equal to 1
        if (userInfo.id >= 1)
            message.text = "Successfully Logged In";
        else
            message.text = "Username or Password Incorrect";

        if (message.text == "Successfully Logged In")
            PlayerPrefs.SetInt("ID", userInfo.id);

        usernameInput.value = "";
        passwordInput.value = "";
    }

    // Clas to hold Login info that will be turned into JSON
    public class LoginInfo
    {
        public string login;
        public string password;

        public LoginInfo(string username, string password)
        {
            login = username;
            PlayerPrefs.SetString("Name", login);
            this.password = password;
        }
    }

    // Class to hold UserInfo that will come from a JSON 
    public class UserInfo
    {
        public int id;
        public string error;
    }

    // Hash algorithm for password hashing
    public string CalculateMD5Hash(string input)

    {

        // step 1, calculate MD5 hash from input

        MD5 md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(input);

        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)

        {

            sb.Append(hash[i].ToString());

        }

        return sb.ToString();

    }

    public static string CleanForJSON(string s)
    {
        if (s == null || s.Length == 0) {
            return "";
        }

        char         c = '\0';
        int          i;
        int          len = s.Length;
        StringBuilder sb = new StringBuilder(len + 4);
        string t;

        for (i = 0; i < len; i += 1) {
            c = s[i];
            switch (c) {
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
                    if (c < ' ') {
                        t = "000" + string.Format("X", c);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    } else {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }


}
