using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play02Controller : MonoBehaviour {

	AudioSource BGM;
	AudioSource BGMonVoice;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM = audioSources[0];
		BGMonVoice = audioSources[1];
		BGMonVoice.Play ();

		Invoke ("ChangeScene", 3.0f);
	}

	// Update is called once per frame
	void Update () {

	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play03");
	}
}
