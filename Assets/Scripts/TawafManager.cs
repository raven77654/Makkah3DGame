using UnityEngine;
using UnityEngine.SceneManagement;

public class TawafManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject kabainfoPanelE;
    public GameObject finalPanelE;

    [Space(20)]
    public GameObject kabainfoPanelU;
    public GameObject finalPanelU;

     public Transform player;
    public Transform playerHead;            // Reference to the player's head transform

    public float indicatorHeightOffset = 2f;



    [Header("Kaba Controller Script")]
    public AroundAround aroundAround; // Reference to the script controlling the rounds

    void Start()
    {
        // Initialize panels
        if (kabainfoPanelE != null)
            kabainfoPanelE.SetActive(true);
        else
            Debug.LogError("kabainfoPanel is not assigned in the Inspector.");

        if (finalPanelE != null)
            finalPanelE.SetActive(false);
        else
            Debug.LogError("finalPanelEng is not assigned in the Inspector.");

        if (kabainfoPanelU != null)
            kabainfoPanelU.SetActive(true);
        else
            Debug.LogError("kabainfoPanel is not assigned in the Inspector.");

        if (finalPanelU != null)
            finalPanelU.SetActive(false);
        else
            Debug.LogError("finalPanelEng is not assigned in the Inspector.");


    }

    public void CloseInfoPanel()
    {
        if (kabainfoPanelE != null)
            kabainfoPanelE.SetActive(false);
        else
            Debug.LogError("kabainfoPanel is not assigned.");

        if (kabainfoPanelU != null)
            kabainfoPanelU.SetActive(false);
        else
            Debug.LogError("kabainfoPanel is not assigned.");

    }

    // This function is called by the AroundAround script when the rounds are completed
    public void ShowFinalPanel()
    {
        if (finalPanelE != null)
        {
            finalPanelE.SetActive(true);
        }
        else
        {
            Debug.LogError("finalPanelEng is not assigned.");
        }
        if (finalPanelU != null)
        {
            finalPanelU.SetActive(true);
        }
        else
        {
            Debug.LogError("finalPanelEng is not assigned.");
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