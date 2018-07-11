using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BolaManager : NetworkBehaviour {

	public GameObject prefabBola;
	bool isShowObject = false;
	GameObject bola;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//kode ini hanya dijalankan di server
		if (!isServer) {
			return;
		}

		//ketika terdapat dua player maka permainan akan dimulai
		if (NetworkServer.connections.Count == 2) {
			//memastikan bola belom dibuat sebelomnya
			if (!isShowObject) {
				//kemudian bola diciptakan di server yang nanti diteruskan ke client
				bola = (GameObject)Instantiate (prefabBola);
				NetworkServer.Spawn (bola);
				isShowObject = true;
			}
		} else {
			if (isShowObject) {
				//bola dihapus
				Destroy (bola);
				isShowObject = false;
			}
		}
	}
}
