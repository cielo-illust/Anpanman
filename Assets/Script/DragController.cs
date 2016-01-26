using UnityEngine;
using System.Collections;

public class DragController : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	void OnMouseDown()
	{
		print("MouseDown");
		this.screenPoint = Camera.main.WorldToScreenPoint (transform.position);
		this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	void onMouseDrag()
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + this.offset;
		transform.position = currentPosition;
	}
}
