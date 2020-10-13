using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public List<GameObject> props;
    public List<Vector3> spawnLocs;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "grabbable" && !col.GetComponent<ItemInterface>().getGood())
        {
            respawnObjects();
            Destroy(col.gameObject);
        }
    }

    void respawnObjects()
    {
        int random = Random.Range(0, props.Count);
        int randomLoc = Random.Range(0, spawnLocs.Count);
        Instantiate(props[random], spawnLocs[randomLoc], Quaternion.identity);
    }
}
