using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoFly : MonoBehaviour {

	private NetworkPlayer owner;
	public bool bMaxPos = false;
	public bool bOut = false;
	public float vFlySpeed;
	public float vDownSpeed;
	public float vMoveHorizonal;
	// Use this for initialization
	void Start () {
		vFlySpeed = ((float)Random.Range (4, 9))/1000;
	}
	
	// Update is called once per frame
	void Update () {
		autoFlyCharacter ();
		autoMove ();
	}

	//set Speed
	void setFlySpeed(float flySpeed){
		vFlySpeed = flySpeed;
	}
	
	void setDownSpeed(float downSpeed){
		vDownSpeed = downSpeed;
	}

	[RPC]
	void SetPlayer(NetworkPlayer player){
		Debug.Log("setPlayer........");
		owner = player;
		if(player == Network.player){
			//Hey thats us! We can control this player: enable this script (this enables Update());
			enabled = true;
		}
	}

	void Awake(){
		// We are probably not the owner of this object: disable this script.
		// RPC's and OnSerializeNetworkView will STILL get trough!
		// The server ALWAYS run this script though
		if(Network.isClient){
		 	enabled = false;	 // disable this script (this enables Update());
			Debug.Log("i am client");
		}	
	}
	
//	void Update(){
//		
//		//Client code
//		if(owner!=null && Network.player==owner){
//			//Only the client that owns this object executes this code
//			var HInput : float = Input.GetAxis("Horizontal");
//			var VInput : float = Input.GetAxis("Vertical");
//			
//			//Is our input different? Do we need to update the server?
//			if(lastClientHInput!=HInput || lastClientVInput!=VInput ){
//				lastClientHInput = HInput;
//				lastClientVInput = VInput;			
//				
//				if(Network.isServer){
//					//Too bad a server can't send an rpc to itself using "RPCMode.Server"!...bugged :[
//					SendMovementInput(HInput, VInput);
//				}else if(Network.isClient){
//					//SendMovementInput(HInput, VInput); //Use this (and line 64) for simple "prediction"
//					networkView.RPC("SendMovementInput", RPCMode.Server, HInput, VInput);
//				}
//				
//			}
//		}
//		
//		//Server movement code
//		if(Network.isServer){//Also enable this on the client itself: "|| Network.player==owner){|"
//			//Actually move the player using his/her input
//			var moveDirection : Vector3 = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
//			var speed : float = 5;
//			transform.Translate(speed * moveDirection * Time.deltaTime);
//		}
//		
//	}


	//fly auto
	void autoFlyCharacter(){
		if (bMaxPos && !bOut) {
			gameObject.transform.Translate(new Vector3(0, vFlySpeed, 0));
//			Camera.main.transform.Translate(new Vector3(0, vFlySpeed, 0));
//			GameObject.Find("UI Root").transform.Find("Sky01").transform.GetComponent<Animator>().SetBool("bStart", true);
//			GameObject.Find("UI Root").transform.Find("Sky01").transform.GetComponent<Animator>().SetBool("bStop", false);
//			GameObject.Find("UI Root").transform.Find("Sky02").transform.GetComponent<Animator>().SetBool("bStart", true);
//			GameObject.Find("UI Root").transform.Find("Sky02").transform.GetComponent<Animator>().SetBool("bStop", false);
//			GameObject.Find("Scene").transform.Find("House").transform.GetComponent<Animator>().SetBool("bStart", true);
		}
		else if(!bOut){
			gameObject.transform.Translate(new Vector3(0, vFlySpeed, 0));
//			Camera.main.transform.Translate(new Vector3(0, vFlySpeed, 0));
		}
		else if(bOut){
			Debug.Log("restart bOut");
			gameObject.transform.Translate(new Vector3(0, vDownSpeed * (-1), 0));
			if(Camera.main.WorldToScreenPoint (gameObject.transform.position).y <= Screen.height * 3 / 6 && gameObject.transform.position.y >= (float)(0)){
//				Camera.main.transform.Translate(new Vector3(0, vDownSpeed * (-1), 0));
			}
			if(gameObject.transform.position.y <= (float)(-0.5842785)){
				Debug.Log("bout  = ........false ......");
				bOut = false;
				gameObject.transform.Find("Bubble").gameObject.SetActive(true);
			}
		}
		
//		if (Camera.main.WorldToScreenPoint (gameObject.transform.position).y >= Screen.height * 5 / 8) {
//			bMaxPos = true;
//		}
	}

	//move player left- right
	void autoMove(){
		Vector3 dir = Vector3.zero;
		dir.z = Input.acceleration.z;
		dir.y = Input.acceleration.y;
		dir.x = -Input.acceleration.x;

		if (dir.x > 0) {
			if(Camera.main.WorldToScreenPoint (gameObject.transform.position).x < 20){

			}
			else{
//				transform.Translate(new Vector3((-1) *vMoveHorizonal * System.Math.Abs(dir.x), 0, 0));
				transform.position = Vector3.Slerp(transform.position, transform .position + new Vector3((-1) *vMoveHorizonal * System.Math.Abs(dir.x), 0, 0), Time.deltaTime * 15);
			}
		}
		if(dir.x < 0){
			if(Camera.main.WorldToScreenPoint (gameObject.transform.position).x > Screen.width - 20 ){
			}
			else {
//				transform.Translate(new Vector3(vMoveHorizonal * System.Math.Abs(dir.x) , 0, 0));
				transform.position = Vector3.Slerp(transform.position, transform .position + new Vector3(vMoveHorizonal * System.Math.Abs(dir.x) , 0, 0), Time.deltaTime * 15);
			}
		}
	}
}
