using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
	public CharactorController batako;
	public Cutin top;

	private float currentRemainTime = 0.0f;
	private bool changeScene = false;
	private bool endFlag = false;

	AudioSource sound;
	AudioSource BGM;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this); // シーンが変わっても消えないオブジェクトに指定
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound = audioSources[0];
		BGM = audioSources[1];

		GameObject obj = GameObject.FindWithTag ("TitleController");
		if (obj == null) {
			BGM.Play ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (endFlag) {
			return;
		}
		if (!changeScene) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
				if (hit.collider.gameObject.tag == "Button0") {
					top.setState (0);
					changeScene = true;
				}
				if (hit.collider.gameObject.tag == "Button1") {
					batako.setState ("Select", true);
					sound.Play ();
					changeScene = true;
					Invoke ("PreChangeScene", 1.0f);
					Invoke ("ChangeScene", 2.0f);
					Invoke ("EndScene", sound.clip.length);
				}
				return;
			}
//		} else {
//			currentRemainTime += Time.deltaTime;
//			if (currentRemainTime >= 1.1f) {
//				SceneManager.LoadScene ("subMenu");
//				endFlag = true;
//			}
//			return;
		}
	}
	void PreChangeScene()
	{
		top.setState (0);
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("subMenu");
		endFlag = true;
	}
	void EndScene()
	{
		GameObject.Destroy (gameObject);
	}
//	void OnGUI(){
//		string label = "aaa = " + aaa;
//		GUI.Label (new Rect (0, 0, 100, 30), label);
//	}
}
