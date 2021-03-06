﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play04Controller : MonoBehaviour {
	public Animator char1;
	public Animator char2;
	public Animator char3;
	AudioSource sound;

	private GameObject playController;
	private PlayController play;

	private GameObject bentoObj1;
	private GameObject bentoObj2;
	private GameObject bentoObj3;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
		sound.Play ();

		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		GameObject playController = GameObject.FindWithTag ("PlayController");
		if (playController == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		play = playController.GetComponent<PlayController>();

		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		bentoObj1 = GameObject.FindWithTag ("BentoObj");
		if (bentoObj1 == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		bentoObj1.transform.localScale = new Vector3 (0.15f, 0.15f, transform.localScale.z);

		bentoObj1.transform.position = new Vector3 (0.0f, -1.5f, transform.localScale.z);
		bentoObj2 = (GameObject)Instantiate (bentoObj1, new Vector3 (-2.55f, -1.5f, 0.0f), Quaternion.identity);
		bentoObj3 = (GameObject)Instantiate (bentoObj1, new Vector3 (2.55f, -1.5f, 0.0f), Quaternion.identity);

		Invoke ("ShowFab", 8.0f);
		Invoke ("ShowResult", 8.0f);
		Invoke ("ChangeScene", sound.clip.length + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ChangeScene()
	{
		GameObject.Destroy (bentoObj1);
		GameObject.Destroy (bentoObj2);
		GameObject.Destroy (bentoObj3);
		SceneManager.LoadScene ("play05");
	}
	void ShowResult()
	{
		if (play.fabFlag1) {
			char1.SetBool ("OK", true);
		} else {
			char1.SetBool ("NG", true);
		}
		if (play.fabFlag2) {
			char2.SetBool ("OK", true);
		} else {
			char2.SetBool ("NG", true);
		}
		if (play.fabFlag3) {
			char3.SetBool ("OK", true);
		} else {
			char3.SetBool ("NG", true);
		}
	}
	void ShowFab()
	{
		Instantiate (play.fabObj1, new Vector3(-2.15f, 2.28f, 0.0f), Quaternion.identity);
		Instantiate (play.fabObj2, new Vector3(0.4f, 2.28f, 0.0f), Quaternion.identity);
		Instantiate (play.fabObj3, new Vector3(2.95f, 2.28f, 0.0f), Quaternion.identity);
	}
}
