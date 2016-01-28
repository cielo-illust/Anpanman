using UnityEngine;
using System.Collections;

public class SeController : MonoBehaviour {

	private AudioSource selectLsize;
	private AudioSource selectMsize;
	private AudioSource selectSsize;
	private AudioSource selectDezato;
	private AudioSource piron;
	private AudioSource pironOK;
	private AudioSource pironOK2;
	private AudioSource pironOK3;
	private AudioSource pironOK4;
	private AudioSource shakin;
	private AudioSource shakinOK;
	private AudioSource shakinOK2;
	private AudioSource baikinEat;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		selectLsize = audioSources[0];
		selectMsize = audioSources[1];
		selectSsize = audioSources[2];
		selectDezato = audioSources[3];
		piron = audioSources[4];
		pironOK = audioSources[5];
		pironOK2 = audioSources[6];
		pironOK3 = audioSources[7];
		pironOK4 = audioSources[8];
		shakin = audioSources[9];
		shakinOK = audioSources[10];
		shakinOK2 = audioSources[11];
		baikinEat = audioSources[12];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySet(int i) {
		if (i == 0) selectLsize.Play ();
		if (i == 1) selectMsize.Play ();
		if (i == 2) selectSsize.Play ();
		if (i == 3) selectDezato.Play ();
		if (i == -1) {
			int j = Random.Range (0, 8);
			if (j == 0) piron.Play ();
			if (j == 1) pironOK.Play ();
			if (j == 2) pironOK2.Play ();
			if (j == 3) pironOK3.Play ();
			if (j == 4) pironOK4.Play ();
			if (j == 5) shakin.Play ();
			if (j == 6) shakinOK.Play ();
			if (j == 7) shakinOK2.Play ();
		}
	}

	public void PlayEat() {
		baikinEat.Play ();
	}
}
