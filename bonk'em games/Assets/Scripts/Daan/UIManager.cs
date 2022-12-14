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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void WinScreen()
    {
        winScreen.active = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.1f;
    }

    public void DeathScreen()
    {
        deathScreen.active = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        SceneManager.LoadScene("Putting together a full scene");
    }
}
