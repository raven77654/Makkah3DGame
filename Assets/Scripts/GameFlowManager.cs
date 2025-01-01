using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] TargetGuideController targetCont;


    public GameObject safaMarwaInfoPanelE;   // Panel for Safa Marwa information
    public GameObject marwaPanelE;          // Panel for Marwa instructions
    public GameObject safaPanelE;           // Panel for Safa instructions
    public GameObject finalPanelE;          // Final panel for pause or next scene

    public GameObject safaMarwaInfoPanelU;   // Panel for Safa Marwa information
    public GameObject marwaPanelU;          // Panel for Marwa instructions
    public GameObject safaPanelU;           // Panel for Safa instructions
    public GameObject finalPanelU;          // Final panel for pause or next scene


    public Transform[] beacons;            // Array of beacon positions (Marwa, Safa)
    public GameObject indicator;           // Indicator that points to the beacon
    public Transform player;               // Reference to the player GameObject
    public Transform playerHead;           // Reference to the player's head transform
    [Range(0f, 60f)] public float indicatorHeightOffset = 5f; // Height offset for the indicator above the player

    private int currentBeaconIndex = 0;     // Track which beacon the player is heading to
    private bool reachedBeacon = false;     // Check if the beacon is reached

    void Start()
    {
        // Start with the first target active
        currentBeaconIndex = 0;
        ShowSafaMarwaInfoPanel(); // Show the Safa Marwa info panel at the start
        indicator.SetActive(false); // Hide the indicator until the panel is closed
    }

    void ShowSafaMarwaInfoPanel()
    {
        safaMarwaInfoPanelE.SetActive(true);
        safaMarwaInfoPanelU.SetActive(true);

    }

    public void OnSafaMarwaInfoPanelClosed()
    {
        safaMarwaInfoPanelE.SetActive(false);   // Close the panel
        safaMarwaInfoPanelU.SetActive(false);   // Close the panel
        indicator.SetActive(true);            // Show the indicator
        MoveIndicatorToBeacon();              // Move the indicator to the first beacon (Marwa)

        targetCont.ActivateTarget(currentBeaconIndex);

    }

   /* void ActivateBeacon(int index)
    {
        // Deactivate all beacons
        for (int i = 0; i < beacons.Length; i++)
        {
            beacons[i].gameObject.SetActive(i == index); // Only activate the current beacon
        }
    }
   */
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

        // Ensure the camera can see the indicator (set the Z value appropriately)
        Vector3 directionToBeacon = (beacons[currentBeaconIndex].position - abovePlayer).normalized;
        if (directionToBeacon != Vector3.zero)
        {
            indicator.transform.rotation = Quaternion.LookRotation(directionToBeacon);
        }

        // Ensure indicator is in a visible layer if it's a 3D object
        if (indicator.GetComponent<Renderer>() != null)
        {
            indicator.GetComponent<Renderer>().enabled = true; // Ensure it is rendered
        }
    }

    void Update()
    {
        MoveIndicatorToBeacon();  // Always update the indicator position above the player's head

        // Check if the player has reached the current beacon
        if (!reachedBeacon && Vector3.Distance(player.position, beacons[currentBeaconIndex].position) < 1.5f)
        {
            reachedBeacon = true;
            OnReachBeacon();  // Player reached the beacon
        }
    }

    void OnReachBeacon()
    {
        indicator.SetActive(false);  // Hide the indicator temporarily

        if (currentBeaconIndex == 0)  // First beacon is Marwa
        {
            ShowMarwaPanel();  // Show the Marwa panel
        }
        else if (currentBeaconIndex == 1)  // Second beacon is Safa
        {
            ShowSafaPanel();  // Show the Safa panel
        }
    }

    void ShowMarwaPanel()
    {
        marwaPanelE.SetActive(true);  // Show Marwa panel
        marwaPanelU.SetActive(true);  // Show Marwa panel

    }

    public void OnMarwaPanelClosed()
    {
        marwaPanelE.SetActive(false);  // Hide Marwa panel
        marwaPanelU.SetActive(false);  // Hide Marwa panel

        currentBeaconIndex++; // Move to the next beacon for Namaz
        reachedBeacon = false; // Reset for the next beacon

        MoveIndicatorToBeacon(); // Immediately move indicator to Namaz beacon after Wazu
        indicator.SetActive(true); // Show the indicator again for Namaz
        targetCont.ActivateTarget(currentBeaconIndex);
    }

    void ShowSafaPanel()
    {
        safaPanelE.SetActive(true);
        safaPanelU.SetActive(true);

    }

    public void OnSafaPanelClosed()
    {
        safaPanelE.SetActive(false);  // Hide Safa panel
        safaPanelU.SetActive(false);  // Hide Safa panel

        ShowFinalPanel();  // Display final panel
    }

    void ShowFinalPanel()
    {
        finalPanelE.SetActive(true);  // Show final panel
        finalPanelU.SetActive(true);  // Show final panel

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
        finalPanelE.SetActive(false);  // Hide final panel
        finalPanelU.SetActive(false);  // Hide final panel

    }

    // Method for Close button in final panel to exit the game
    public void OnFinalPanelCloseButton()
    {
        Debug.Log("Closing the application.");
        Application.Quit();  // This will exit the game in a build
    }
}
