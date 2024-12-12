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
    private bool reachedBeacon = false;     // Check if the beacon is reached

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

    void Update()
    {
        MoveIndicatorToBeacon();  // Always update the indicator position above the player's head

        // Check if the player has reached the current beacon
        float distanceToBeacon = Vector3.Distance(player.position, beacons[currentBeaconIndex].position);
        Debug.Log($"Distance to target {currentBeaconIndex}: {distanceToBeacon}");

        if (!reachedBeacon && distanceToBeacon < 1.5f)
        {
            reachedBeacon = true;
            OnReachBeacon();  // Player reached the beacon
        }
    }

    void OnReachBeacon()
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

        // Move to the next beacon
        currentBeaconIndex++;
        if (currentBeaconIndex < beacons.Length) // Ensure within bounds
        {
            ActivateBeacon(currentBeaconIndex);
            reachedBeacon = false; // Reset the beacon state
            MoveIndicatorToBeacon(); // Update the indicator for the next target
            indicator.SetActive(true); // Show the indicator for the next target
        }
    }

    void ShowSecondStoningPanel()
    {
        secondStoningPanel.SetActive(true);
    }

    public void OnSecondStoningPanelClosed()
    {
        secondStoningPanel.SetActive(false);  // Hide Second Stoning panel

        // Move to the next beacon
        currentBeaconIndex++;
        if (currentBeaconIndex < beacons.Length) // Ensure within bounds
        {
            ActivateBeacon(currentBeaconIndex);
            reachedBeacon = false; // Reset the beacon state
            MoveIndicatorToBeacon(); // Update the indicator for the next target
            indicator.SetActive(true); // Show the indicator for the next target
        }
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

    void ShowFinalPanel()
    {
        finalPanel.SetActive(true);  // Show final panel
        indicator.SetActive(false);  // Hide the indicator
    }

    // Method for Home button to go to level menu
    public void OnFinalPanelHomeButton()
    {
        SceneManager.LoadScene("Select_Location");  // Load the level menu scene
    }

    // Method for Restart button to reload the current scene
    public void OnFinalPanelRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    // Method for Cancel button to hide the final panel
    public void OnFinalPanelCancelButton()
    {
        finalPanel.SetActive(false);  // Hide final panel
    }

    // Method for Close button in final panel to exit the game
    public void OnFinalPanelCloseButton()
    {
        Debug.Log("Closing the application.");
        Application.Quit();  // This will exit the game in a build
    }
}
