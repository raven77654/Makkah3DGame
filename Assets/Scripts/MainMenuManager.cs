using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject aboutDialog;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ShowAboutDialog()
    {
        aboutDialog.SetActive(true);
    }

    public void HideAboutDialog()
    {
        aboutDialog.SetActive(false);
    }

    public void ExitGame()
    {
        // Logs a message for debugging in the editor
        Debug.Log("Exiting the game...");

        // Quits the application
        Application.Quit();
    }
}
