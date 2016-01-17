using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour {
	public void Update() {
		if (!Application.isShowingSplashScreen) {
			SceneManager.LoadScene ("title");
		}
	}
}
