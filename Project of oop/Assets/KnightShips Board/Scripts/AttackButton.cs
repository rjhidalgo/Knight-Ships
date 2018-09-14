using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour {
    
    public static int counter = 0;
    public static float timer = 5.0f;

    // Use this for initialization
    void Start() 
    {

    }

    // Update is called once per frame
    void Update() {
        if (counter == 1)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0.0)
        {
            counter = 0;
            timer = 30.0f;

        }
    }

    public void AttackButtonClicked()
    {
        if (counter == 0)
        {


            if (SharedScript.attackMode && SharedScript.thirty)
            {
                
                showRed(GPS.xcoor + 1, GPS.ycoor);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor + 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor));
                showRed(GPS.xcoor - 1, GPS.ycoor);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor - 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor));
                showRed(GPS.xcoor, GPS.ycoor + 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor + 1));
                showRed(GPS.xcoor, GPS.ycoor - 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor - 1));
                showRed(GPS.xcoor + 1, GPS.ycoor + 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor + 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor + 1));
                showRed(GPS.xcoor + 1, GPS.ycoor - 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor + 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor - 1));
                showRed(GPS.xcoor - 1, GPS.ycoor - 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor - 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor - 1));
                showRed(GPS.xcoor - 1, GPS.ycoor + 1);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor - 1) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor + 1));
                showRed(GPS.xcoor, GPS.ycoor);
                ClickedOnBoard.temp.Add(GetComponent<SharedScript>().NumtoLetter(GPS.xcoor) + GetComponent<SharedScript>().NumtoChar(GPS.ycoor));
            }

            SharedScript.attacking = true;
        }

    }

    public void showRed(int xcoor, int ycoor)
    {
        String letter, square;

        switch (xcoor)
        {
            case 1:
                letter = "A";
                break;
            case 2:
                letter = "B";
                break;
            case 3:
                letter = "C";
                break;
            case 4:
                letter = "D";
                break;
            case 5:
                letter = "E";
                break;
            case 6:
                letter = "F";
                break;
            case 7:
                letter = "G";
                break;
            case 8:
                letter = "H";
                break;
            case 9:
                letter = "I";
                break;
            case 10:
                letter = "J";
                break;
            case 11:
                letter = "K";
                break;
            default:
                letter = "Z";
                break;

        }

        square = letter + GetComponent<SharedScript>().NumtoChar(ycoor);

        gameObject.GetComponent<ClickedOnBoard>().showRed(square);
    }
}
