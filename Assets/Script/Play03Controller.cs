using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play03Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider.gameObject.tag == "Button0") {
				Invoke ("ChangeScene", 0.0f);
			}
			return;
		}
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play04");
	}
}
