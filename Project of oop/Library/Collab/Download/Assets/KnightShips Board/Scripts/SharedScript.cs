using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedScript : MonoBehaviour {

    public static int placeShipsMode = 0, orientationMode = 0, shipsPlaced = 0;
    public static bool clickShipsMode, attackMode, attacking, thirty;
    public static float timer, timerStart = 600f;
    public String NumtoChar(int a)
    {
        if (a == 1)
            return "1";
        else if (a == 2)
            return "2";
        else if (a == 3)
            return "3";
        else if (a == 4)
            return "4";
        else if (a == 5)
            return "5";
        else if (a == 6)
            return "6";
        else if (a == 7)
            return "7";
        else if (a == 8)
            return "8";
        else if (a == 9)
            return "9";
        else if (a == 10)
            return "10";
        else if (a == 11)
            return "11";
        else
            return ".";
    }
    public int StrtoInt(String a)
    {
        if (a.Equals("1"))
            return 1;
        else if (a.Equals("2"))
            return 2;
        else if (a.Equals("3"))
            return 3;
        else if (a.Equals("4"))
            return 4;
        else if (a.Equals("5"))
            return 5;
        else if (a.Equals("6"))
            return 6;
        else if (a.Equals("7"))
            return 7;
        else if (a.Equals("8"))
            return 8;
        else if (a.Equals("9"))
            return 9;
        else if (a.Equals("10"))
            return 10;
        else if (a.Equals("11"))
            return 11;
        else
            return 0;
    }
    public String NumtoLetter(int xcoor)
    {
        switch (xcoor)
        {
            case 1:
                return "A";
            case 2:
                return "B";
            case 3:
                return "C";
            case 4:
                return "D";
            case 5:
                return "E";
            case 6:
                return "F";
            case 7:
                return "G";
            case 8:
                return "H";
            case 9:
                return "I";
            case 10:
                return "J";
            case 11:
                return "K";
            default:
                return "Z";
        }
    }
    void Start () {
        clickShipsMode = true;

    }
	
	void Update () {
        timer =  timerStart - Time.time;

        if (timer <= 570.0)
        {
            thirty = true;
        }
        if (timer <= 0.00)
            GetComponent<TimeUp>().End();
    }
}
