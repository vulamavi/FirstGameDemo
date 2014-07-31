using UnityEngine;
using System.Collections;
using System.IO;
public class InputHanlerPlayer : MonoBehaviour {
	//owner
//	private NetworkPlayer owner;
	private float lastClientHInput=0;
	private float lastClientVInput=0;
	
	//The input values the server will execute on this object
	private float serverCurrentHInput = 0;
	private float serverCurrentVInput = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	void SendMovementInput(float HInput, float VInput){	
		//Called on the server
		serverCurrentHInput = HInput;
		serverCurrentVInput = VInput;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
			if (stream.isWriting){
			Debug.Log("writing.....");
			//This is executed on the owner of the networkview
				//The owner sends it's position over the network
				Vector3 pos =  new Vector3();
				pos = transform.position;		
				stream.Serialize(ref pos);//"Encode" it, and send it
			}else{
				//Executed on all non-owners
				//This client receive a position and set the object to it
				Debug.Log("read.....");
				Vector3 posReceive =  new Vector3();
				posReceive = Vector3.zero;
				stream.Serialize(ref posReceive); //"Decode" it and receive it
				
				//We've just recieved the current servers position of this object in 'posReceive'.
				transform.position = posReceive;		
				//To reduce laggy movement a bit you could comment the line above and use position lerping below instead:	
				//transform.position = Vector3.Lerp(transform.position, posReceive, 0.9); //"lerp" to the posReceive by 90%
				
			}	
	}
}
