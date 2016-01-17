using UnityEngine;
using System.Collections;

public class CharactorController : MonoBehaviour {
	public float time = 1.5f;

	Animator animator;
	private float currentRemainTime = 0.0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool ("Start")) {
			currentRemainTime += Time.deltaTime;
			if (currentRemainTime >= time) {
				setState ("Start", false);
			}
			return;
		}
		if (!animator.GetBool ("Start")) {
			if (Random.Range(0, 50) == 1) {
				animator.SetTrigger("Trg");
			}
			return;
		}
	}

	public void setState(string state, bool value) {
		animator.SetBool (state, value);
	}
}
