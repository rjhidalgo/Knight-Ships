using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedOnBoard : MonoBehaviour {
    Vector3 mouse;

    public int xboard, yboard;
    public static ClickedOnBoard Instance { set; get; }
    public SpriteRenderer A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11,C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, E11, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, G11, H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, H11, I1, I2, I3, I4, I5, I6, I7, I8, I9, I10, I11, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, K1, K2, K3, K4, K5, K6, K7, K8, K9, K10, K11;
    public ArrayList Ship = new ArrayList();
    public static ArrayList temp = new ArrayList();
    public bool left, right, up, down;
    public static int screenx, screeny;
    AttackShip attacker = new AttackShip();

    public SpriteRenderer PlayerIcon;

    public static int killCounter = 0;

    // Use this for initialization
    void Start ()
    {
        PlayerIcon.GetComponent<SpriteRenderer>().enabled = false;
        A1.GetComponent<SpriteRenderer>().enabled = false;
        A2.GetComponent<SpriteRenderer>().enabled = false;
        A3.GetComponent<SpriteRenderer>().enabled = false;
        A4.GetComponent<SpriteRenderer>().enabled = false;
        A5.GetComponent<SpriteRenderer>().enabled = false;
        A6.GetComponent<SpriteRenderer>().enabled = false;
        A7.GetComponent<SpriteRenderer>().enabled = false;
        A8.GetComponent<SpriteRenderer>().enabled = false;
        A9.GetComponent<SpriteRenderer>().enabled = false;
        A10.GetComponent<SpriteRenderer>().enabled = false;
        A11.GetComponent<SpriteRenderer>().enabled = false;
        B1.GetComponent<SpriteRenderer>().enabled = false;
        B2.GetComponent<SpriteRenderer>().enabled = false;
        B3.GetComponent<SpriteRenderer>().enabled = false;
        B4.GetComponent<SpriteRenderer>().enabled = false;
        B5.GetComponent<SpriteRenderer>().enabled = false;
        B6.GetComponent<SpriteRenderer>().enabled = false;
        B7.GetComponent<SpriteRenderer>().enabled = false;
        B8.GetComponent<SpriteRenderer>().enabled = false;
        B9.GetComponent<SpriteRenderer>().enabled = false;
        B10.GetComponent<SpriteRenderer>().enabled = false;
        B11.GetComponent<SpriteRenderer>().enabled = false;
        C1.GetComponent<SpriteRenderer>().enabled = false;
        C2.GetComponent<SpriteRenderer>().enabled = false;
        C3.GetComponent<SpriteRenderer>().enabled = false;
        C4.GetComponent<SpriteRenderer>().enabled = false;
        C5.GetComponent<SpriteRenderer>().enabled = false;
        C6.GetComponent<SpriteRenderer>().enabled = false;
        C7.GetComponent<SpriteRenderer>().enabled = false;
        C8.GetComponent<SpriteRenderer>().enabled = false;
        C9.GetComponent<SpriteRenderer>().enabled = false;
        C10.GetComponent<SpriteRenderer>().enabled = false;
        C11.GetComponent<SpriteRenderer>().enabled = false;
        D1.GetComponent<SpriteRenderer>().enabled = false;
        D2.GetComponent<SpriteRenderer>().enabled = false;
        D3.GetComponent<SpriteRenderer>().enabled = false;
        D4.GetComponent<SpriteRenderer>().enabled = false;
        D5.GetComponent<SpriteRenderer>().enabled = false;
        D6.GetComponent<SpriteRenderer>().enabled = false;
        D7.GetComponent<SpriteRenderer>().enabled = false;
        D8.GetComponent<SpriteRenderer>().enabled = false;
        D9.GetComponent<SpriteRenderer>().enabled = false;
        D10.GetComponent<SpriteRenderer>().enabled = false;
        D11.GetComponent<SpriteRenderer>().enabled = false;
        E1.GetComponent<SpriteRenderer>().enabled = false;
        E2.GetComponent<SpriteRenderer>().enabled = false;
        E3.GetComponent<SpriteRenderer>().enabled = false;
        E4.GetComponent<SpriteRenderer>().enabled = false;
        E5.GetComponent<SpriteRenderer>().enabled = false;
        E6.GetComponent<SpriteRenderer>().enabled = false;
        E7.GetComponent<SpriteRenderer>().enabled = false;
        E8.GetComponent<SpriteRenderer>().enabled = false;
        E9.GetComponent<SpriteRenderer>().enabled = false;
        E10.GetComponent<SpriteRenderer>().enabled = false;
        E11.GetComponent<SpriteRenderer>().enabled = false;
        F1.GetComponent<SpriteRenderer>().enabled = false;
        F2.GetComponent<SpriteRenderer>().enabled = false;
        F3.GetComponent<SpriteRenderer>().enabled = false;
        F4.GetComponent<SpriteRenderer>().enabled = false;
        F5.GetComponent<SpriteRenderer>().enabled = false;
        F6.GetComponent<SpriteRenderer>().enabled = false;
        F7.GetComponent<SpriteRenderer>().enabled = false;
        F8.GetComponent<SpriteRenderer>().enabled = false;
        F9.GetComponent<SpriteRenderer>().enabled = false;
        F10.GetComponent<SpriteRenderer>().enabled = false;
        F11.GetComponent<SpriteRenderer>().enabled = false;
        G1.GetComponent<SpriteRenderer>().enabled = false;
        G2.GetComponent<SpriteRenderer>().enabled = false;
        G3.GetComponent<SpriteRenderer>().enabled = false;
        G4.GetComponent<SpriteRenderer>().enabled = false;
        G5.GetComponent<SpriteRenderer>().enabled = false;
        G6.GetComponent<SpriteRenderer>().enabled = false;
        G7.GetComponent<SpriteRenderer>().enabled = false;
        G8.GetComponent<SpriteRenderer>().enabled = false;
        G9.GetComponent<SpriteRenderer>().enabled = false;
        G10.GetComponent<SpriteRenderer>().enabled = false;
        G11.GetComponent<SpriteRenderer>().enabled = false;
        H1.GetComponent<SpriteRenderer>().enabled = false;
        H2.GetComponent<SpriteRenderer>().enabled = false;
        H3.GetComponent<SpriteRenderer>().enabled = false;
        H4.GetComponent<SpriteRenderer>().enabled = false;
        H5.GetComponent<SpriteRenderer>().enabled = false;
        H6.GetComponent<SpriteRenderer>().enabled = false;
        H7.GetComponent<SpriteRenderer>().enabled = false;
        H8.GetComponent<SpriteRenderer>().enabled = false;
        H9.GetComponent<SpriteRenderer>().enabled = false;
        H10.GetComponent<SpriteRenderer>().enabled = false;
        H11.GetComponent<SpriteRenderer>().enabled = false;
        I1.GetComponent<SpriteRenderer>().enabled = false;
        I2.GetComponent<SpriteRenderer>().enabled = false;
        I3.GetComponent<SpriteRenderer>().enabled = false;
        I4.GetComponent<SpriteRenderer>().enabled = false;
        I5.GetComponent<SpriteRenderer>().enabled = false;
        I6.GetComponent<SpriteRenderer>().enabled = false;
        I7.GetComponent<SpriteRenderer>().enabled = false;
        I8.GetComponent<SpriteRenderer>().enabled = false;
        I9.GetComponent<SpriteRenderer>().enabled = false;
        I10.GetComponent<SpriteRenderer>().enabled = false;
        I11.GetComponent<SpriteRenderer>().enabled = false;
        J1.GetComponent<SpriteRenderer>().enabled = false;
        J2.GetComponent<SpriteRenderer>().enabled = false;
        J3.GetComponent<SpriteRenderer>().enabled = false;
        J4.GetComponent<SpriteRenderer>().enabled = false;
        J5.GetComponent<SpriteRenderer>().enabled = false;
        J6.GetComponent<SpriteRenderer>().enabled = false;
        J7.GetComponent<SpriteRenderer>().enabled = false;
        J8.GetComponent<SpriteRenderer>().enabled = false;
        J9.GetComponent<SpriteRenderer>().enabled = false;
        J10.GetComponent<SpriteRenderer>().enabled = false;
        J11.GetComponent<SpriteRenderer>().enabled = false;
        K1.GetComponent<SpriteRenderer>().enabled = false;
        K2.GetComponent<SpriteRenderer>().enabled = false;
        K3.GetComponent<SpriteRenderer>().enabled = false;
        K4.GetComponent<SpriteRenderer>().enabled = false;
        K5.GetComponent<SpriteRenderer>().enabled = false;
        K6.GetComponent<SpriteRenderer>().enabled = false;
        K7.GetComponent<SpriteRenderer>().enabled = false;
        K8.GetComponent<SpriteRenderer>().enabled = false;
        K9.GetComponent<SpriteRenderer>().enabled = false;
        K10.GetComponent<SpriteRenderer>().enabled = false;
        K11.GetComponent<SpriteRenderer>().enabled = false;
        float ratio = Screen.width / Screen.height;
        if (ratio < 2)
        {
            screenx = 543;
            screeny = 653;
        }
        else
        {
            screenx = 743;
            screeny = 853;
        }
    }

    // Update is called once per frame
    void Update ()
    {// .0001
        //Debug.Log(GPS.Instance.latitude - GPS.Instance.latCenter);
        float x = ((GPS.latitude - GPS.latCenter) * 900000) + screenx, y = ((GPS.longitude - GPS.lonCenter) * 900000) + screeny;
        if (x > 325)
            x = 325;
        if (y > 405)
            y = 405;
        if (x < 10)
            x = 10;
        if (y < 60)
            y = 60;
        PlayerIcon.GetComponent<SpriteRenderer>().transform.position = new Vector3 (x, y);

    }

    String Itoa(int a)
    {
        if (a == 1)
            return "A";
        else if (a == 2)
            return "B";
        else if (a == 3)
            return "C";
        else if (a == 4)
            return "D";
        else if (a == 5)
            return "E";
        else if (a == 6)
            return "F";
        else if (a == 7)
            return "G";
        else if (a == 8)
            return "H";
        else if (a == 9)
            return "I";
        else if (a == 10)
            return "J";
        else if (a == 11)
            return "K";
        else
            return ".";
    }

    
    public int[] BoardCoords()
    {
        int[] coords = new int[2];
        mouse = Input.mousePosition;
        int width = Screen.width;
        int height = Screen.height;
        float offsetytop = (height * 0.40f);
        float offsetybot = (height * 0.086f);
        float offsetx = (width * 0.095f);
        float sizeofboardx = width - (2 * offsetx);
        float sizeofboardy = height - (offsetybot + offsetytop);
        xboard = coords[0] = (int)Mathf.Floor((mouse.x - offsetx) / (sizeofboardx / 11)) + 1;
        yboard = coords[1] = (int)Mathf.Floor((mouse.y - offsetybot) / (sizeofboardy / 11)) + 1;
        return coords;
    }
    
    public void showRed(String name)
    {
        if (GameObject.Find(name).GetComponent<SpriteRenderer>().color.Equals(Color.yellow) || GameObject.Find(name).GetComponent<SpriteRenderer>().color.Equals(Color.green))
            ;
        else
        {
            GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void showGreen(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.green;

    }

    public void showMagenta(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.magenta;

    }

    public void showBlue(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.blue;

    }

    public void showCyan(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.cyan;

    }

    public void showWhite(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void showYellow(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find(name).GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void resetArray(ArrayList name)
    {
        for (int i = 0; i < name.Count; i++)
        {
            if (GameObject.Find(name[i].ToString()).GetComponent<SpriteRenderer>().color.Equals(Color.yellow) || GameObject.Find(name[i].ToString()).GetComponent<SpriteRenderer>().color.Equals(Color.green))
                ;
            else
            {
                showWhite(name[i].ToString());
                hideSquare(name[i].ToString());
            }
        }
    }

    public void hideSquare(String name)
    {
        GameObject.Find(name).GetComponent<SpriteRenderer>().enabled = false;        
    }

    void OnMouseDown()
    {
        Debug.Log(Input.mousePosition);
        int[] coords = BoardCoords();
        Itoa(coords[0]);
        xboard = coords[0];
        yboard = coords[1];
        if (SharedScript.clickShipsMode == true )
        {
            return;
        }
        else if (SharedScript.placeShipsMode > 0)
        {
            if (GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                return;
            String coord = GetComponent<SharedScript>().NumtoChar(coords[0]) + "," + GetComponent<SharedScript>().NumtoChar(coords[1]);
            Ship.Add(coord);
            SharedScript.orientationMode = SharedScript.placeShipsMode;
            SharedScript.shipsPlaced += SharedScript.placeShipsMode;
            SharedScript.placeShipsMode = 0;
            for (int i = SharedScript.orientationMode - 1; i >= 1; i--)
            {
                if ((coords[0] + i) > 11 || GameObject.Find(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                    right = true;
            }
            for (int i = SharedScript.orientationMode - 1; i >= 1; i--)
            {
                if ((coords[1] + i) > 11 || GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i)).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                    up = true;
            }
            for (int i = (SharedScript.orientationMode * (-1)) + 1; i <= -1; i++)
            {
                if ((coords[0] + i) < 1 || GameObject.Find(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                    left = true;
            }
            for (int i = (SharedScript.orientationMode * (-1)) + 1; i <= -1; i++)
            {
                if ((coords[1] + i) < 1 || GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i)).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                    down = true;
            }

            for (int i = SharedScript.orientationMode-1; i >= 1; i--)
            {
                if ((coords[0] + i) > 11 || right)
                    break;
                if (i == SharedScript.orientationMode - 1)
                    showMagenta(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
                else
                    showBlue(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
            }
            for (int i = SharedScript.orientationMode-1; i >= 1; i--)
            {
                if ((coords[1] + i) > 11 || up)
                    break;
                if (i == SharedScript.orientationMode - 1)
                    showMagenta(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
                else
                    showBlue(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
            }
            for (int i = (SharedScript.orientationMode * (-1))+1; i <= -1; i++)
            {
                if ((coords[0] + i) < 1 || left)
                    break;
                if (i == (SharedScript.orientationMode * (-1)) + 1)
                    showMagenta(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
                else
                    showBlue(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
            }
            for (int i = (SharedScript.orientationMode * (-1))+1; i <= -1; i++)
            {
                if ((coords[1] + i) < 1 || down)
                    break;
                if (i == (SharedScript.orientationMode * (-1)) + 1)
                    showMagenta(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
                else
                    showBlue(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
            }
            showCyan(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1]));
            right = false;
            up = false;
            left = false;
            down = false;
        }
        else if (SharedScript.orientationMode > 0)
        {
            char direction = ' ';
            if (GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.magenta))
            {
                showCyan(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1]));
                Ship.Add(GetComponent<SharedScript>().NumtoChar(coords[0]) + "," + GetComponent<SharedScript>().NumtoChar(coords[1]));
                if (SharedScript.orientationMode == 2)
                {
                    ;
                }
                else if (((coords[0] + 1) < 12) && GameObject.Find(Itoa(coords[0] + 1) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.blue) && GameObject.Find(Itoa(coords[0] + 1) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().enabled)
                {
                    direction = 'r';
                }
                else if (((coords[1] + 1) < 12) && GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + 1)).GetComponent<SpriteRenderer>().color.Equals(Color.blue) && GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + 1)).GetComponent<SpriteRenderer>().enabled)
                {
                    direction = 'u';
                }
                else if (((coords[0] - 1) > 0) && GameObject.Find(Itoa(coords[0] - 1) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().color.Equals(Color.blue) && GameObject.Find(Itoa(coords[0] - 1) + GetComponent<SharedScript>().NumtoChar(coords[1])).GetComponent<SpriteRenderer>().enabled)
                {
                    direction = 'l';
                }
                else if (((coords[0] + 1) > 0) && GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] - 1)).GetComponent<SpriteRenderer>().color.Equals(Color.blue) && GameObject.Find(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] - 1)).GetComponent<SpriteRenderer>().enabled)
                {
                    direction = 'd';
                }
            }
            else
            {
                return;
            }
            for(int i=1; i<(SharedScript.orientationMode-1); i++)
            {
                if (SharedScript.orientationMode == 2)
                    break;
                if (direction == 'r')
                {
                    showCyan(Itoa(coords[0] + i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
                    Ship.Add(GetComponent<SharedScript>().NumtoChar(coords[0] + i) + "," + GetComponent<SharedScript>().NumtoChar(coords[1]));
                }
                else if (direction == 'l')
                {
                    showCyan(Itoa(coords[0] - i) + GetComponent<SharedScript>().NumtoChar(coords[1]));
                    Ship.Add(GetComponent<SharedScript>().NumtoChar(coords[0] - i) + "," + GetComponent<SharedScript>().NumtoChar(coords[1]));
                }
                else if (direction == 'u')
                {
                    showCyan(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
                    Ship.Add(GetComponent<SharedScript>().NumtoChar(coords[0]) + "," + GetComponent<SharedScript>().NumtoChar(coords[1] + i));
                }
                else if (direction == 'd')
                {
                    showCyan(Itoa(coords[0]) + GetComponent<SharedScript>().NumtoChar(coords[1] - i));
                    Ship.Add(GetComponent<SharedScript>().NumtoChar(coords[0]) + "," + GetComponent<SharedScript>().NumtoChar(coords[1] - i));
                }
            }
            GetComponent<PlaceShip>().Place(Ship);
            Ship.Clear();


            if (SharedScript.shipsPlaced == 9)
            {
                SharedScript.attackMode = true;
                SharedScript.orientationMode = 0;
                for (int i = 1; i < 12; i++)
                {
                    for (int j = 1; j < 12; j++)
                    {
                        if (GameObject.Find(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j)).GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                        {
                            showWhite(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j));
                            hideSquare(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j));
                        }
                    }
                }
            }
            else
            {
                SharedScript.orientationMode = 0;
                SharedScript.clickShipsMode = true;
                SharedScript.placeShipsMode = 0;
            }
            //For all squares, if the color is magenta or Blue, change it to white and make it invisible.
            for (int i = 1; i < 12; i++){
                for (int j = 1; j < 12; j++){
                    if(GameObject.Find(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j)).GetComponent<SpriteRenderer>().color.Equals(Color.magenta) || GameObject.Find(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j)).GetComponent<SpriteRenderer>().color.Equals(Color.blue))
                    {
                        showWhite(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j));
                        hideSquare(GetComponent<SharedScript>().NumtoLetter(i) + GetComponent<SharedScript>().NumtoChar(j));
                    }
                }
            }
        }
        else if (SharedScript.attacking == true)
        {

           string attackPosition = (xboard.ToString() + "," + yboard.ToString());


            if (GameObject.Find(GetComponent<SharedScript>().NumtoLetter(xboard) + GetComponent<SharedScript>().NumtoChar(yboard)).GetComponent<SpriteRenderer>().color.Equals(Color.red))
            {

                if (attacker.Attack(attackPosition)) 
                {
                    showGreen(GetComponent<SharedScript>().NumtoLetter(xboard) + GetComponent<SharedScript>().NumtoChar(yboard));
                    killCounter++;
                }
                else
                {
                    showYellow(GetComponent<SharedScript>().NumtoLetter(xboard) + GetComponent<SharedScript>().NumtoChar(yboard));
                }
                AttackButton.counter = 1;
                SharedScript.attacking = false;
                resetArray(temp);
            }
        }
        return;
    }
}
