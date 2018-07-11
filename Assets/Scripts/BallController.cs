using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour {

	//untuk menyamakan variable ke semua client
	[SyncVar(hook="OnChangeScore1")]
	public int scoreP1;
	[SyncVar(hook="OnChangeScore2")]
	public int scoreP2;

	public int force;

	//nyiapin variable buat ngambil gameobject
	GameObject scoreP1UI;
	GameObject scoreP2UI;

	//bikin variable buat nyimpen audio
	AudioSource audio;
	public AudioClip hitSound;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (2, 1).normalized * force);

		//inisialisasi score
		scoreP1 = 0;
		scoreP2 = 0;

		//nyari terus ngambil gameobject
		scoreP1UI = GameObject.Find ("Score P1");
		scoreP2UI = GameObject.Find ("Score P2");

		//ngambil component audio source terus diisi ke variable audio tadi
		audio = GetComponent<AudioSource>();
	}

	void OnChangeScore1(int score){
		if(scoreP1UI != null) {
			scoreP1UI.GetComponent<Text>().text = "" + score;
		} else {
			scoreP1UI = GameObject.Find ("Score P1");
		}
	}

	void OnChangeScore2(int score){
		if(scoreP2UI != null) {
			scoreP2UI.GetComponent<Text>().text = "" + score;
		} else {
			scoreP2UI = GameObject.Find ("Score P2");
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (!isServer) {
			return;
		}

		//apabila bertabrakan dengan pemukul
		if (coll.gameObject.name.Contains("Pemukul")) {
			//mencatat arah pergerakan bola
			Vector2 direction = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, GetComponent<Rigidbody2D> ().velocity.y).normalized;
			//menambahkan force pada bola
			GetComponent<Rigidbody2D> ().AddForce (direction * force);
		}
		//apabila bertabrakan dengan tepi kiri
		else if (coll.gameObject.name == "Tepi Kiri") {
			scoreP2 += 1;
			ResetBall ();
			//menggerakan bola
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-2, 1).normalized * force);
		}
		//apabila bertabrakan dengan tepi kanan
		else if (coll.gameObject.name == "Tepi Kanan") {
			scoreP1 += 1;
			ResetBall ();
			//menggerakan bola
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (2, 1).normalized * force);
		}

		//biar ada suaranya
		audio.PlayOneShot(hitSound);	
	}

	//memindahkan bola ke titik awal, set kecepatan vbola menjadi 0
	void ResetBall() {
		transform.localPosition = new Vector2 (0, 0);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		if (GameObject.FindGameObjectsWithTag ("Player").Length == 1) {
			NetworkManager.singleton.StopHost ();
		}
	}
}
