using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Material boarder;
    public Material nonBoarder;

    public bool isHighlighted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Highlight();
    }


    void Highlight()
    {
        if(isHighlighted)
            GetComponent<Renderer>().material = boarder;
        else
            GetComponent<Renderer>().material = nonBoarder;
    }

    //Test highlighting with mouse
    private void OnMouseOver()
    {
        isHighlighted = true;
    }

    private void OnMouseExit()
    {
        isHighlighted = false;
    }
}
