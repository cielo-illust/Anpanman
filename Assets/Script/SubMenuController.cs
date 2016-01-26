using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SubMenuController : MonoBehaviour {
	public Cutin top;
	public Cutin black;
	public Cutin item01;
	public Cutin item02;
	public Cutin item03;
	public Cutin buttonYes;
	public Cutin buttonNo;
	public GameObject mainBgmController;
	public GameObject objItem01;

	private bool selectFlag = false;
	private bool changeScene = false;

	private Vector3 offset;
	private Vector3 mousePosition;
	private Vector3 preMousePosition;
	private bool mouseDown = false;

	// Use this for initialization
	void Start () {
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
		if (mouseDown) {
			objItem01.transform.Translate (Camera.main.ScreenToWorldPoint(Input.mousePosition) - preMousePosition);
			preMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		}
		if (Input.GetMouseButtonDown (0)) {
			mouseDown = true;
			offset = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			preMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		}
		if (Input.GetMouseButtonUp (0)) {
			mouseDown = false;
		}
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag == "Button0") {
					top.setState (0);
					changeScene = true;
					Invoke ("PrevScene", 1.1f);
				}
				if (hit.collider.gameObject.tag == "Item01") {
					black.setState (0);
					item01.setState (0);
					item02.setState (2);
					item03.setState (2);
					buttonYes.setState (0);
					buttonNo.setState (0);
					selectFlag = true;
				}
				if (hit.collider.gameObject.tag == "Item02") {
					black.setState (0);
					item01.setState (2);
					item02.setState (0);
					item03.setState (2);
					buttonYes.setState (0);
					buttonNo.setState (0);
					selectFlag = true;
				}
				if (hit.collider.gameObject.tag == "Item03") {
					black.setState (0);
					item01.setState (2);
					item02.setState (2);
					item03.setState (0);
					buttonYes.setState (0);
					buttonNo.setState (0);
					selectFlag = true;
				}
				if ((hit.collider.gameObject.tag == "ButtonYes") && (selectFlag)) {
					top.setState (0);
					changeScene = true;
					Invoke ("ChangeScene", 1.1f);
				}
				if ((hit.collider.gameObject.tag == "ButtonNo") && (selectFlag)) {
					black.setState (2);
					item01.setState (2);
					item02.setState (2);
					item03.setState (2);
					buttonYes.setState (2);
					buttonNo.setState (2);
					selectFlag = false;
				}
			}
			return;
		}
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play01");
	}
	void PrevScene()
	{
		SceneManager.LoadScene ("menu");
	}
}
