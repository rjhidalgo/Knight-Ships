using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedShip3 : MonoBehaviour
{
    //bool placed = false;
    public SpriteRenderer Glow3;

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
        if (SharedScript.shipsPlaced == 3 || SharedScript.shipsPlaced == 5 || SharedScript.shipsPlaced == 7 || SharedScript.shipsPlaced == 9)
        {
            return;
        }
        if (SharedScript.clickShipsMode)
        {
            Glow3.GetComponent<SpriteRenderer>().enabled = true;
            SharedScript.placeShipsMode = 3;
            SharedScript.clickShipsMode = false;
        }
    }
}
