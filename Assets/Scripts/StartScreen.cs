using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {
    public Button startButton; 
    public Button quitButton;

    // Use this for initialization
    void Start () {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(CloseGame);
    }
	
    /* Transition to gameplay scene. */
    void StartGame()
    {
        SceneManager.LoadScene("test");
    }

    /* Close application. */
    void CloseGame()
    {
        Application.Quit();
    }
}
