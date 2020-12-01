using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedResume : MonoBehaviour
{
    public GameObject PausedScreen;
    public GameObject PauseButton;
    bool GamePaused;
    // Start is called before the first frame update
    void Start()
    {
        GamePaused=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GamePaused)
            Time.timeScale=0;
        else
        {
            Time.timeScale=1;
        } 
    }

    public void PauseGame()
    {
        GamePaused=true;
        PausedScreen.SetActive(true);
        PauseButton.SetActive(false);
        FindObjectOfType<MusicP>().SoundControlPause();
    }
    public void ResumeGame()
    {
        GamePaused=false;
        PausedScreen.SetActive(false);
        PauseButton.SetActive(true);
        FindObjectOfType<MusicP>().SoundControlResume();
    }
     //added
    public void PauseButtonRemove()
    {
      PauseButton.SetActive(false);   
    }   
}
