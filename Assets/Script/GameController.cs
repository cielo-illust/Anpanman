using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float bgmTime = 0.0f;
	public float soundTime = 0.0f;
	public float voiceTime = 0.0f;

	AudioSource bgm;
	AudioSource sound;
	AudioSource voice;

	private float currentRemainTime = 0.0f;
	private bool bgmPlay = false;
	private bool soundPlay = false;
	private bool voicePlay = false;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		bgm = audioSources[0];
		sound = audioSources[1];
		voice = audioSources[2];
	}
	
	// Update is called once per frame
	void Update () {
		currentRemainTime += Time.deltaTime;
		if ((!bgmPlay) && (currentRemainTime >= bgmTime)) {
			bgm.Play ();
			bgmPlay = true;
		}
		if ((!soundPlay) && (currentRemainTime >= soundTime)) {
			sound.Play ();
			soundPlay = true;
		}
		if ((!voicePlay) && (currentRemainTime >= voiceTime)) {
			voice.Play ();
			voicePlay = true;
		}
	
	}
}
