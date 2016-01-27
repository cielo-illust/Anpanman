using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play02Controller : MonoBehaviour {

	AudioSource BGM;
	AudioSource BGMonVoice;
	AudioSource voiceFaceFinish;
	private bool changeScene = false;

	private GameObject playController;
	private PlayController play;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM = audioSources[0];
		BGMonVoice = audioSources[1];
		voiceFaceFinish = audioSources[2];

		BGMonVoice.Play ();

		//Invoke ("PlayFaceFinish", 3.0f);
		Invoke ("PlayNoVoice", BGMonVoice.clip.length);

		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		GameObject playController = GameObject.FindWithTag ("PlayController");
		if (playController == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		play = playController.GetComponent<PlayController>();

		// 表示
		//play.ShowFoods ();
		play.Show (0);
	}

	// Update is called once per frame
	void Update () {
		if (changeScene) {
			return;
		}
		if (play.state == 2) {
			changeScene = true;
			Invoke ("NextScene", 0.0f);
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "ButtonYes") {
					changeScene = true;
					Invoke ("NextScene", 0.0f);
				}
				if (hit.collider.gameObject.tag == "ButtonNo") {
					changeScene = true;
					Invoke ("PrevScene", 0.0f);
				}
			}
			return;
		}
	}
	void NextScene()
	{
		SceneManager.LoadScene ("play03");
	}
	void PrevScene()
	{
		SceneManager.LoadScene ("play02");
	}
	void PlayNoVoice()
	{
		BGM.Play ();
	}
	void PlayFaceFinish()
	{
		voiceFaceFinish.Play ();
	}
}
