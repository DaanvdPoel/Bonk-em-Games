using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject deathScreen;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void WinScreen()
    {
        winScreen.active = true;
    }

    public void DeathScreen()
    {
        deathScreen.active = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
  public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
