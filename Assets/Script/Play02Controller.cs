using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play02Controller : MonoBehaviour {

	public Cutin finish;
	public Cutin buttonYes;
	public Cutin buttonNo;
	public BoxCollider2D buttonYesCol;
	public BoxCollider2D buttonNoCol;

	private AudioSource BGM;
	private AudioSource BGMonVoice;
	private AudioSource voiceFinish;

	private bool changeScene = false;
	private bool preNextScene = false;

	private GameObject playController;
	private PlayController play;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM = audioSources[0];
		BGMonVoice = audioSources[1];
		voiceFinish = audioSources[2];

		BGMonVoice.Play ();
		Invoke ("PlayNoVoice", BGMonVoice.clip.length);

		buttonYesCol.enabled = false;
		buttonNoCol.enabled = false;

		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		GameObject playController = GameObject.FindWithTag ("PlayController");
		if (playController == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		play = playController.GetComponent<PlayController>();

		// 表示
		play.InitObj();
		play.Show (-1);
	}

	// Update is called once per frame
	void Update () {
		if (changeScene) {
			return;
		}
		if ((!preNextScene) && (play.state == 100)) {
			preNextScene = true;
			voiceFinish.Play ();
			Invoke ("ShowFinish", 3.0f);
			Invoke ("ConfirmNextScene", voiceFinish.clip.length);
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "ButtonYes") {
					changeScene = true;
					finish.setState (2);
					buttonYes.setState (2);
					buttonNo.setState (2);
					buttonYesCol.enabled = false;
					buttonNoCol.enabled = false;
					play.state = 3;
					Invoke ("NextScene", 2.5f);
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
	void ShowFinish()
	{
		finish.setState (0);
	}
	void ConfirmNextScene()
	{
		buttonYesCol.enabled = true;
		buttonNoCol.enabled = true;
		buttonYes.setState (0);
		buttonNo.setState (0);
	}
}
