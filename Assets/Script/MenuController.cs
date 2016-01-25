using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public CharactorController batako;
	public Cutin top;
	public GameObject mainBgmController;

	private float currentRemainTime = 0.0f;
	private bool changeScene = false;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this); // シーンが変わっても消えないオブジェクトに指定
		sound = GetComponent<AudioSource>();

		GameObject obj = GameObject.FindWithTag ("MainBgmController");
		if (obj == null) {
			Instantiate (mainBgmController);
		}
	}

	// Update is called once per frame
	void Update () {
		if (changeScene) {
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider.gameObject.tag == "Button0") {
				batako.setState ("Select", true);
				sound.Play ();
				changeScene = true;
				Invoke ("PreChangeScene", 1.0f);
				Invoke ("ChangeScene", 2.0f);
				Invoke ("EndScene", sound.clip.length);
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
	}
	void PreChangeScene()
	{
		top.setState (0);
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("subMenu");
	}
	void EndScene()
	{
		GameObject.Destroy (gameObject);
	}
}
