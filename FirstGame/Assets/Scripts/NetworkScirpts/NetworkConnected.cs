using UnityEngine;
using System.Collections;

public class NetworkConnected : MonoBehaviour {
	string connectToIP = "127.0.0.1";
	int connectPort = 25001;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (Network.peerType == NetworkPeerType.Disconnected) {
			GUILayout.Label("Connection status: Disconnected");
			connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
			connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
			
			GUILayout.BeginVertical();
			if (GUILayout.Button ("Connect as client"))
			{
				Debug.Log("client connecttiing...");

				//Connect to the "connectToIP" and "connectPort" as entered via the GUI
				//Ignore the NAT for now
				
				Network.Connect(connectToIP, connectPort);
			}
			
			if (GUILayout.Button ("Start Server"))
			{
				//Start a server for 32 clients using the "connectPort" given via the GUI
				//Ignore the nat for now	
				
				Network.InitializeServer(32, connectPort, true);
//				Network.in
			}
			GUILayout.EndVertical();
		}
		else {
			if (Network.peerType == NetworkPeerType.Connecting){
				
				GUILayout.Label("Connection status: Connecting");
				
			} else if (Network.peerType == NetworkPeerType.Client){
				
				GUILayout.Label("Connection status: Client!");
				GUILayout.Label("Ping to server: "+Network.GetAveragePing(  Network.connections[0] ) );		
				
			} else if (Network.peerType == NetworkPeerType.Server){
				
				GUILayout.Label("Connection status: Server!");
				GUILayout.Label("Connections: "+ Network.connections.Length);
				if(Network.connections.Length >=1 ){
					GUILayout.Label("Ping to first player: "+Network.GetAveragePing(  Network.connections[0] ) );
				}			
			}
			
			if (GUILayout.Button ("Disconnect"))
			{
				Network.Disconnect(200);
			}
		}
	}

	void OnConnectedToServer() {
		Debug.Log("This CLIENT has connected to a server");	
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: "+ error);
	}
	
	
	//Server functions called by Unity
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}
	
	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}
	

	
	void OnFailedToConnectToMasterServer(NetworkConnectionError info){
		Debug.Log("Could not connect to master server: "+ info);
	}
	
	void OnNetworkInstantiate (NetworkMessageInfo info) {
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		//Custom code here (your code!)
	}
//
//	public Transform playerPrefab;
//	public ArrayList playerScripts = new ArrayList();
//	// Use this for initialization
//
//	
//	void OnServerInitialized(){
//		Debug.Log("1 player connected,...");
//
//		//Spawn a player for the server itself
////		Spawnplayer(Network.player);
//	}
//	
//	void OnPlayerConnected(NetworkPlayer newPlayer) {
////		Debug.Log("...player connected,...");
//
//		//A player connected to me(the server)!
//		Spawnplayer(newPlayer);
//	}
//	
//	
//	void Spawnplayer(NetworkPlayer newPlayer){
//		//Called on the server only
//		Debug.Log("creation one player................................................");
//		
//		int playerNumber = int.Parse(newPlayer + "");
//		//Instantiate a new object for this player, remember; the server is therefore the owner.
//		GameObject player = ((GameObject)Network.Instantiate (playerPrefab, transform.position, transform.rotation, playerNumber));
//		Transform myNewTrans = player.transform;
//		
//		//Get the networkview of this new transform
//		NetworkView newObjectsNetworkview = myNewTrans.networkView;
//		
//		//Keep track of this new player so we can properly destroy it when required.
//		playerScripts.Add(myNewTrans.GetComponent<NetworkControl>());
//		
//		//Call an RPC on this new networkview, set the player who controls this player
//		newObjectsNetworkview.RPC("SetPlayer", RPCMode.AllBuffered, newPlayer);//Set it on the owner
//	}
//	
//	
//	
//	void OnPlayerDisconnected(NetworkPlayer player) {
//		Debug.Log("Clean up after player " + player);
//		
//		//		for(NetworkControl scriptt in playerScripts){
//		//			if(player==script.owner){//We found the players object
//		//				Network.RemoveRPCs(script.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
//		//				Network.Destroy(script.gameObject);//Destroying the GO will destroy everything
//		//				playerScripts.Remove(script);//Remove this player from the list
//		//				break;
//		//			}
//		//		}
//		
//		//Remove the buffered RPC call for instantiate for this player.
//		int playerNumber = int.Parse(player + "");
//		Network.RemoveRPCs(Network.player, playerNumber);
//		
//		
//		// The next destroys will not destroy anything since the players never
//		// instantiated anything nor buffered RPCs
//		Network.RemoveRPCs(player);
//		Network.DestroyPlayerObjects(player);
//	}
//	
//	void OnDisconnectedFromServer(NetworkDisconnection info) {
//		Debug.Log("Resetting the scene the easy way.");
//		//		Application.LoadLevel(Application.loadedLevel);	
//	}
}
