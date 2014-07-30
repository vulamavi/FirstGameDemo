using UnityEngine;
using System.Collections;

public class ButtonHanller : MonoBehaviour {
	public GameObject player;
	Quaternion quaternionBeginPosition;
	GameObject Ax;
//	public GameObject otherPlayer;
	private bool bAttack;
	// Use this for initialization
	void Start () {
		bAttack = false;
//		player = GameObject.Find ("Player");
		Ax = player.transform.Find ("Ax").gameObject;
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

		if (bAttack) {
//			Ax.transform.position = Vector3.Slerp(Ax.transform.position, otherPlayer.transform.position, Time.deltaTime * 3);
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
		bAttack = true;
	}
}
