using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HighScore;
    private string HighScoreText = "";
    void Start()
    {
        int i = 1;
        string s = PlayerPrefs.GetString("HighScores");
        if(!(s.Length == 0)){
            String[] scores = s.Split(',');
            foreach(var score in scores) {
                if(Convert.ToInt32(score) < 0)
                    break;
                HighScoreText += score + "\n";
                i += 1;
            }
        }
        while(i <= 5) {
            HighScoreText += "--\n";
            i += 1;
        }
        // Debug.Log("Highscore: " + HighScoreText);
        HighScore.text = HighScoreText;
    }
}
