using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void ResumeGame()
    {
        
    }

    public void HelpMenu()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Nando");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
