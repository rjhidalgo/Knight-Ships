using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{

    public static GPS Instance { set; get; }

    public static float latitude;
    public static float longitude;

    public static float latCenter;
    public static float lonCenter;

    public static int xcoor;
    public static int ycoor;

    public static bool centerFound = false;

    public static float directionA;
    public static string direction;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());

    }


    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enbaled GPS");
            yield break;
        }


        Input.location.Start();
        Input.compass.enabled = true;
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

         
        //PlayerIcon.transform.Translate(500, 600, 0);

        yield break;
    }

    public static void getCenter()
    {
        latCenter = latitude;
        lonCenter = longitude;
    }

    private void Update()
    {
        //PlayerIcon.transform.Translate(latitude + 538, longitude  + 653, 0);
                  
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        float latPlayer = latitude;
        float lonPlayer = longitude;

        if (centerFound == true)
        {
            xcoor = findX(latCenter, latPlayer);
            ycoor = findY(lonCenter, lonPlayer);
        }
       /* else
        {
            latCenter = 
        }*/

        directionA = Input.compass.trueHeading;
        direction = findDirection(directionA);
    }

    static int findX(float latCenter, float latPlayer)
    {
        float latCenterLeft = latCenter - .00005f;
        float latCenterRight = latCenter + .00005f;


        float lat1, lat2, lat3, lat4, lat5, lat7, lat8, lat9, lat10, lat11;

        // Left X-axis values
        lat1 = latCenterLeft - .0005f;
        lat2 = latCenterLeft - .0004f;
        lat3 = latCenterLeft - .0003f;
        lat4 = latCenterLeft - .0002f;
        lat5 = latCenterLeft - .0001f;

        // Right X - axis values
        lat7 = latCenterRight + .0001f;
        lat8 = latCenterRight + .0002f;
        lat9 = latCenterRight + .0003f;
        lat10 = latCenterRight + .0004f;
        lat11 = latCenterRight + .0005f;

        // Check if Left of Center
        if (latPlayer >= lat1 && latPlayer < lat2)
            return 1;

        else if (latPlayer >= lat2 && latPlayer < lat3)
            return 2;

        else if (latPlayer >= lat3 && latPlayer < lat4)
            return 3;

        else if (latPlayer >= lat4 && latPlayer < lat5)
            return 4;

        else if (latPlayer >= lat5 && latPlayer < latCenterLeft)
            return 5;

        // Check if in Center
        else if (latPlayer >= latCenterLeft && latPlayer < latCenterRight)
            return 6;

        // Check if Right of Center
        else if (latPlayer >= latCenterRight && latPlayer < lat7)
            return 7;

        else if (latPlayer >= lat7 && latPlayer < lat8)
            return 8;

        else if (latPlayer >= lat8 && latPlayer < lat9)
            return 9;

        else if (latPlayer >= lat9 && latPlayer < lat10)
            return 10;

        else if (latPlayer >= lat10 && latPlayer <= lat11)
            return 11;

        return 0;
    }

    static int findY(float lonCenter, float lonPlayer)
    {
        float lonCenterDown = lonCenter - .00005f;
        float lonCenterUp = lonCenter + .00005f;


        float lon1, lon2, lon3, lon4, lon5, lon7, lon8, lon9, lon10, lon11;

        // Left y-axis values
        lon1 = lonCenterDown - .0005f;
        lon2 = lonCenterDown - .0004f;
        lon3 = lonCenterDown - .0003f;
        lon4 = lonCenterDown - .0002f;
        lon5 = lonCenterDown - .0001f;

        // Right y - axis values
        lon7 = lonCenterUp + .0001f;
        lon8 = lonCenterUp + .0002f;
        lon9 = lonCenterUp + .0003f;
        lon10 = lonCenterUp + .0004f;
        lon11 = lonCenterUp + .0005f;

        // Check if Below of Center
        if (lonPlayer >= lon1 && lonPlayer < lon2)
            return 1;

        else if (lonPlayer >= lon2 && lonPlayer < lon3)
            return 2;

        else if (lonPlayer >= lon3 && lonPlayer < lon4)
            return 3;

        else if (lonPlayer >= lon4 && lonPlayer < lon5)
            return 4;

        else if (lonPlayer >= lon5 && lonPlayer < lonCenterDown)
            return 5;

        // Check if in Center
        else if (lonPlayer >= lonCenterDown && lonPlayer < lonCenterUp)
            return 6;

        // Check if Right of Center
        else if (lonPlayer >= lonCenterUp && lonPlayer < lon7)
            return 7;

        else if (lonPlayer >= lon7 && lonPlayer < lon8)
            return 8;

        else if (lonPlayer >= lon8 && lonPlayer < lon9)
            return 9;

        else if (lonPlayer >= lon9 && lonPlayer < lon10)
            return 10;

        else if (lonPlayer >= lon10 && lonPlayer <= lon11)
            return 11;

        return 0;
    }

    static string findDirection(float directionA)
    {
        if (directionA > 337 || directionA <= 22)
            return "N";
        else if (directionA > 22 && directionA <= 67)
            return "NE";
        else if (directionA > 67 && directionA <= 112)
            return "E";
        else if (directionA > 112 && directionA <= 157)
            return "SE";
        else if (directionA > 157 && directionA <= 202)
            return "S";
        else if (directionA > 202 && directionA <= 247)
            return "SW";
        else if (directionA > 247 && directionA <= 292)
            return "W";
        else if (directionA > 292 && directionA <= 337)
            return "NW";

        return "Unable to determine direction";
    }
}
