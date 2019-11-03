using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This script creates a list that holds all of the unhidden illicit items and hopefully change the state of the game accordingly. 
/// </summary>
public class GameStateManager : MonoBehaviour
{ 

    public List<GameObject> illicitItemList;
 

    public void HideObject(GameObject hiddenObject)
    { 
        illicitItemList.Remove(hiddenObject);
        Debug.Log(illicitItemList.Count);
    }

    void CheckList()  //call when needed
    {
        if (illicitItemList.Count == 0)
        {
            //Good ending 
        }
        else if (illicitItemList.Count != 0)
        {
            //Bad ending (mama gets the chancla)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> illicitItemList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
