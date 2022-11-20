using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;

    public void Awake()
    {
        if (instance = null)
            instance = this;
        else
            Destroy(this);
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
