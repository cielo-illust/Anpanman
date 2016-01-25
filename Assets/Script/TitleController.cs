using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public float bgmTime = 8.0f;
	public float soundTime = 2.5f;
	public float voiceTime = 4.7f;
	public float endTime = 10.0f;
	public GameObject mainBgmControllerPrefab;

	AudioSource sound;
	AudioSource voice;

	private float currentRemainTime = 0.0f;
	private bool bgmPlay = false;
	private bool soundPlay = false;
	private bool voicePlay = false;
	private bool endFlag = false;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound = audioSources[0];
		voice = audioSources[1];
	}

	// Update is called once per frame
	void Update () {
		currentRemainTime += Time.deltaTime;
		if ((!bgmPlay) && (currentRemainTime >= bgmTime)) {
			Instantiate (mainBgmControllerPrefab);
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
		if ((!endFlag) && (currentRemainTime >= endTime)) {
			endFlag = true;
			SceneManager.LoadScene ("menu");
		}
	}
}
