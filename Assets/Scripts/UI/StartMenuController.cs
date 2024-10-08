using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MenuLevel");
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("MenuCredits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
