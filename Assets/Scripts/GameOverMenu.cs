using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public Text scoreText;
    bool toggle = false;
    private float transitionAlpha = 0;

    public Image backgroundImg;
    public float maxTrasitionAlpha;

    //added
    bool revived=false;
    public GameMaster gamemaster;
    public float PlaceZ;
    public float PlaceY;

    //added 03-12
    public int times_continued=0;
    public Button ContinueButton;
    public Text ContinueText;

    public static GameOverMenu instance;
    private void Awake() {
        if(instance==null)
            instance=this;    
       
    }

    // Use this for initialization
    void Start ()
    {
        gameObject.SetActive(false);
        toggle = false;
        transitionAlpha = 0;
        ContinueButton.interactable=true;
    }
	
	// Update is called once per frame
	void Update () {
        int left = 2-times_continued;
        ContinueText.text = "Continue (" + left + ") left"; 
        if (toggle)
        {
            if(transitionAlpha < maxTrasitionAlpha)
                transitionAlpha += Time.deltaTime;
            backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, transitionAlpha), transitionAlpha);
        }
        //added 13-11
		if(revived)
        {
            ContinueAfterDeath();
        }
		
	}

    public void ToggleGameOverMenu(int score)
    {
        FindObjectOfType<MusicP>().DeathSound();
        //added
        FindObjectOfType<PausedResume>().PauseButtonRemove();
        gameObject.SetActive(true);
        scoreText.text = score.ToString();
        toggle = true;
    }

    public void PlayAgain()
    {
        //added 15-11
        PlayerPrefs.SetInt("Revived", 0);
        //added 04-12 
        ContinueButton.interactable=true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMaster gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void GoToTitleMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //added
     public void ContinueRevive()
    {
        revived=true;
    }
    public void ContinueAfterDeath()
    {
        //added 03-12
        PlayerPrefs.SetInt("timesrevived",times_continued+1);
        // int left = 2 -times_continued;
       // Debug.Log("times_continued:"+times_continued);
        PlayerPrefs.SetInt("Revived", 1);
        //Debug.Log(PlayerPrefs.GetInt("Revived"));
        PlayerPrefs.SetInt("Score", GameMaster.instance.score);
        //PlaceY=Random.Range(-2.2100f,-2.2112f);
        //PlaceZ=Random.Range(3.0f,4.8f);
        PlaceZ=Random.Range(3.0f,5.0f);
        //PlayerPrefs.SetFloat("PlayerPositionY",PlaceY);//GameMaster.instance.player.transform.position.y+
        //Debug.Log(GameMaster.instance.player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ",GameMaster.instance.player.transform.position.z+PlaceZ);
        //Debug.Log(GameMaster.instance.player.transform.position.z);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void disableButton()
    {
        ContinueButton.interactable=false;
        ContinueText.color= new Color(0,0,0,0);

        //Debug.Log("disableButton");
    }

}