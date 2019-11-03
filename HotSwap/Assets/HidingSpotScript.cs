using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Called by the hiding place trigger, flags the object as hidden by the GameStateManager and
/// marks the hiding spot as used, and plays any associated animations, destroys the hidden object
/// 
/// DEV NOTES: This script will NOT function without some integration with the GameStateManager script post merge, most of it should be there,
/// it just needs to be uncommented.
/// </summary>
public class HidingSpotScript : MonoBehaviour
{
    [Header("Contextual Data")]
    public bool hidingSpotFull = false;
    public int curCapacity = 0;

    [Header("Dependencies")]
    // public GameStateManager gameStateM;
    [Tooltip("The Component that contains the function for the object's animation")]
    public Component parentObjectController;

    [Header("Hiding Spot Values")]
    [Tooltip("The size of the items that can go in the hiding spot (small, medium, or large.)")]
    public string hidingSpotSize;
    [Tooltip("The maximum number of objects that can be hidden in the spot.")]
    public int hidingSpotCapacity;

    
    // Body -----------------------------------------------------------------------------------------------

    void OnTriggerEnter(Collider collision)
    {
        // check if object is correct size
        if (collision.transform.tag != hidingSpotSize)
        {
            collision.GetComponent<Rigidbody>().velocity *= -1; // reverse the direction of the object
        }

        // check if the hiding spot is full
        if (curCapacity >= hidingSpotCapacity)
        {
            /*We're going to pass the function that plays animation a game component that contains a function that's always called
             animation, that way we can just call component.animation and it will always play the correct animation*/
            // parentObjectController.Animation(); // play associated animation

            hidingSpotFull = true;
            Destroy(collision); // destroy the game object
            // gameStateM.HideObject(collision); // update the game state manager
        }


    }
    
    // Start is called before the first frame update
    void Start()
    {
        // gameStateM = GameObject.FindObjectOfType<GameStateManager>();
    }
}
