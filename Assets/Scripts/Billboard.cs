using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera playerCamera;
    void Start()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>(); 
    }
    void LateUpdate()
    {
        transform.LookAt(playerCamera.transform);
    }
}