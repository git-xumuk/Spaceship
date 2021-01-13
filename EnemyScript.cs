using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyExplosion, playerExplosion;

    public float minSpeed, maxSpeed;

    public GameObject enemyLazerShot;
    public Transform EnemyGun;

    public float shotDelay;  // time betweeen shots
    private float nextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody enemy = GetComponent<Rigidbody>();

        float speed = Random.Range(minSpeed, maxSpeed);
        enemy.velocity = new Vector3(0, 0, -speed);
    }

    void Update()
    {
        //lazer shot
        if (Time.time > nextShotTime)
        {
            Instantiate(enemyLazerShot, EnemyGun.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary" || other.tag == "EnemyShot" || other.tag == "Shield")
            return;

        var playerLife = GameControllerScript.instance.shieldLevel;

        if (other.tag == "Player")
        {
            if (playerLife != 0)  //  > 0
            {
                GameControllerScript.instance.decreaseShield();
                playerLife--;
            } else
            {
                GameControllerScript.instance.shieldLevel -= 1;
                Destroy(other.gameObject);
                Instantiate(playerExplosion, transform.position, Quaternion.identity);
            }
        }

        if (other.tag == "PlayerShot")
            GameControllerScript.instance.increaseScore(15);

        Destroy(gameObject);
        Instantiate(enemyExplosion, transform.position, Quaternion.identity);
    }
}
