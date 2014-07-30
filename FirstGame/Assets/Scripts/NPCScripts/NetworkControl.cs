using UnityEngine;
using System.Collections;

public class NetworkControl : MonoBehaviour {

	public Transform playerPrefab;
	public ArrayList playerScripts = new ArrayList();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnServerInitialized(){
		//Spawn a player for the server itself
		Spawnplayer(Network.player);
	}
	
	void OnPlayerConnected(NetworkPlayer newPlayer) {
		//A player connected to me(the server)!
		Spawnplayer(newPlayer);
		Debug.Log("1 player connected,...");
	}
	
	
	void Spawnplayer(NetworkPlayer newPlayer){
		//Called on the server only
		Debug.Log("creation one player................................................");
		
		int playerNumber = int.Parse(newPlayer + "");
		//Instantiate a new object for this player, remember; the server is therefore the owner.
		GameObject player = ((GameObject)Network.Instantiate (playerPrefab, transform.position, transform.rotation, playerNumber));
		Transform myNewTrans = player.transform;
		
		//Get the networkview of this new transform
		NetworkView newObjectsNetworkview = myNewTrans.networkView;
		
		//Keep track of this new player so we can properly destroy it when required.
//		playerScripts.Add(myNewTrans.GetComponent<NetworkControl>());
		
		//Call an RPC on this new networkview, set the player who controls this player
		newObjectsNetworkview.RPC("SetPlayer", RPCMode.AllBuffered, newPlayer);//Set it on the owner
	}
	
	
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		
//		for(NetworkControl scriptt in playerScripts){
//			if(player==script.owner){//We found the players object
//				Network.RemoveRPCs(script.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
//				Network.Destroy(script.gameObject);//Destroying the GO will destroy everything
//				playerScripts.Remove(script);//Remove this player from the list
//				break;
//			}
//		}
		
		//Remove the buffered RPC call for instantiate for this player.
		int playerNumber = int.Parse(player + "");
		Network.RemoveRPCs(Network.player, playerNumber);
		
		
		// The next destroys will not destroy anything since the players never
		// instantiated anything nor buffered RPCs
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("Resetting the scene the easy way.");
//		Application.LoadLevel(Application.loadedLevel);	
	}
}