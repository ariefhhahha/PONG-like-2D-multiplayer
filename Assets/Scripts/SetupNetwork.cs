using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupNetwork : MonoBehaviour {

	public int portNumber = 7777; 
	public string serverIPAddress = "localhost";
	public GameObject panelSetup;
	NetworkManager network;
	bool isNetworkActive = false;

	// Use this for initialization
	void Start () {
		network = GameObject.Find ("Network Manager").GetComponent<NetworkManager> ();
		Debug.Log ("This IP " + Network.player.ipAddress.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		if (ClientScene.ready || NetworkServer.active) {
			if (!isNetworkActive) {
				panelSetup.SetActive (false);
				isNetworkActive = true;
			}
			if (Input.GetKeyUp (KeyCode.Escape)) {
				network.StopHost ();
				panelSetup.SetActive (true);
			}
		} else {
			if (isNetworkActive) {
				panelSetup.SetActive (true);
				isNetworkActive = false;
			}
		}
	}

	public void StartHostGame(){
		if (!NetworkServer.active) {
			if (NetworkClient.active) {
				network.StopClient ();
			}
			network.networkPort = portNumber;
			network.networkAddress = serverIPAddress;
			network.StartHost ();
		}
	}

	public void StartJointGame(){
		if(!NetworkClient.active && !NetworkServer.active){
			network.networkPort = portNumber;
			network.networkAddress = serverIPAddress;
			network.StartClient ();
		}
	}
}
