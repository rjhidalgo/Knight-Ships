using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedShip4 : MonoBehaviour
{
    //bool placed = false;
    public SpriteRenderer Glow4;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        if (SharedScript.shipsPlaced == 4 || SharedScript.shipsPlaced == 6 || SharedScript.shipsPlaced == 7 || SharedScript.shipsPlaced == 9)
        {
            return;
        }
        if (SharedScript.clickShipsMode)
        {
            Glow4.GetComponent<SpriteRenderer>().enabled = true;
            SharedScript.placeShipsMode = 4;
            SharedScript.clickShipsMode = false;
        }
    }
}
