using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    //public static float rMin = 1;

   // private int score = 0;
    public int score;
    private bool gameOver;
    private GameObject[] tiles;

    public GameObject player;

    public float[] values = new float[6];

    public Text scoreText;

    public float difficultyModifier;

    public GameOverMenu gom;
    public GameObject Ship1, DefaultShip;
    public int temp;
    const float stabley=-2.21f;
    public static GameMaster instance;
    private void Awake() {
        if(instance==null)
            instance=this;    
       
    }
    // Use this for initialization
    public void Start ()
    {
        // score = 0;
        // values[0] = 0f;
        // values[1] = 0.1f;
        // values[2] = 0f;
        // values[3] = 0.7f;
        // values[4] = 0.4f;
        // values[5] = 1f;

        // gameOver = false;
        if(PlayerPrefs.GetInt("PlayerShip") == 1) {
            // Debug.Log("!!!!!!here");
            Ship1.SetActive(true);
            // defaultShip.SetActive(false);
        } else {
            DefaultShip.SetActive(true);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");

        temp=PlayerPrefs.GetInt("Revived");
        
        //Debug.Log("temp:"+temp);
        if(temp==1 && PlayerPrefs.GetInt("timesrevived")<=2)
        {    
            if(PlayerPrefs.HasKey("Score"))
                score=PlayerPrefs.GetInt("Score");
            
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position=new Vector3 (player.transform.position.x,player.transform.position.y,PlayerPrefs.GetFloat("PlayerPositionZ"));
           
            Debug.Log("y position:"+player.transform.position.y);
            //added 03-12
            GameOverMenu.instance.times_continued=PlayerPrefs.GetInt("timesrevived");
            
        } else {
            score=0;
            if(PlayerPrefs.HasKey("Revived"))
            PlayerPrefs.SetInt("Revived", 0);
            //Debug.Log("in else part");
            player = GameObject.FindGameObjectWithTag("Player");
            //added 03-12
           

             //added 01-12
            //PlayerPrefs.SetInt("PlayerColorInt",0);
            //Color baseColorRed = new Color( 1f,1f,1f);
            //PlayerPrefs.SetColor("PlayerColor",baseColorRed);
        
        }
        //score = 0;
        values[0] = 0f;
        values[1] = 0.1f;
        values[2] = 0f;
        values[3] = 0.7f;
        values[4] = 0.4f;
        values[5] = 1f;


        //added 03-12
        if(GameOverMenu.instance.times_continued==2)
        {
             gom.disableButton();
        }
        if(PlayerPrefs.GetInt("timesrevived")>2)
        {
                gameOver=true;
                GameOverMenu.instance.times_continued=0;
                //gom.enableButton();
                //gom.disableButton();
        }
        else
            gameOver = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameOver)
        {
            OnGameOver();
        }
    }

    public void RecolorTiles()
    {
        tiles = GameObject.FindGameObjectsWithTag("Respawn");
        values = new float[6];

        int domiantColor = (int)UnityEngine.Random.Range(0, 5.999f);

        values[0] = UnityEngine.Random.Range(0f, values[domiantColor]);
        values[1] = UnityEngine.Random.Range(values[0], 1f);
        values[2] = UnityEngine.Random.Range(0f, values[domiantColor]);
        values[3] = UnityEngine.Random.Range(values[2], 1f);
        values[4] = UnityEngine.Random.Range(0f, values[domiantColor]);
        values[5] = UnityEngine.Random.Range(values[4], 1f);
    }

    public void IncreamentScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        if (score % 100 == 0 && score != 0)
        {
            RecolorTiles();
            SetDifficulty();
        }
    }

    private void SetDifficulty()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.SetSpeed(pc.GetSpeed() + difficultyModifier);
    }

    public int getScore()
    {
        return score;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void SetGameOver( bool over)
    {
        gameOver = over;
        string s = PlayerPrefs.GetString("HighScores");
		List<int> TopScores = new List<int>();
		if(s.Length == 0){
			TopScores.Add(score);
			PlayerPrefs.SetString("HighScores",String.Join(",", TopScores.ToArray()));
		} else {
			String[] listOfInts = s.Split(',');
			foreach(var scre in listOfInts) {
				TopScores.Add(Convert.ToInt32(scre));
			}
			TopScores.Add(score);
			TopScores.Sort();
			TopScores.Reverse();
			if(TopScores.Count > 5) {
				TopScores.RemoveAt(5);
			}
		}
		// Debug.Log("capacity: " + TopScores.Count);
		PlayerPrefs.SetString("HighScores",String.Join(",", TopScores.ToArray()));
    }

    private void OnGameOver()
    {
        gom.ToggleGameOverMenu(score);
    }
}
