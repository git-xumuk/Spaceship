using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    public float speed;
    public GameObject playerExplosion, enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "Enemy" || other.tag == "Shield")
            return;

        var playerLife = GameControllerScript.instance.shieldLevel;
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            if (playerLife != 0)  //  > 0
            {
                GameControllerScript.instance.decreaseShield();
                playerLife--;
            }
            else
            {
                GameControllerScript.instance.shieldLevel -= 1;
                Destroy(other.gameObject);
                Instantiate(playerExplosion, transform.position, Quaternion.identity);
            }
        }

        
    }
}
