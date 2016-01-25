using UnityEngine;
using System.Collections;

public class PlayController : MonoBehaviour {

	public int fab1 = 1;
	public int fab2 = 1;
	public int fab3 = 1;
	public bool fabFlag1 = false;
	public bool fabFlag2 = false;
	public bool fabFlag3 = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this); // シーンが変わっても消えないオブジェクトに指定
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
