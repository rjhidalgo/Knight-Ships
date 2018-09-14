using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedShip2 : MonoBehaviour {
    //bool placed = false;
    public SpriteRenderer Glow2;
    
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update () {
	}

    void OnMouseDown()
    {
        if (SharedScript.shipsPlaced == 2 || SharedScript.shipsPlaced == 5 || SharedScript.shipsPlaced == 6 || SharedScript.shipsPlaced == 9)
        {
            return;
        }
        if (SharedScript.clickShipsMode)
        {
            Glow2.GetComponent<SpriteRenderer>().enabled = true;
            SharedScript.placeShipsMode = 2;
            SharedScript.clickShipsMode = false;
        }
    }
}
