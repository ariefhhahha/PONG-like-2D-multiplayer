using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour {

	public AudioSource klik;
	public GameObject panelMenu;
	public GameObject panelExit;

	// Use this for initialization
	void Start () {
		panelExit.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(){
		klik.Play ();
		SceneManager.LoadScene (1);
	}

	public void Credits(){
		klik.Play ();
		SceneManager.LoadScene (2);
	}

	public void Back(){
		klik.Play ();
		SceneManager.LoadScene (0);
	}

	public void PanelExit(){
		klik.Play ();
		panelMenu.SetActive (false);
		panelExit.SetActive (true);
	}

	public void Cancel(){
		klik.Play ();
		panelExit.SetActive (false);
		panelMenu.SetActive(true);
	}

	public void Exit(){
		klik.Play ();
		Application.Quit();
	}
}
