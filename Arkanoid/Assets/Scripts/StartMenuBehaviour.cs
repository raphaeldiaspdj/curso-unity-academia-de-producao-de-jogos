using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuBehaviour : MonoBehaviour {

    public GameObject creditsPanel;

    public void pressPlay()
    {
        SceneManager.LoadScene("Scenes/MainGame");
    }

    public void pressCredits(bool show)
    {
        creditsPanel.SetActive(show);
    }


}
