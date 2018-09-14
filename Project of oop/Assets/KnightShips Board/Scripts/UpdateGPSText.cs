using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public Text Attack;

    private void Update()
    {
        if (AttackButton.counter == 1)
        {
            Attack.text = AttackButton.timer.ToString("0.00");
        }
        else
            Attack.text = "Attack";

        if (GPS.xcoor == 0)
            coordinates.text = "LatC: " + GPS.latCenter.ToString() + "   LonC: " + GPS.lonCenter.ToString() +  "   Lat: " + GPS.latitude.ToString() + "   Lon: " + GPS.longitude.ToString() +
               "\n X Coordinate out of bounds";
        else if (GPS.ycoor == 0)
            coordinates.text = "LatC: " + GPS.latCenter.ToString() + "   LonC: " + GPS.lonCenter.ToString() + "   Lat: " + GPS.latitude.ToString() + "   Lon: " + GPS.longitude.ToString() +
               "\n Y Coordinate out of bounds";
        else
            coordinates.text = "LatC: " + GPS.latCenter.ToString() + "   LonC: " + GPS.lonCenter.ToString() + "   Lat: " + GPS.latitude.ToString() + "   Lon: " + GPS.longitude.ToString() + "Dir: " + GPS.direction +
            "\n(x,y) = (" + GPS.xcoor.ToString() + "," + GPS.ycoor.ToString() + ")";
    }



}
