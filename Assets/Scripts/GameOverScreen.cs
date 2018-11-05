using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    public Button goBack;
    public Button tryAgain;

    // Use this for initialization
    void Start ()
    {
        // Set up button listeners
        goBack.onClick.AddListener(ToStartScreen);
        tryAgain.onClick.AddListener(StartGame);
    }

    /* Transition to Start Screen scene. */
    void ToStartScreen()
    {
        SceneManager.LoadScene(0);
    }

    /* Restart gameplay. */
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
