using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    public float timeRemaining = 2;
    public bool timerIsRunning = false;
    public float spaceTimeRemaining = 2;
    public bool spaceTimerIsRunning = false;
    public float speed, add_speed=0f;
    private Rigidbody rb;
    public GameObject gm;
    public int count=0;
    public int coins_collected = 0;
    public int red_coins_collected = 0;
    public int green_coins_collected = 0;
    public int blue_coins_collected = 0;
    public int wrong_coins_collected = 0;
    public int color_loops_passed = 0;
    public GameObject Floating50Prefab;
    public GameObject halloweenPrefab;
    public string op="";
    GameMaster gmScript;
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        // gmScript = gm.GetComponent<GameMaster>();

        // Color baseColorRed = new Color( 1f,1f,1f);
        // Renderer rend = GetComponent<Renderer>();
        // Material mat = rend.material;
        // mat.SetColor("_Color", baseColorRed);

        rb = GetComponent<Rigidbody>();
        gmScript = gm.GetComponent<GameMaster>();

        //Color baseColorRed = new Color( 1f,1f,1f);
        //Color32 baseColorRed = new Color32(102, 0, 0, 255);
        Color baseColorBlue = new Color( 0f,0.4f,0.5f);
        Color baseColorGreen = new Color( 0.0f,0.4f,0f);

        Color baseColorRed = new Color( 0.4f,0f,0f);
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;
       
        if(PlayerPrefs.GetInt("Revived")!=0)
        {
            //Debug.Log("colorofspaceship:"+PlayerPrefs.GetString("colorofspaceship"));
            if(PlayerPrefs.GetString("colorofspaceship")=="Red")
            {
                // Debug.Log("red color");
                 mat.SetColor("_Color", baseColorRed);
            }
            else if(PlayerPrefs.GetString("colorofspaceship")=="Blue")
            {   
                mat.SetColor("_Color", baseColorBlue);
                // Debug.Log("blue color");
            }
            else
                mat.SetColor("_Color", baseColorGreen);

        }
        else
            mat.SetColor("_Color", baseColorRed);
    }

    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                add_speed=0f;
                timeRemaining = 2;
                timerIsRunning = false;
            }
        }

        if (spaceTimerIsRunning)
        {
            if (spaceTimeRemaining > 0)
            {
                spaceTimeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                add_speed=0f;
                spaceTimeRemaining = 2;
                spaceTimerIsRunning = false;
            }
        }


        // if(Input.GetKeyDown("space")){
        //     this.spaceTimerIsRunning = true;
        //     this.spaceTimeRemaining=3;
        //     Vector3 temp = new Vector3(this.transform.position.x ,this.transform.position.y+1.2f, this.transform.position.z);
        //     this.transform.position = temp;
        // }
       // AnalyticsResult analyticsResult;
        if (!gmScript.IsGameOver())
        {
            // rb.velocity = new Vector3(0, rb.velocity.y, speed);
            rb.velocity = new Vector3(0, rb.velocity.y, speed + add_speed);
            rb.AddForce(Vector3.down * 100f);

            if (this.transform.position.y < -3)
            {
                //Debug.Log("Killed by falling");
                // analyticsResult = Analytics.CustomEvent("Total coins collected : " + coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Red coins collected : " + red_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Green coins collected : " + green_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Blue coins collected : " + blue_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Mistached coins collected : " + wrong_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total colored loops encountered : " + color_loops_passed);
                // analyticsResult = Analytics.CustomEvent("Player's Speed : " + this.speed);
                // analyticsResult = Analytics.CustomEvent("Death by Pit");
                Analytics.CustomEvent("speedEvent", new Dictionary<string, object>
                        {
                            { "speed", this.speed}
                        });
                Analytics.CustomEvent("distanceEvent", new Dictionary<string, object>
                        {
                            { "distance", this.transform.position.z}
                        });

                Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
                        {
                            { "death", "pit"}
                        });
                Analytics.CustomEvent("totalCoinsEvent", new Dictionary<string, object>
                        {
                            
                            { "redCoins", red_coins_collected},
                            { "greenCoins", green_coins_collected},
                            { "blueCoins", blue_coins_collected}


                        });
                Analytics.CustomEvent("redCoinsEvent", new Dictionary<string, object>
                        {
                            { "redCoins", red_coins_collected}
                        });
                Analytics.CustomEvent("greenCoinsEvent", new Dictionary<string, object>
                        {
                            { "greenCoins", green_coins_collected}
                        });
                Analytics.CustomEvent("blueCoinsEvent", new Dictionary<string, object>
                        {
                            { "blueCoins", blue_coins_collected}
                        });
                Analytics.CustomEvent("mismatchedCoinsEvent", new Dictionary<string, object>
                        {
                            { "mismatchedCoins", wrong_coins_collected}
                        });
                Analytics.CustomEvent("totalColorLoopsEvent", new Dictionary<string, object>
                        {
                            { "totalColorLoops", color_loops_passed}
                        });
                
                gmScript.SetGameOver(true);
                rb.position=new Vector3 (rb.position.x,rb.position.y,PlayerPrefs.GetFloat("PlayerPositionZ"));
            }

        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        Color baseColorBlue = new Color( 0f,0.4f,0.5f);
        Color baseColorRed = new Color( 0.4f,0f,0f);
        Color baseColorGreen = new Color( 0.0f,0.4f,0f);
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;
        //Debug.Log("Collision:" + (mat.color==baseColorRed));
        //AnalyticsResult analyticsResult;

        if (hit.gameObject.CompareTag("Red") && (mat.color==baseColorRed)) {
            Destroy(hit.gameObject);
        } else if (hit.gameObject.CompareTag("Green") && (mat.color==baseColorGreen) ) {
            Destroy(hit.gameObject);
        } else if(hit.gameObject.CompareTag("Blue") && (mat.color==baseColorBlue) ) {
            Destroy(hit.gameObject);
        } else if(hit.gameObject.CompareTag("BlueLoop")) {
            color_loops_passed++;
            mat.SetColor("_Color", baseColorBlue);
            Destroy(hit.gameObject);
        } else if(hit.gameObject.CompareTag("GreenLoop")) {
            color_loops_passed++;
            mat.SetColor("_Color", baseColorGreen);
            Destroy(hit.gameObject);
        } else if(hit.gameObject.CompareTag("RedLoop")) {
            color_loops_passed++;
            mat.SetColor("_Color", baseColorRed);
            Destroy(hit.gameObject);
        } else if( (hit.gameObject.CompareTag("Red") && (mat.color!=baseColorRed)) || (hit.gameObject.CompareTag("Blue") && (mat.color!=baseColorBlue)) || (hit.gameObject.CompareTag("Green") && (mat.color!=baseColorGreen)) ) {
            // analyticsResult = Analytics.CustomEvent("Total coins collected : " + coins_collected);
            // analyticsResult = Analytics.CustomEvent("Total Red coins collected : " + red_coins_collected);
            // analyticsResult = Analytics.CustomEvent("Total Green coins collected : " + green_coins_collected);
            // analyticsResult = Analytics.CustomEvent("Total Blue coins collected : " + blue_coins_collected);
            // analyticsResult = Analytics.CustomEvent("Total Mistached coins collected : " + wrong_coins_collected);
            // analyticsResult = Analytics.CustomEvent("Total colored loops encountered : " + color_loops_passed);
            // analyticsResult = Analytics.CustomEvent("Player's Speed : " + this.speed);
            // analyticsResult = Analytics.CustomEvent("Death by passing through wrong colored loop");
                Analytics.CustomEvent("speedEvent", new Dictionary<string, object>
                        {
                            { "speed", this.speed}
                        });
                Analytics.CustomEvent("distanceEvent", new Dictionary<string, object>
                        {
                            { "distance", this.transform.position.z}
                        });

                Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
                        {
                            { "death", "wrong_color_loop"}
                        });
                Analytics.CustomEvent("totalCoinsEvent", new Dictionary<string, object>
                        {
                            { "redCoins", red_coins_collected},
                            { "greenCoins", green_coins_collected},
                            { "blueCoins", blue_coins_collected}
                        });
                Analytics.CustomEvent("redCoinsEvent", new Dictionary<string, object>
                        {
                            { "redCoins", red_coins_collected}
                        });
                Analytics.CustomEvent("greenCoinsEvent", new Dictionary<string, object>
                        {
                            { "greenCoins", green_coins_collected}
                        });
                Analytics.CustomEvent("blueCoinsEvent", new Dictionary<string, object>
                        {
                            { "blueCoins", blue_coins_collected}
                        });
                Analytics.CustomEvent("mismatchedCoinsEvent", new Dictionary<string, object>
                        {
                            { "mismatchedCoins", wrong_coins_collected}
                        });
                Analytics.CustomEvent("totalColorLoopsEvent", new Dictionary<string, object>
                        {
                            { "totalColorLoops", color_loops_passed}
                        });
            string spaceship_color = "";
            if(mat.color==baseColorRed)spaceship_color="Red";
            if(mat.color==baseColorBlue)spaceship_color="Blue";
            if(mat.color==baseColorGreen)spaceship_color="Green";
            string color_loop = "";
            if(hit.gameObject.CompareTag("Red"))color_loop="Red";
            if(hit.gameObject.CompareTag("Blue"))color_loop="Blue";
            if(hit.gameObject.CompareTag("Green"))color_loop="Green";
            //analyticsResult = Analytics.CustomEvent("Expected : "+ spaceship_color + " | Actual : " + color_loop);
            PlayerPrefs.SetString("colorofspaceship",spaceship_color);
            gmScript.SetGameOver(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //AnalyticsResult analyticsResult;
        Color baseColorBlue = new Color( 0f,0.4f,0.5f);
        Color baseColorRed = new Color( 0.4f,0f,0f);
        Color baseColorGreen = new Color( 0.0f,0.4f,0f);
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;
        string coin_color = "";
        if(other.gameObject.CompareTag("RedCoin"))coin_color="Red";
        if(other.gameObject.CompareTag("BlueCoin"))coin_color="Blue";
        if(other.gameObject.CompareTag("GreenCoin"))coin_color="Green";
        if(other.gameObject.CompareTag("halloween")){
            Destroy(other.gameObject);
            if(halloweenPrefab!=null){
                Vector3 pos = new Vector3(0f, 0f, 0f);
                var go = Instantiate(halloweenPrefab, transform.position+pos, Quaternion.identity, transform);
            }
            FindObjectOfType<MusicP>().PlayHalSound();
            return;
        }
        if(other.gameObject.CompareTag("life_pot")){
            this.timerIsRunning = true;
            this.timeRemaining=2;
            this.add_speed -= 2f;
            if(Floating50Prefab!=null){
                 Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().color = baseColorGreen;
                go.GetComponent<TextMesh>().text = "Speed ---";
            }
            Destroy(other.gameObject);
            return;
        }
        if(other.gameObject.CompareTag("death_pot")){
            this.timerIsRunning = true;
            this.timeRemaining=2;
            this.add_speed += 2f;
            if(Floating50Prefab!=null){
                 Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().color = baseColorRed;
                go.GetComponent<TextMesh>().text = "Speed +++";
            }
            Destroy(other.gameObject);
            return;
        }

        if(other.gameObject.CompareTag("RedCoin") && (mat.color==baseColorRed))
        {
            Debug.Log("Red Coin");
            coins_collected++;
            red_coins_collected++;
            gmScript.IncreamentScore(50);
            if(Floating50Prefab!=null){
                 Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().color = baseColorRed;
                go.GetComponent<TextMesh>().text = "+50";
            }
            FindObjectOfType<MusicP>().PlayCoinSound();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("BlueCoin")&& (mat.color==baseColorBlue))
        {

            coins_collected++;
            blue_coins_collected++;
            gmScript.IncreamentScore(50);
            if(Floating50Prefab!=null){
                 Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().color = baseColorBlue;
                go.GetComponent<TextMesh>().text = "+50";
            }
            FindObjectOfType<MusicP>().PlayCoinSound();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("GreenCoin") && (mat.color==baseColorGreen))
        {

            coins_collected++;
            green_coins_collected++;
            gmScript.IncreamentScore(50);
            if(Floating50Prefab!=null){
                Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().color = baseColorGreen;
                go.GetComponent<TextMesh>().text = "+50";
            }
            FindObjectOfType<MusicP>().PlayCoinSound();
            Destroy(other.gameObject);
        } else if(!other.gameObject.CompareTag("life_pot") && !other.gameObject.CompareTag("death_pot") &&!other.gameObject.CompareTag("RedCoin") && (mat.color==baseColorRed) || (!other.gameObject.CompareTag("BlueCoin")&& (mat.color==baseColorBlue)) || (!other.gameObject.CompareTag("GreenCoin") && (mat.color==baseColorGreen))){
            wrong_coins_collected++;
            gmScript.IncreamentScore(5);
            if(Floating50Prefab!=null){
                Color ccolor = new Color( 0f,0.4f,0.5f);;
                Vector3 pos = new Vector3(0f, 1.3f, 0f);
                var go = Instantiate(Floating50Prefab, transform.position+pos, Quaternion.identity, transform);
                if(other.gameObject.CompareTag("RedCoin"))ccolor=baseColorRed;
                if(other.gameObject.CompareTag("BlueCoin"))ccolor=baseColorBlue;
                if(other.gameObject.CompareTag("GreenCoin"))ccolor=baseColorGreen;
                if(ccolor!=null && (other.gameObject.CompareTag("RedCoin") || other.gameObject.CompareTag("BlueCoin") || other.gameObject.CompareTag("GreenCoin")))go.GetComponent<TextMesh>().color = ccolor;
                go.GetComponent<TextMesh>().text = "+5";
            }
            Destroy(other.gameObject);
            if(gmScript.getScore()<0){
                // analyticsResult = Analytics.CustomEvent("Total coins collected : " + coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Red coins collected : " + red_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Green coins collected : " + green_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Blue coins collected : " + blue_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total Mistached coins collected : " + wrong_coins_collected);
                // analyticsResult = Analytics.CustomEvent("Total colored loops encountered : " + color_loops_passed);
                // analyticsResult = Analytics.CustomEvent("Player's Speed : " + this.speed);
                // analyticsResult = Analytics.CustomEvent("Death by mismatched coins collected");
                Analytics.CustomEvent("speedEvent", new Dictionary<string, object>
                        {
                            { "speed", this.speed}
                        });
                Analytics.CustomEvent("distanceEvent", new Dictionary<string, object>
                        {
                            { "distance", this.transform.position.z}
                        });

                Analytics.CustomEvent("deathEvent", new Dictionary<string, object>
                        {
                            { "death", "wrong_coins_collected"}
                        });
                Analytics.CustomEvent("totalCoinsEvent", new Dictionary<string, object>
                        {
                            { "totCoins", coins_collected}
                        });
                Analytics.CustomEvent("redCoinsEvent", new Dictionary<string, object>
                        {
                            { "redCoins", red_coins_collected}
                        });
                Analytics.CustomEvent("greenCoinsEvent", new Dictionary<string, object>
                        {
                            { "greenCoins", green_coins_collected}
                        });
                Analytics.CustomEvent("blueCoinsEvent", new Dictionary<string, object>
                        {
                            { "blueCoins", blue_coins_collected}
                        });
                Analytics.CustomEvent("mismatchedCoinsEvent", new Dictionary<string, object>
                        {
                            { "mismatchedCoins", wrong_coins_collected}
                        });
                Analytics.CustomEvent("totalColorLoopsEvent", new Dictionary<string, object>
                        {
                            { "totalColorLoops", color_loops_passed}
                        });
                gmScript.SetGameOver(true);
            }
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return this.speed;
    }
}
