using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject openMenu;
    public GameObject pauseMenu;
    public GameObject helpMenu;

    public void OpenPauseMenu()
    {
        openMenu.SetActive(false);
        pauseMenu.SetActive(true);
        helpMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        openMenu.SetActive(true);
        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    public void OpenHelpMenu()
    {
        openMenu.SetActive(false);
        pauseMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuStart");
    }
}
