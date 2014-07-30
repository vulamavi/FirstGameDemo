﻿/* 
*  This file is part of the Unity networking tutorial by M2H (http://www.M2H.nl)
*  The original author of this code Mike Hergaarden, even though some small parts 
*  are copied from the Unity tutorials/manuals.
*  Feel free to use this code for your own projects, drop me a line if you made something exciting! 
*/
#pragma strict
#pragma implicit
#pragma downcast

public var owner : NetworkPlayer;

//Last input value, we're saving this to save network messages/bandwidth.
private var lastClientHInput : float=0;
private var lastClientVInput : float=0;

//The input values the server will execute on this object
private var serverCurrentHInput : float = 0;
private var serverCurrentVInput : float = 0;


function Awake(){
	// We are probably not the owner of this object: disable this script.
	// RPC's and OnSerializeNetworkView will STILL get trough!
	// The server ALWAYS run this script though
	if(Network.isClient){
		enabled=false;	 // disable this script (this enables Update());	
	}	
}


@RPC
function SetPlayer(player : NetworkPlayer){
 	Debug.Log(".......setttttt player");
	owner = player;
	if(player==Network.player){
		//Hey thats us! We can control this player: enable this script (this enables Update());
		enabled=true;
	}
}

function Update(){
	
	//Client code
	if(owner!=null && Network.player==owner){
		//Only the client that owns this object executes this code
		var HInput : float = Input.GetAxis("Horizontal");
		var VInput : float = Input.GetAxis("Vertical");
		
		//Is our input different? Do we need to update the server?
		if(lastClientHInput!=HInput || lastClientVInput!=VInput ){
			lastClientHInput = HInput;
			lastClientVInput = VInput;			
			
			if(Network.isServer){
				//Too bad a server can't send an rpc to itself using "RPCMode.Server"!...bugged :[
				SendMovementInput(HInput, VInput);
			}else if(Network.isClient){
				//SendMovementInput(HInput, VInput); //Use this (and line 64) for simple "prediction"
				networkView.RPC("SendMovementInput", RPCMode.Server, HInput, VInput);
			}
			
		}
	}
	
	//Server movement code
	if(Network.isServer){//Also enable this on the client itself: "|| Network.player==owner){|"
		//Actually move the player using his/her input
		var moveDirection : Vector3 = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
		var speed : float = 5;
		transform.Translate(speed * moveDirection * Time.deltaTime);
	}
	
}




@RPC
function SendMovementInput(HInput : float, VInput : float){	
	//Called on the server
	serverCurrentHInput = HInput;
	serverCurrentVInput = VInput;
}


function OnSerializeNetworkView(stream : BitStream, info : NetworkMessageInfo)
{
	if (stream.isWriting){
		//This is executed on the owner of the networkview
		//The owner sends it's position over the network
		
		var pos : Vector3 = transform.position;		
		stream.Serialize(pos);//"Encode" it, and send it
				
	}else{
		//Executed on all non-owners
		//This client receive a position and set the object to it
		
		var posReceive : Vector3 = Vector3.zero;
		stream.Serialize(posReceive); //"Decode" it and receive it
		
		//We've just recieved the current servers position of this object in 'posReceive'.
		
		transform.position = posReceive;		
		//To reduce laggy movement a bit you could comment the line above and use position lerping below instead:	
		//transform.position = Vector3.Lerp(transform.position, posReceive, 0.9); //"lerp" to the posReceive by 90%
		
	}
}