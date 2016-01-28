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
	private bool slideFlag = false;

	private float vx;
	private float vy;
	private float u = 100.5f;
	private float currentRemainTime = 0.0f;

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
		/*
		if (slideFlag) {
			currentRemainTime += Time.deltaTime;
			float x = 0.0f;
			if ((vx > 0.0f) && (vx - (u * currentRemainTime) > 0.0f)){
				x = (vx - (u * currentRemainTime)) * Time.deltaTime;
			} else if ((vy < 0.0f) && (vx + (u * currentRemainTime) < 0.0f)) {
				x = (vx + (u * currentRemainTime)) * Time.deltaTime;
			}
			float y = 0.0f;
			if ((vy > 0.0f) && (vy - (u * currentRemainTime) > 0.0f)) {
				y = (vy - (u * currentRemainTime)) * Time.deltaTime;
			} else if ((vy < 0.0f) && (vy + (u * currentRemainTime) < 0.0f)) {
				y = (vy + (u * currentRemainTime)) * Time.deltaTime;
			}
			if ((x == 0.0f) && (y == 0.0f)) {
				slideFlag = false;
			} else {
				objItem01.transform.Translate (new Vector3(x, y, 0.0f));
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			mouseDown = true;
			offset = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			preMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		}
		if (Input.GetMouseButtonUp (0)) {
			mouseDown = false;
			currentRemainTime = 0.0f;
			mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));

			vx = (mousePosition.x - preMousePosition.x) / Time.deltaTime;
			if (vx > 50.0f) {
				vx = 50.0f;
			}
			if (vx < -50.0f) {
				vx = -50.0f;
			}
			vy = (mousePosition.y - preMousePosition.y) / Time.deltaTime;
			if (vy > 50.0f) {
				vy = 50.0f;
			}
			if (vy < -50.0f) {
				vy = -50.0f;
			}
			print ("mousePosition.x = " + mousePosition.x);
			print ("preMousePosition.x = " + preMousePosition.x);
			print ("(mousePosition.x - preMousePosition.x) = " + (mousePosition.x - preMousePosition.x));
			print ("Time.deltaTime = " + Time.deltaTime);
			print ("vx = " + vx);
			slideFlag = true;
		}
		if (mouseDown) {
			slideFlag = false;
			objItem01.transform.Translate (Camera.main.ScreenToWorldPoint(Input.mousePosition) - preMousePosition);
			preMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		}
		*/
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
