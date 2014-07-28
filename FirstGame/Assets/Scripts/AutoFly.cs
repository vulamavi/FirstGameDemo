using UnityEngine;
using System.Collections;

public class AutoFly : MonoBehaviour {

	public bool bMaxPos = false;
	public bool bOut = false;
	public float vFlySpeed;
	public float vDownSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		autoFlyCharacter ();
	}
	 
	//set Speed
	void setFlySpeed(float flySpeed){
		vFlySpeed = flySpeed;
	}
	
	void setDownSpeed(float downSpeed){
		vDownSpeed = downSpeed;
	}

	//fly auto
	void autoFlyCharacter(){
		if (bMaxPos && !bOut) {
			GameObject.Find("Scene").transform.Find("Sky01").transform.GetComponent<Animator>().SetBool("bStart", true);
			GameObject.Find("Scene").transform.Find("Sky01").transform.GetComponent<Animator>().SetBool("bStop", false);
			
			GameObject.Find("Scene").transform.Find("Sky02").transform.GetComponent<Animator>().SetBool("bStart", true);
			GameObject.Find("Scene").transform.Find("Sky02").transform.GetComponent<Animator>().SetBool("bStop", false);

			GameObject.Find("Scene").transform.Find("House").transform.GetComponent<Animator>().SetBool("bStart", true);

		}
		else if(!bOut){
			gameObject.transform.Translate(new Vector3(0, vFlySpeed, 0));
		}
		else if(bOut){
			Debug.Log("restart bOut");
			gameObject.transform.Translate(new Vector3(0, vDownSpeed * (-1), 0));
			if(Camera.main.WorldToScreenPoint (gameObject.transform.position).y <= Screen.height * 1 / 6){
				bOut = false;
				gameObject.transform.Find("Bubble").gameObject.SetActive(true);
			}
		}
		
		if (Camera.main.WorldToScreenPoint (gameObject.transform.position).y >= Screen.height * 3 / 4) {
			bMaxPos = true;
		}
	}
}
