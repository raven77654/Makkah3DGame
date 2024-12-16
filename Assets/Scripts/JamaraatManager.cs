using UnityEngine;
using UnityEngine.SceneManagement;

public class JamaraatManager : MonoBehaviour
{
    public GameObject jamaraatInfoPanel;   // Panel for Jamaraat information
    public GameObject firstStoningPanel;  // Panel for First Stoning instructions
    public GameObject secondStoningPanel; // Panel for Second Stoning instructions
    public GameObject thirdStoningPanel;  // Panel for Third Stoning instructions
    public GameObject finalPanel;         // Final panel for pause or next scene
    public Transform[] beacons;           // Array of beacon positions (targets)
    public GameObject indicator;          // Indicator that points to the beacon
    public Transform player;              // Reference to the player GameObject
    public Transform playerHead;          // Reference to the player's head transform
    [Range(0f, 60f)] public float indicatorHeightOffset = 5f; // Height offset for the indicator above the player

    private int currentBeaconIndex = 0;     // Track which beacon the player is heading to

    void Start()
    {
        // Start with the first target active
        ActivateBeacon(0);
        ShowJamaraatInfoPanel(); // Show the Jamaraat info panel at the start
        indicator.SetActive(false); // Hide the indicator until the panel is closed
    }

    void ShowJamaraatInfoPanel()
    {
        jamaraatInfoPanel.SetActive(true);
    }

    public void OnJamaraatInfoPanelClosed()
    {
        jamaraatInfoPanel.SetActive(false);   // Close the panel
        indicator.SetActive(true);            // Show the indicator
        MoveIndicatorToBeacon();              // Move the indicator to the first beacon
    }

    void ActivateBeacon(int index)
    {
        // Deactivate all beacons
        for (int i = 0; i < beacons.Length; i++)
        {
            beacons[i].gameObject.SetActive(i == index); // Only activate the current beacon
        }
    }

    void MoveIndicatorToBeacon()
    {
        if (playerHead == null || beacons.Length == 0)
        {
            Debug.LogWarning("Player head transform or beacons array is not assigned.");
            return;
        }

        // Calculate the new position for the indicator above the player's head
        Vector3 abovePlayer = playerHead.position + Vector3.up * indicatorHeightOffset;
        indicator.transform.position = abovePlayer;

        // Ensure the indicator points towards the current beacon
        Vector3 directionToBeacon = (beacons[currentBeaconIndex].position - abovePlayer).normalized;
        if (directionToBeacon != Vector3.zero)
        {
            indicator.transform.rotation = Quaternion.LookRotation(directionToBeacon);
        }
    }

    public void OnReachBeacon()
    {
        Debug.Log($"Reached beacon {currentBeaconIndex}");
        indicator.SetActive(false);  // Hide the indicator temporarily

        if (currentBeaconIndex == 0)  // First target
        {
            ShowFirstStoningPanel();
        }
        else if (currentBeaconIndex == 1)  // Second target
        {
            ShowSecondStoningPanel();
        }
        else if (currentBeaconIndex == 2)  // Third target
        {
            ShowThirdStoningPanel();
        }
    }

    void ShowFirstStoningPanel()
    {
        firstStoningPanel.SetActive(true);  // Show First Stoning panel
    }

    public void OnFirstStoningPanelClosed()
    {
        firstStoningPanel.SetActive(false);  // Hide First Stoning panel
        MoveToNextBeacon();
    }

    void ShowSecondStoningPanel()
    {
        secondStoningPanel.SetActive(true);
    }

    public void OnSecondStoningPanelClosed()
    {
        secondStoningPanel.SetActive(false);  // Hide Second Stoning panel
        MoveToNextBeacon();
    }

    void ShowThirdStoningPanel()
    {
        thirdStoningPanel.SetActive(true);
    }

    public void OnThirdStoningPanelClosed()
    {
        thirdStoningPanel.SetActive(false);  // Hide Third Stoning panel
        ShowFinalPanel();  // Display final panel
    }

    void MoveToNextBeacon()
    {
        currentBeaconIndex++;
        if (currentBeaconIndex < beacons.Length) // Ensure within bounds
        {
            ActivateBeacon(currentBeaconIndex);
            MoveIndicatorToBeacon(); // Update the indicator for the next target
            indicator.SetActive(true); // Show the indicator for the next target
        }
    }

    void ShowFinalPanel()
    {
        finalPanel.SetActive(true);  // Show final panel
        indicator.SetActive(false);  // Hide the indicator
    }

    public void OnFinalPanelHomeButton()
    {
        SceneManager.LoadScene("Select_Location");  // Load the level menu scene
    }

    public void OnFinalPanelRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    public void OnFinalPanelCancelButton()
    {
        finalPanel.SetActive(false);  // Hide final panel
    }

    public void OnFinalPanelCloseButton()
    {
        Debug.Log("Closing the application.");
        Application.Quit();  // This will exit the game in a build
    }
}