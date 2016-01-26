using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play05Controller : MonoBehaviour {

	public GameObject result1;
	public GameObject result2;
	public GameObject result3;

	AudioSource BGM1;
	AudioSource BGM1onVoice;
	AudioSource BGM2;
	AudioSource BGM2onVoice;
	AudioSource BGM3;
	AudioSource BGM3onVoice;

	private GameObject playController;
	private PlayController play;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM1 = audioSources[0];
		BGM1onVoice = audioSources[1];
		BGM2 = audioSources[2];
		BGM2onVoice = audioSources[3];
		BGM3 = audioSources[4];
		BGM3onVoice = audioSources[5];

		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		GameObject playController = GameObject.FindWithTag ("PlayController");
		if (playController == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		play = playController.GetComponent<PlayController>();

		// リザルト確認
		int resultCnt = 0;
		if (play.fabFlag1) {
			resultCnt++;
		}
		if (play.fabFlag2) {
			resultCnt++;
		}
		if (play.fabFlag3) {
			resultCnt++;
		}
		if (resultCnt >= 2) {
			Instantiate (result1);
			BGM1onVoice.Play ();
			Invoke ("PlayNoVoice1", BGM1onVoice.clip.length);
		}
		if (resultCnt == 1) {
			Instantiate (result2);
			BGM2onVoice.Play ();
			Invoke ("PlayNoVoice2", BGM2onVoice.clip.length);
		}
		if (resultCnt == 0) {
			Instantiate (result3);
			BGM3onVoice.Play ();
			Invoke ("PlayNoVoice3", BGM3onVoice.clip.length);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "ButtonHome") {
					Invoke ("HomeScene", 0.0f);
				}
				if (hit.collider.gameObject.tag == "ButtonRetry") {
					Invoke ("RetryScene", 0.0f);
				}
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
	void PlayNoVoice1()
	{
		BGM1.Play ();
	}
	void PlayNoVoice2()
	{
		BGM2.Play ();
	}
	void PlayNoVoice3()
	{
		BGM3.Play ();
	}
}
