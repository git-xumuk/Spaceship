using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion; 
    public GameObject playerExplosion;

    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float minSize, maxSize;

    public static float size;
     
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        size = Random.Range(minSize, maxSize);
        asteroid.transform.localScale *= size;  // change size of asteroid

        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        float speed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "GameBoundary" || other.tag == "Shield" || other.tag == "EnemyShot")
            return;

        var playerLife = GameControllerScript.instance.shieldLevel;
        if (other.tag == "Player")
        {
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
        
        if (other.tag == "PlayerShot")
            GameControllerScript.instance.increaseScore(10);

        Destroy(gameObject);

        // animation of explosion (boom)
        GameObject explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= size;  // change size of explosion (boom)

    }
}
