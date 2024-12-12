using UnityEngine;
using UnityEngine.SceneManagement;

public class TawafManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject kabainfoPanel;
    public GameObject finalPanel;

    [Header("Kaba Controller Script")]
    public AroundAround aroundAround; // Reference to the script controlling the rounds

    void Start()
    {
        // Initialize panels
        if (kabainfoPanel != null)
            kabainfoPanel.SetActive(true);
        else
            Debug.LogError("kabainfoPanel is not assigned in the Inspector.");

        if (finalPanel != null)
            finalPanel.SetActive(false);
        else
            Debug.LogError("finalPanel is not assigned in the Inspector.");
    }

    public void CloseInfoPanel()
    {
        if (kabainfoPanel != null)
            kabainfoPanel.SetActive(false);
        else
            Debug.LogError("kabainfoPanel is not assigned.");
    }

    // This function is called by the AroundAround script when the rounds are completed
    public void ShowFinalPanel()
    {
        if (finalPanel != null)
        {
            finalPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("finalPanel is not assigned.");
        }
    }

    // Restart the scene
    public void RestartScene()
    {
        Debug.Log("Restarting scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Go back to the home scene
    public void GoHome()
    {
        Debug.Log("Going to HomeScene.");
        SceneManager.LoadScene("HomeScene");
    }

    // Exit the game
    public void ExitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}