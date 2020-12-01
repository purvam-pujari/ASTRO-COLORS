using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void LoadHighScores() {
        // Debug.Log("here");
        SceneManager.LoadScene("HighScore");
    }

    public void LoadTutorial() {
        // Debug.Log("here");
        SceneManager.LoadScene("Tutorial");
    }
}
