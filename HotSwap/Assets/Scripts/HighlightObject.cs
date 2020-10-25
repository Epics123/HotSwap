using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches the material between the outline shader and the normal shader when an object is selected
/// </summary>
public class HighlightObject : MonoBehaviour
{
    public Material border;
    public Material nonBorder;

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
            GetComponent<Renderer>().material = border;
        else
            GetComponent<Renderer>().material = nonBorder;
    }
}
