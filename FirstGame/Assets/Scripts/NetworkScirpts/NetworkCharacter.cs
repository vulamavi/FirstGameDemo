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
			if(realPosition != null && realPosition != Vector3.zero){
//				transform.position = realPosition;
//				transform.rotation = realRotate;
			}
		}
		else{
			if(realPosition != null){
				transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * 5);
				transform.rotation = Quaternion.Lerp(transform.rotation, realRotate, Time.deltaTime * 5);
			}
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log(",........ onphotonSerializeView");
		if (stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else{
			realPosition = (Vector3)stream.ReceiveNext();
			realRotate = (Quaternion)stream.ReceiveNext();
			if(!isBegin){
				isBegin = true;
				transform.position = realPosition;
				transform.rotation = realRotate;
			}

		}

//
//		if (stream.isWriting)
//		{
//			Vector3 pos = transform.localPosition;
//			stream.Serialize(ref pos);
//		}
//		else
//		{
//			Vector3 pos = Vector3.zero;
//			stream.Serialize(ref pos);
//			realPosition = pos;
//		}
	}
}
