using UnityEngine;
using System.Collections;

public class autoFlyNoneNPC : MonoBehaviour {
	public float vFlySpeed;	// Use this for initialization
	public bool bLife = true;

	void Start () {
		vFlySpeed = (float)(Random.Range (3, 8)) / 1000;
	}
	
	// Update is called once per frame
	void Update () {
		if (bLife) {
			transform.Translate(new Vector3(0, vFlySpeed, 0));		
		}
	}

	//set Speed
	void setFlySpeed(float flySpeed){
		vFlySpeed = flySpeed;
	}
}
