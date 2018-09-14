using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;


public class AddUser : MonoBehaviour
{
    public UIInput username;
    public UIInput password;
    public UIInput confirm;
    public UILabel message;

	public void Add()
    {
        if (password.value == confirm.value)
        {
            // Create UserInfo instance with username and hashed password
            UserInfo info = new UserInfo(cleanForJSON(username.value), CalculateMD5Hash(cleanForJSON(password.value)));

            // Create JSON out of info
            string jsonPayload = JsonConvert.SerializeObject(info);

            // Make HttpWebRequest to AddUser page
            HttpWebRequest request = WebRequest.Create("http://cop4331project.com/AddUser.php") as HttpWebRequest;

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

            // Convert JSON into instance of Error type
            Error error = JsonConvert.DeserializeObject<Error>(result);

            // Print error
            message.text = error.error;
        }
        else
            message.text = "passwords don't match";
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

    public class UserInfo
    {
        public string Login;
        public string Password;

        public UserInfo(string username, string password)
        {
            Login = username;
            this.Password = password;
        }
    }

    public class Error
    {
        public string error;
    }

    public static string cleanForJSON(string s)
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



