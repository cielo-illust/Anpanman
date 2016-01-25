using UnityEngine;
using System.Collections;

public class ScrollBgController : MonoBehaviour {
	public float speed = 2.0f;
	public float width = 15.0f;

	private float startPos;
	void Start () {
		startPos = transform.position.x;
	}
	void Update () {
		transform.Translate (speed * Time.deltaTime, speed * Time.deltaTime, 0);
		if (transform.position.x >= startPos + (width / 2.0f)) {
			ScrollEnd ();
		}
	}

	void ScrollEnd () {
		transform.Translate (-width, -width, 0);
		SendMessage ("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
	}
}
