using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(0, -1, player.transform.position.z - 3);
    }
}