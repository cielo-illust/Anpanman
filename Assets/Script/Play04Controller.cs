using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play04Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("ChangeScene", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ChangeScene()
	{
		SceneManager.LoadScene ("play05");
	}
}
