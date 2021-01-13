using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitterScript : MonoBehaviour
{
    public GameObject enemy;

    public float minDelay, maxDelay;
    private float nextLaunchTime;


    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.getIsStarted())
            return;

        float positionZ = transform.position.z;
        float positionX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

        if (Time.time > nextLaunchTime)
        {
            Instantiate(enemy, new Vector3(positionX, 0, positionZ), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
