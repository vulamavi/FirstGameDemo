using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateBoot : MonoBehaviour {
	//count time
	private float time;

	//array position create NoneNPC
	public List<Vector3> arrPositionStart;

	//array NPC
	public List<GameObject> arrNPC;

	//player below
	public GameObject PlayerBelow;

	//objects NPC
	public GameObject NPCNone;
	public GameObject NPC1;
	public GameObject NPC2;
	public GameObject NPC3;
	public GameObject NPC4;
	public GameObject NPC5;

	//time create NPC
	public float timeCreateNone;
	public float timeCreateNPC1;
	public float timeCreateNPC2;
	public float timeCreateNPC3;
	public float timeCreateNPC4;
	public float timeCreateNPC5;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time = time + Time.deltaTime;
		autoCreateNoneNPC ();
		autoCreateNPC1 ();
		autoCreateNPC2 ();
		autoCreateNPC3 ();
		autoCreateNPC4 ();
		autoCreateNPC5 ();
	}

	/***************** create NPC ******************/
	void createNoneNPC(GameObject NPC, Vector3 position){
		Instantiate (NPC, position, NPCNone.transform.rotation);
		arrNPC.Add (NPC);
	}

	void autoCreateNoneNPC(){
		if ((int)(time / timeCreateNone) > 1) {
//			Debug.Log("create non-character");
			createNoneNPC(NPCNone,new Vector3(arrPositionStart[Random.Range(0, arrPositionStart.Count)].x, PlayerBelow.transform.position.y - (float)(0.6), arrPositionStart[Random.Range(0, arrPositionStart.Count)].z));	
			time = 0;
		}
	}

	void autoCreateNPC1(){
		
	}

	void autoCreateNPC2(){
		
	}

	void autoCreateNPC3(){
		
	}

	void autoCreateNPC4(){
		
	}

	void autoCreateNPC5(){
		
	}
	/***************** END create NPC ******************/

}
