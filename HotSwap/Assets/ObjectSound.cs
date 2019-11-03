using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides various method for interacting with an object's sound and handles collision sounds.
/// </summary>
public class ObjectSound : MonoBehaviour
{
    private AudioClip uniqueAudioClip;
    private AudioClip collisionClip;

    private AudioSource audioSource;
    private ItemInterface objItemInt;
    

    
    
    // Methods --------------------------------------------------------------

    /// <summary>
    /// Plays the sound associated with the object, called by the 'Grabbing' script upon picking up an object
    /// </summary>
    public void PlayUniqueSound()
    {
        audioSource.clip = uniqueAudioClip;
        audioSource.Play();
    }

    
    // Body --------------------------------------------------------------

    void OnCollisionEnter(Collision collider)
    {
        audioSource.clip = collisionClip;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        objItemInt = GetComponent<ItemInterface>();

        uniqueAudioClip = objItemInt.getUniqueAudio();
        collisionClip = objItemInt.getCollisionAudio();
    }
}
