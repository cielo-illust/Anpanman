using UnityEngine;
using System.Collections;

public class CharactorController : MonoBehaviour {
	public bool start = true;
	public float time = 1.5f;
	public int range = 100;

	Animator animator;
	AnimatorStateInfo state;
	private float currentRemainTime = 0.0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		state = animator.GetCurrentAnimatorStateInfo (0);
		if (start) {
			if (animator.GetBool ("Start")) {
				currentRemainTime += Time.deltaTime;
				if (currentRemainTime >= time) {
					setState ("Start", false);
				}
				return;
			}
			if (state.fullPathHash == Animator.StringToHash("Base Layer.idle")) {
				if (Random.Range (0, range) == 1) {
					animator.SetTrigger ("Trg");
				}
				return;
			}
		} else {
			if (state.fullPathHash == Animator.StringToHash("Base Layer.idle")) {
				if (Random.Range (0, range) == 1) {
					animator.SetTrigger ("Trg");
				}
			}
			return;
		}
	}

	public void setState(string state, bool value) {
		animator.SetBool (state, value);
	}
}
