using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play01Controller : MonoBehaviour {
	public Cutin top;
	public Cutin black;
	public Cutin buttonRetry;
	public Cutin buttonHome;
	public Cutin buttonOpenMenu;
	public Cutin buttonCloseMenu;

	AudioSource BGM;
	AudioSource BGMonVoice;

	private float currentRemainTime = 0.0f;
	private bool menuFlag = false;


	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM = audioSources[0];
		BGMonVoice = audioSources[1];
		BGMonVoice.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider.gameObject.tag == "Button0") {
				Invoke ("ChangeScene", 0.0f);
			}
			if ((hit.collider.gameObject.tag == "ButtonOpenMenu") && (!menuFlag)) {
				menuFlag = true;
				black.setState(0);
				buttonRetry.setState(0);
				buttonHome.setState(0);
			}
			if ((hit.collider.gameObject.tag == "ButtonCloseMenu") && (menuFlag)) {
				menuFlag = false;
				black.setState(2);
				buttonRetry.setState(2);
				buttonHome.setState(2);
			}
			if ((hit.collider.gameObject.tag == "ButtonRetry") && (menuFlag)) {
				Invoke ("PrevScene", 0.0f);
			}
			if ((hit.collider.gameObject.tag == "ButtonHome") && (menuFlag)) {
				Invoke ("PrevScene", 0.0f);
			}
			return;
		}
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play02");
	}
	void PrevScene()
	{
		SceneManager.LoadScene ("menu");
	}
}
