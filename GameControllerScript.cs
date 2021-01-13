using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public Text scoreText;
    public Text shieldLevelText;

    public Button startButton;
    public GameObject menu;
  
    public static GameControllerScript instance;

    int score = 0;
    public int shieldLevel = 0;

    bool isStarted = false;

    public bool getIsStarted()
    {
        return isStarted;
    }

    public void increaseScore(int increment)
    {
        score += increment;
        scoreText.text = "Score: " + score;
    }
    
    public void increaseShield()
    {
        shieldLevel += 1;
        shieldLevelText.text = "Shield level: " + shieldLevel;
    }

    public void decreaseShield()
    {
        shieldLevel -= 1;
        shieldLevelText.text = "Shield level: " + shieldLevel;
    }

    private void Start()
    {
        instance = this;
        AudioSource sound = GetComponent<AudioSource>();
        startButton.onClick.AddListener(delegate
        {
            menu.SetActive(false);
            isStarted = true;
            sound.Play();
        });
    }

    private void Update()
    {
        if (shieldLevel < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            menu.SetActive(true);
        }
    }
}
