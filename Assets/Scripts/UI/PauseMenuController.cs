using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static event Action E_PauseGame;
    public GameObject openMenu;
    public GameObject pauseMenu;
    public GameObject helpMenu;
    public GameObject victoryMenu;
    private int nextLevelID = 0;

	private void OnEnable()
	{
        PlayerController.E_ReachedEnd += OpenVictoryMenu;
	}

	private void OnDisable()
	{
		PlayerController.E_ReachedEnd -= OpenVictoryMenu;
	}

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if (!GameManager.isPaused)
            {
				OpenPauseMenu();
			}
            else
            {
                ResumeGame();
            }
        }*/
    }

    public void OpenPauseMenu()
    {
        E_PauseGame?.Invoke();
        DisableAllMenus();
        pauseMenu.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
		DisableAllMenus();
		pauseMenu.SetActive(true);
	}

    public void ResumeGame()
    {
		E_PauseGame?.Invoke();
        DisableAllMenus();
		openMenu.SetActive(true);
    }

    public void OpenHelpMenu()
    {
        DisableAllMenus();
        helpMenu.SetActive(true);
    }

    public void OpenVictoryMenu(int currentLevelID)
    {
        DisableAllMenus();
        victoryMenu.SetActive(true);
        UpdateNextLevelID(currentLevelID);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuStart");
    }

    public void UpdateNextLevelID(int currentLevelID)
    {
        nextLevelID = currentLevelID + 1;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelID);
    }

    public void ReplayLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentLevelID = currentScene.buildIndex;
        SceneManager.LoadScene(currentLevelID);
    }

    public void DisableAllMenus()
    {
        openMenu.SetActive(false);
        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        victoryMenu.SetActive(false);
    }
}
