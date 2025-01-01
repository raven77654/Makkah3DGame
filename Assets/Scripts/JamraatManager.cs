using UnityEngine;
using UnityEngine.SceneManagement;

public class JamaraatManager : MonoBehaviour
{
    [SerializeField] TargetGuideController targetCont;


    public GameObject jamaraatInfoPanelE;   // Panel for Jamaraat information
    public GameObject firstStoningPanelE;  // Panel for First Stoning instructions
    public GameObject secondStoningPanelE; // Panel for Second Stoning instructions
    public GameObject thirdStoningPanelE;  // Panel for Third Stoning instructions
    public GameObject finalPanelE;         // Final panel for pause or next scene


    public GameObject jamaraatInfoPanelU;   // Panel for Jamaraat information
    public GameObject firstStoningPanelU;  // Panel for First Stoning instructions
    public GameObject secondStoningPanelU; // Panel for Second Stoning instructions
    public GameObject thirdStoningPanelU;  // Panel for Third Stoning instructions
    public GameObject finalPanelU;         // Final panel for pause or next scene



    public Transform[] beacons;           // Array of beacon positions (targets)
    public GameObject indicator;          // Indicator that points to the beacon
    public Transform player;              // Reference to the player GameObject
    public Transform playerHead;          // Reference to the player's head transform
    [Range(0f, 60f)] public float indicatorHeightOffset = 5f; // Height offset for the indicator above the player

    private int currentBeaconIndex = 0;     // Track which beacon the player is heading to
    private bool reachedBeacon = false;     // Check if the beacon is reached
   

    void Start()
    {
        currentBeaconIndex = 0;
        ShowJamaraatInfoPanel();    // Show the Jamaraat info panel at the start
        indicator.SetActive(false); // Hide the indicator until the panel is closed
    }

    void ShowJamaraatInfoPanel()
    {
        jamaraatInfoPanelE.SetActive(true);
        jamaraatInfoPanelU.SetActive(true);

    }

    public void OnJamaraatInfoPanelClosed()
    {
        jamaraatInfoPanelE.SetActive(false);
        jamaraatInfoPanelU.SetActive(false);

        MoveIndicatorToBeacon();              // Move the indicator to the first beacon
        indicator.SetActive(true);            // Show the indicator
        targetCont.ActivateTarget(currentBeaconIndex);
    }

    /*
    
     void ActivateBeacon(int index)
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

        if (!reachedBeacon && Vector3.Distance(player.position, beacons[currentBeaconIndex].position) < 1.5f)
        {
            reachedBeacon = true;
            OnReachBeacon();  // Player reached the beacon
        }
    }



    public void OnReachBeacon()
    {
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
        
            firstStoningPanelE.SetActive(true);
            firstStoningPanelU.SetActive(true);
    }

    public void OnFirstStoningPanelClosed()
    {
        firstStoningPanelE.SetActive(false);  // Hide First Stoning panel
        firstStoningPanelU.SetActive(false);  // Hide First Stoning panel
        currentBeaconIndex++; // Move to the next beacon for Namaz
        reachedBeacon = false; // Reset for the next beacon

        MoveIndicatorToBeacon(); // Immediately move indicator to Namaz beacon after Wazu
        indicator.SetActive(true); // Show the indicator again for Namaz
        targetCont.ActivateTarget(currentBeaconIndex);

    }

    void ShowSecondStoningPanel()
    {
        
            secondStoningPanelE.SetActive(true);
            secondStoningPanelU.SetActive(true);
        
    }

    public void OnSecondStoningPanelClosed()
    {
        secondStoningPanelE.SetActive(false);  // Hide Second Stoning panel
        secondStoningPanelU.SetActive(false);  // Hide Second Stoning panel
        currentBeaconIndex++; // Move to the next beacon for Namaz
        reachedBeacon = false; // Reset for the next beacon

        MoveIndicatorToBeacon(); // Immediately move indicator to Namaz beacon after Wazu
        indicator.SetActive(true); // Show the indicator again for Namaz
        targetCont.ActivateTarget(currentBeaconIndex);
    }

    void ShowThirdStoningPanel()
    {
        thirdStoningPanelE.SetActive(true);
        thirdStoningPanelU.SetActive(true);
        
    }

    public void OnThirdStoningPanelClosed()
    {
        thirdStoningPanelE.SetActive(false);  // Hide Third Stoning panel
        thirdStoningPanelU.SetActive(false);  // Hide Third Stoning panel
        ShowFinalPanel();  // Display final panel
    }

   /* void MoveToNextBeacon()
    {
        currentBeaconIndex++;
        if (currentBeaconIndex < beacons.Length) // Ensure within bounds
        {
            ActivateBeacon(currentBeaconIndex);
            MoveIndicatorToBeacon(); // Update the indicator for the next target
            indicator.SetActive(true); // Show the indicator for the next target
        }
    }*/

    void ShowFinalPanel()
    {
       
         finalPanelE.SetActive(true);
         finalPanelU.SetActive(true);
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

   

    public void OnFinalPanelCloseButton()
    {
        Debug.Log("Closing the application.");
        Application.Quit();  // This will exit the game in a build
    }
}