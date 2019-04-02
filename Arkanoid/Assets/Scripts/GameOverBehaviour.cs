using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
			SceneManager.LoadScene (0, LoadSceneMode.Single);
	}
}
