using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEmitterScript : MonoBehaviour
{
    public GameObject ast_m, ast_l, ast_xxl;
    public GameObject asteroid;
    private int asteroidType;

    public float minDelay, maxDelay;
    private float nextLaunchTime;

    
    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.getIsStarted())
            return;

        float positionZ = transform.position.z;
        float positionX = Random.Range(-transform.localScale.x/2, transform.localScale.x/2);
        
        if (Time.time > nextLaunchTime) 
        {
            asteroidType = Random.Range(0, 3);
            switch (asteroidType)
            {
                case 0:
                    asteroid = ast_m;
                    break;
                case 1:
                    asteroid = ast_l;
                    break;
                case 2:
                    asteroid = ast_xxl;
                    break;
            }

            Instantiate(asteroid, new Vector3(positionX, 0, positionZ), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
        

    }
}
