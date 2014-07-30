using UnityEngine;
using System.Collections;

public class TouchHanler : MonoBehaviour {

	public float speedDown;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
//			Debug.Log("Touch ");
//			gameObject.transform.GetComponent<AutoFly>().bOut = true;
//			gameObject.transform.GetComponent<AutoFly>().bMaxPos = false;
//			gameObject.transform.Find("Bubble").gameObject.SetActive(false);
		}
	}

	void OnMouseDown(){
		Debug.Log("OK touched");
		Debug.Log("Touch ");
		gameObject.transform.GetComponent<AutoFly>().bOut = true;
		gameObject.transform.GetComponent<AutoFly>().bMaxPos = false;
		gameObject.transform.Find("Bubble").gameObject.SetActive(false);
	}
}
