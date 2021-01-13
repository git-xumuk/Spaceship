using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEmitterScript : MonoBehaviour
{
    public GameObject shield;
    

    public float minDelay, maxDelay;
    private float nextLaunchTime;

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.getIsStarted())
            return;

        float positionZ = Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);
        float positionX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
        
        if (Time.time > nextLaunchTime)
        {
            Instantiate(shield, new Vector3(positionX, 0, positionZ), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
