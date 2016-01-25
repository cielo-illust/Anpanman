using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play05Controller : MonoBehaviour {

	AudioSource BGM1;
	AudioSource BGM1onVoice;
	AudioSource BGM2;
	AudioSource BGM2onVoice;
	AudioSource BGM3;
	AudioSource BGM3onVoice;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM1 = audioSources[0];
		BGM1onVoice = audioSources[1];
		BGM2 = audioSources[2];
		BGM2onVoice = audioSources[3];
		BGM3 = audioSources[4];
		BGM3onVoice = audioSources[5];
		BGM1onVoice.Play ();
		Invoke ("PlayNoVoice", BGM1onVoice.clip.length);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider.gameObject.tag == "ButtonHome") {
				Invoke ("HomeScene", 0.0f);
			}
			if (hit.collider.gameObject.tag == "ButtonRetry") {
				Invoke ("RetryScene", 0.0f);
			}
			return;
		}
	}
	void HomeScene()
	{
		SceneManager.LoadScene ("menu");
	}
	void RetryScene()
	{
		SceneManager.LoadScene ("play01");
	}
	void PlayNoVoice()
	{
		BGM1.Play ();
	}
}
