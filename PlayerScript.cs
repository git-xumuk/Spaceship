using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playership;
    public GameObject asteroidExplosion;

    public GameObject lazerShot;
    public GameObject littlelazerShot;
    public Transform lazerGun;
    public Transform rightGun;
    public Transform leftGun;

    public float speed;
    public float tilt = 1f;
    public float xMin, xMax, zMin, zMax;

    public float shotDelay;  // delta time betweeen shots
    private float nextShotTime;
    private float nextLittleShotTime;


    // Start is called before the first frame update
    void Start()
    {
        playership = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.getIsStarted())
            return;

        // ship's control
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        playership.velocity = new Vector3(MoveHorizontal, 0, MoveVertical) * speed;

        float restrictedX = Mathf.Clamp(playership.position.x, xMin, xMax);
        float restrictedZ = Mathf.Clamp(playership.position.z, zMin, zMax);

        playership.position = new Vector3(restrictedX, 0, restrictedZ);
        //

        // rotation is addicted to ship's velocity
        playership.rotation = Quaternion.Euler(playership.velocity.z * tilt, 0, -playership.velocity.x * tilt);

        // lazer shot
        if (Time.time > nextShotTime && Input.GetButton("Fire1")) 
        {
            Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }

        // little lazer shot
        if (Time.time > nextLittleShotTime && Input.GetButton("Fire2"))
        {
            Instantiate(littlelazerShot, rightGun.position, Quaternion.identity);
            Instantiate(littlelazerShot, leftGun.position, Quaternion.identity);
            nextLittleShotTime = Time.time + shotDelay/2;
        }

        //mega gun
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach (GameObject i in asteroids) {
                Destroy(i);

                // animation of explosion (boom)
                GameObject explosion = Instantiate(asteroidExplosion, i.transform.position, Quaternion.identity);
                explosion.transform.localScale *= AsteroidScript.size;  // change size of explosion (boom)
            }
        }
    }

}
