using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SubMenuController : MonoBehaviour {
	public Cutin top;

	private float currentRemainTime = 0.0f;
	private bool changeScene = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider.gameObject.tag == "Button0") {
				top.setState(0);
				changeScene = true;
			}
			if (hit.collider.gameObject.tag == "Button1") {
				top.setState(0);
				changeScene = true;
			}
			return;
		}
		if (changeScene) {
			currentRemainTime += Time.deltaTime;
			if (currentRemainTime >= 1.1f) {
				SceneManager.LoadScene ("menu");
			}
			return;
		}	
	}
}
