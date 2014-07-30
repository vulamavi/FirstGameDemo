using UnityEngine;
using System.Collections;

public class AutoFollow : MonoBehaviour {
	public GameObject playerFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool bMaxPos = playerFollow.transform.GetComponent<AutoFly> ().bMaxPos;
		bool bOut = playerFollow.transform.GetComponent<AutoFly> ().bOut;
		float vFlySpeed = playerFollow.transform.GetComponent<AutoFly> ().vFlySpeed;
		float vDownSpeed = playerFollow.transform.GetComponent<AutoFly> ().vDownSpeed;

		if (bMaxPos && !bOut) {
			Camera.main.transform.Translate(new Vector3(0, vFlySpeed, 0));
//			gameObject.transform.Translate(new Vector3(0, vFlySpeed, 0));
		}
		else if(!bOut){
			Debug.Log("!bOut" );
		}
		else if(bOut){
			Debug.Log("bOut" );
			playerFollow.transform.GetComponent<AutoFly> ().bMaxPos = false;
			if(Camera.main.WorldToScreenPoint (playerFollow.transform.position).y <= Screen.height * 3 / 6 && playerFollow.transform.position.y >= (float)(0)){
				Camera.main.transform.Translate(new Vector3(0, vDownSpeed * (-1), 0));
			}
		}
		if (Camera.main.WorldToScreenPoint (playerFollow.transform.position).y >= Screen.height * 5 / 8) {
			Debug.Log("max pos");
			playerFollow.transform.GetComponent<AutoFly> ().bMaxPos = true;
		}
	}
}
