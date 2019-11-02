using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class pointer : MonoBehaviour
{
    public Color color;
    public float thickness = 0.002f;
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean fire;
    public GameObject holder;
    public GameObject line;

    public GameObject objectHit;

    // Start is called before the first frame update
    void Start()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        line = GameObject.CreatePrimitive(PrimitiveType.Cube);
        line.transform.parent = holder.transform;
        line.transform.localScale = new Vector3(thickness, thickness, 100f);
        line.transform.localPosition = new Vector3(0f, 0f, 50f);
        line.transform.localRotation = Quaternion.identity;

        BoxCollider collider = line.GetComponent<BoxCollider>();
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        line.GetComponent<MeshRenderer>().material = newMaterial;
        line.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
		//if (fire != null && fire.GetState(pose.inputSource))
		//{
		//    firePointer();
		//}
		firePointer();


    }

    void firePointer()
    {
        RaycastHit hit;
        Ray ray = new Ray(holder.transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (hit.transform.CompareTag("grabbable"))
            {
                Debug.Log("meme " + hit.transform.name);
                objectHit = hit.transform.gameObject;
            }
        }
    }
}
