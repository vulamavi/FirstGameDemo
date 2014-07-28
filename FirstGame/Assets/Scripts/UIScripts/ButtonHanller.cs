using UnityEngine;
using System.Collections;

public class ButtonHanller : MonoBehaviour {
	GameObject player;
	Quaternion quaternionBeginPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		quaternionBeginPosition = player.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Lumbering")){
			if (player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > (float)0.95) {
				player.transform.GetComponent<Animator> ().SetBool ("bAttack", false);
				player.transform.rotation = quaternionBeginPosition;
			}
		}
	}

	void OnPress(bool isDown){
		if (isDown) {
			attack();
		}
	}

	void attack(){
		Debug.Log("Attack");
		player.transform.GetComponent<Animator> ().SetBool ("bAttack", true);
	}
}
