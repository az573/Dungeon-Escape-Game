using System.Collections;
using System.Collections.Generic;using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject winScreen;
    public GameObject gameOverScreen;
    public int playerHealth = 3;
    public int maxHealth = 5;

    public int keysCollected = 0;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddHealth(int amount)
    {
        playerHealth = Mathf.Min(playerHealth + amount, maxHealth);
        Debug.Log("Health: " + playerHealth);
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddKey()
    {
        keysCollected++;
        Debug.Log("Keys: " + keysCollected);
    }

    public void UseKey()
    {
        if (keysCollected > 0)
        {
            keysCollected--;
            Debug.Log("Key used. Remaining: " + keysCollected);
        }
    }


    private void GameOver()
    {
        Debug.Log("Game Over!");

            if (gameOverScreen != null)
    {
        gameOverScreen.SetActive(true);
    }

 
    Time.timeScale = 0f;

   
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    }
    
        public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f; 
    }

public void RestartGame()
{   
    playerHealth = maxHealth; 
    Time.timeScale = 1f;
    SceneManager.LoadScene(0); 
}

    public void RestartLevel()
    {
        playerHealth = maxHealth;
        Time.timeScale = 1f;

            if (gameOverScreen != null)
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }



    public void QuitGame()
    {
        Application.Quit();
        Time.timeScale = 1f;
    }
}
