using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating50_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyTime = 0f;
    void Start()
    {
        //yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject,DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
