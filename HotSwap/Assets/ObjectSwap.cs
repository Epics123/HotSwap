using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{

	public GameObject leftHand;
	public GameObject rightHand;
    public bool running = false;

    // Start is called before the first frame update
    void Start()
    {
		leftHand = GameObject.Find("Controller (left)");
		rightHand = GameObject.Find("Controller (right)");
    }

	//Swaps objects if weight classes are equal
    public void SwapObjects(GameObject left, GameObject right)
    {
        Vector3 tmp;
        //tmp = left.transform.position;
        //left.transform.position = right.transform.position;
        //right.transform.position = tmp;
        StartCoroutine("afterEffectSwap");

    }

    IEnumerator afterEffectSwap()
    {
        running = true;
        Vector3 start = leftHand.transform.position;
        Vector3 end = rightHand.transform.position;

        leftHand.transform.position = Vector3.Lerp(start, end, 1.0f);
        rightHand.transform.position = Vector3.Lerp(end, start, 1.0f);

        GameObject after1;
        GameObject after2;

        while (true)
        {
            if (Vector3.Distance(leftHand.transform.position, end) < 0.2f && Vector3.Distance(rightHand.transform.position, start) < 0.2f)
            {
                break;
            }

            after1 = Instantiate(leftHand);
            Destroy(after1, 0.1f);
            after2 = Instantiate(rightHand);
            Destroy(after2, 0.1f);

            yield return new WaitForSeconds(0.2f);
        }

        leftHand.transform.position = end;
        rightHand.transform.position = start;
        running = false;
    }
}
