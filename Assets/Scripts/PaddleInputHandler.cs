using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaddleInputHandler : NetworkBehaviour {

	public int moveSpeed = 5; //kecepatan pergerakan
	public string keyControl; //mengatur input keyboard

	int currentState; //mencatat state saat ini

	//var newSprite : Sprite;
	public Sprite pedal1;

	const int NORMAL = 0;
	const int STUCK_ATAS = 1;
	const int STUCK_BAWAH = 2;

	public override void OnStartLocalPlayer ()
	{
		GetComponent<SpriteRenderer> ().sprite = pedal1;
		currentState = NORMAL;
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		//menghitung perpindahan posisi y berdasarkan input keyboard dan kecepatan pergerakan
		float yChange = Input.GetAxis (keyControl) * moveSpeed * 0.2f;

		//pengaturan berdasarkan state
		switch (currentState) {
		case STUCK_ATAS:
			if (yChange < 0) {
				currentState = NORMAL;
			}
		break;
		case STUCK_BAWAH:
			if (yChange > 0) {
				currentState = NORMAL;
			}
		break;
		default:
		//memindahkan pemukul
			transform.position = new Vector3 (transform.position.x, transform.position.y + yChange, transform.position.z);
		break;
		}
	}

	//ngatur benturan. jadi bisa kedeteksi kalo si padle ini nabrak tepi atas atau bawah
	void OnCollisionEnter2D(Collision2D coll) { //ngisi variable coll dengan component collision2D
		if (coll.gameObject.name == "Tepi Atas") { //coll itu adalah component yang ada di gameobject bernama "Tepi Atas"
			currentState = STUCK_ATAS;
		} else if (coll.gameObject.name == "Tepi Bawah") { //coll itu adalah component yang ada di gameobject bernama "Tepi Bawah"
			currentState = STUCK_BAWAH;
		}

		if (coll.gameObject.CompareTag ("bola")) {
			GetComponent<SpriteRenderer> ().color = new Color32 (150, 150, 150, 255);
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("bola")) {
			GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
		}
	}
}
