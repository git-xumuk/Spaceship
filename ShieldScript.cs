using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour
{
    public GameObject shield;
    public static float shieldLevel = 0f;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
        Destroy(shield, 6.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "GameBoundary" || other.tag == "Asteroid")
            return;
            
        if (other.tag == "Player")
        {
            GameControllerScript.instance.increaseShield();
            Destroy(gameObject);
        } 
    }
}
