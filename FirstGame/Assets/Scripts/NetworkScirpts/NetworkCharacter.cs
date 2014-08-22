using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
	Vector3 realPosition;
	Quaternion realRotate;
	private bool isBegin = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
		}
		else{
			transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * 10);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log(",........ onphotonSerializeView");
		if (stream.isWriting) {
			stream.SendNext(transform.position);
		}
		else{
			realPosition = (Vector3)stream.ReceiveNext();
		}
	}
}
