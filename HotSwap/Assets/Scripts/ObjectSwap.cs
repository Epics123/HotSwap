using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{

	public GameObject leftHand;
	public GameObject rightHand;
    public bool running = false;

	private Vector3 velocity = Vector3.zero;

	Vector3 start;
	Vector3 end;

	// Start is called before the first frame update
	void Start()
    {
		leftHand = GameObject.Find("Controller (left)");
		rightHand = GameObject.Find("Controller (right)");
    }

	//Swaps objects if weight classes are equal
    public void SwapObjects(GameObject left, GameObject right)
    {
		//Vector3 tmp;
		//tmp = left.transform.position;
		//left.transform.position = right.transform.position;
		//right.transform.position = tmp;
		start = left.transform.position;
		end = right.transform.position;
		StartCoroutine(afterEffectSwap(left, right));
	}

    IEnumerator afterEffectSwap(GameObject left, GameObject right)
    {
		Debug.Log("test");
        running = true;

        GameObject after1;
        GameObject after2;
		left.GetComponent<Collider>().enabled = false;
		right.GetComponent<Collider>().enabled = false;
		right.GetComponent<Rigidbody>().useGravity = false;
		left.GetComponent<Rigidbody>().useGravity = false;
		while (true)
        {
			left.transform.position = Vector3.SmoothDamp(left.transform.position, end, ref velocity, Time.deltaTime, 30.0f);
			right.transform.position = Vector3.SmoothDamp(right.transform.position, start, ref velocity, Time.deltaTime, 30.0f);
			if (Vector3.Distance(left.transform.position, end) < 0.03f && Vector3.Distance(right.transform.position, start) < 0.03f)
            {
                break;
            }

			after1 = Instantiate(left);
            Destroy(after1, 0.05f);
            after2 = Instantiate(right);
			Destroy(after2, 0.05f);

            yield return new WaitForSeconds(0.02f);
        }
		right.GetComponent<Rigidbody>().useGravity = true;
		left.GetComponent<Rigidbody>().useGravity = true;
		left.GetComponent<Collider>().enabled = true;
		right.GetComponent<Collider>().enabled = true;
		left.transform.position = end;
        right.transform.position = start;

		yield return new WaitForSeconds(0.1f);
		running = false;
    }
}
