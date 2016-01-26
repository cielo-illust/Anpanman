using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play03Controller : MonoBehaviour {

	private GameObject playController;
	private PlayController play;

	// Use this for initialization
	void Start () {
		// Playコントローラーがない場合は無条件でメニュー画面に戻る
		GameObject playController = GameObject.FindWithTag ("PlayController");
		if (playController == null) {
			SceneManager.LoadScene ("menu");
			return;
		}
		play = playController.GetComponent<PlayController>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "Button0") {
					Invoke ("ChangeScene", 0.0f);
				}
			}
			return;
		}
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play04");
	}
}
