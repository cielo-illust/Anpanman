using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play04Controller : MonoBehaviour {
	public Animator char1;
	public Animator char2;
	public Animator char3;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
		sound.Play ();
		Invoke ("ChangeScene", 16.5f);
		Invoke ("ShowResult", 8.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play05");
	}
	void ShowResult()
	{
		char1.SetBool("OK", true);
		char2.SetBool("NG", true);
		char3.SetBool("OK", true);
	}
}
