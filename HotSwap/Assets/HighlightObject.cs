using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material boarder;
    public Material nonBoarder;

    public bool isHighlighted = false;

    // Update is called once per frame
    void Update()
    {
        Highlight();
		//if (isHighlighted)
		//	isHighlighted = !isHighlighted;
    }


    void Highlight()
    {
        if(isHighlighted)
            GetComponent<Renderer>().material = boarder;
        else
            GetComponent<Renderer>().material = nonBoarder;
    }
}
