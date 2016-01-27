using UnityEngine;
using System.Collections;

public class DontDestroyController : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad (this); // シーンが変わっても消えないオブジェクトに指定
	}
}
