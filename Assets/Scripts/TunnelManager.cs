using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour {

    public GameObject[] CylinderPrefabs;
    public GameObject[] coins;
    public GameObject bonus;
    public int count=0;
    public int basic_count=0;
    private List<GameObject> cylindersOnScreen;
    public int maxNumOfFreeCylinders = 5;
    private int curNumOfFreeCylinders;
    private int lastIndex = 0;
    GameMaster gmScript;

    //public const float ZERO = 12, ONE = 3, TWO = 2, THREE = 1, FOUR = 2;

    private Transform playerTransform;
    private float spawnZ = 0f;
    public float offset = 5f;
    private float numOfCylinders = 10;
    public float cylinderLength;

    // Use this for initialization
    void Start()
    {
        count=0;
        cylindersOnScreen = new List<GameObject>();
        curNumOfFreeCylinders = maxNumOfFreeCylinders;

        for (int i = 0; i < numOfCylinders; i++)
            SpawnCylinder();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject gm = GameObject.FindGameObjectWithTag("GameMaster");
        gmScript = gm.GetComponent<GameMaster>();

    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform.position.z - offset > (spawnZ - numOfCylinders * cylinderLength))
        {
            SpawnCylinder(ChooseIndex(), ChooseAngle());
            DeleteCylinder();

            //add points
            gmScript.IncreamentScore(1);
        }
    }

    private void SpawnCylinder(int prefabIndex = 0, float angle = 0f)
    {
        GameObject cylinder;
        if(basic_count<=0 && count<40 && prefabIndex!=0)prefabIndex=4;
        if(count==0){
            prefabIndex=0;
        } else if(count==1){
            prefabIndex=1;
        } else if(count==21){
            prefabIndex=2;
        } 
        else if(count==41){
            prefabIndex=3;
            basic_count++;
        }
        count++;
        if(count >= 60){
            count=0;
        }


        cylinder = Instantiate(CylinderPrefabs[prefabIndex]) as GameObject;
        cylinder.transform.SetParent(this.transform);

        cylinder.transform.position = Vector3.forward * spawnZ;

        cylinder.transform.rotation = transform.rotation;
        cylinder.transform.Rotate(0, 0, angle);

        cylindersOnScreen.Add(cylinder);

        PopulateCylinder(prefabIndex, cylinder);

        spawnZ += cylinderLength;
    }

    private void PopulateCylinder(int cylinderID, GameObject cylinder)
    {
        if( cylinderID ==0)
        {
            float spawnChance = Random.Range(0f,1f);
            if(basic_count==0 && count<20)return ;
            if (spawnChance >= 0.4f ){
                int color_idx = (int)(Random.Range(0f,1f)*10)%6;
                if(basic_count<=3 )color_idx=color_idx%3;
                // else if(basic_count<=2 && count<50)color_idx=color_idx%4;
                Instantiate(coins[color_idx], new Vector3(0,0,spawnZ), Quaternion.Euler(0,0, ChooseAngle()*60f), cylinder.transform);
            } else if(basic_count>3){
                int color_idx = (int)(Random.Range(0f,1f)*10)%6;
                Instantiate(coins[color_idx], new Vector3(0,0,spawnZ), Quaternion.Euler(0,0, ChooseAngle()*60f), cylinder.transform);
            }
        }
    }

    private void DeleteCylinder()
    {
        Transform t = cylindersOnScreen[0].transform;
        foreach (Transform child in t)
        {
            GameObject.Destroy(child.gameObject);
        }
        Destroy(cylindersOnScreen[0]);
        cylindersOnScreen.RemoveAt(0);
    }

    private int ChooseIndex()
    {
        if (lastIndex == 0 && curNumOfFreeCylinders == 0)
        {
            curNumOfFreeCylinders = maxNumOfFreeCylinders;
            lastIndex = (int)Random.Range(5f, 7);
            return lastIndex;
        }
        else
        {
            curNumOfFreeCylinders--;
            lastIndex = 0;
            return lastIndex;
        }
    }

    private float ChooseAngle()
    {
        return 60f * (int)Random.Range(0f, 5);
    }
}
