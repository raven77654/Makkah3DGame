using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] TargetGuideController targetCont;


    public GameObject minaPanelE;
    public GameObject namazTimingPanelE;
    public GameObject wazuPanelE;
    public GameObject namazPanelE;
    public GameObject finalPanelE; // The final panel to be displayed after Namaz

    public GameObject minaPanelU;
    public GameObject namazTimingPanelU;
    public GameObject wazuPanelU;
    public GameObject namazPanelU;
    public GameObject finalPanelU; // The final panel to be displayed after Namaz


    public Button minaCloseButton;
    public Button namazTimingCloseButton;
    public Button wazuCloseButton;
    public Button namazCloseButton;
    public Button restartButton; // Restart button in the final panel
    public Button homeButton;    // Home button in the final panel
    public Button closeButton;   // Close button in the final panel

    public AudioSource azanAudioSource;
    public Transform[] beacons;
    public GameObject indicator;
    public Transform player;
    public Transform playerHead;            // Reference to the player's head transform

    public float indicatorHeightOffset = 2f;

    private int currentBeaconIndex = 0;
    private bool reachedBeacon = false;

    private bool minaClosed = false;
    private bool namazTimingClosed = false;

    void Start()
    {
        // Initialize sequence
        StartCoroutine(StartSequence());

        // Assign close button events
        minaCloseButton.onClick.AddListener(() => minaClosed = true);
        namazTimingCloseButton.onClick.AddListener(() => namazTimingClosed = true);
        wazuCloseButton.onClick.AddListener(OnWazuClosed);
        namazCloseButton.onClick.AddListener(OnNamazClosed);

       
    }

    IEnumerator StartSequence()
    {
        Debug.Log("Starting sequence...");

        // Step 1: Show Mina Panel and wait for it to close
        minaPanelE.SetActive(true);
        minaPanelU.SetActive(true);

        Debug.Log("Mina panel active, waiting to close...");
        yield return new WaitUntil(() => minaClosed);

        // Step 2: Show Namaz Timing Panel and wait for it to close
        namazTimingPanelE.SetActive(true);
        namazTimingPanelU.SetActive(true);

        Debug.Log("Namaz Timing panel active, waiting to close...");
        yield return new WaitUntil(() => namazTimingClosed);

        // Step 3: Activate indicator and start azan sound
        indicator.SetActive(true);
        azanAudioSource.Play();
        MoveIndicatorToBeacon();
        Debug.Log("Indicator and azan activated.");

        // Start coroutine to stop azan audio after 22 seconds
        StartCoroutine(StopAzanAfterDelay(22f));
    }

    IEnumerator StopAzanAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        azanAudioSource.Stop();
        Debug.Log("Azan audio stopped after " + delay + " seconds.");
    }

    void Update()
    {
        MoveIndicatorToBeacon();

        if (!reachedBeacon && Vector3.Distance(player.position, beacons[currentBeaconIndex].position) < 1.5f)
        {
            reachedBeacon = true;
            OnReachBeacon();
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

    void OnReachBeacon()
    {
        indicator.SetActive(false);

        if (currentBeaconIndex == 0)
        {
             wazuPanelE.SetActive(true);
            wazuPanelU.SetActive(true);

        }
        else if (currentBeaconIndex == 1)
        {
               namazPanelE.SetActive(true);
            namazPanelU.SetActive(true);

        }
    }

    void OnWazuClosed()
    {
        wazuPanelE.SetActive(false);
        wazuPanelU.SetActive(false);

        // Move to the next beacon and re-enable indicator
        currentBeaconIndex++; // Move to the next beacon for Namaz
        reachedBeacon = false; // Reset for the next beacon

        MoveIndicatorToBeacon(); // Immediately move indicator to Namaz beacon after Wazu
        indicator.SetActive(true); // Show the indicator again for Namaz
        targetCont.ActivateTarget(currentBeaconIndex);
    }

    void OnNamazClosed()
    {
        namazPanelE.SetActive(false);
        namazPanelU.SetActive(false);


        // Show the final panel after Namaz panel is closed
        Debug.Log("Sequence completed.");
        finalPanelE.SetActive(true);
        finalPanelU.SetActive(true);

    }

    // Final Panel button actions
    void RestartSequence()
    {
        Debug.Log("Restarting sequence...");
        // Reset sequence by hiding the final panel and restarting the flow
        finalPanelE.SetActive(false);
        finalPanelU.SetActive(false);

        minaClosed = false;
        namazTimingClosed = false;
        StartCoroutine(StartSequence());
    }

    void GoToHome()
    {
        Debug.Log("Returning to home...");
        // Logic for going to home (e.g., load a home scene or reset the game)
        // SceneManager.LoadScene("HomeScene");
    }

    public void OnFinalPanelHomeButton()
    {
        SceneManager.LoadScene("Select_Location");  // Load the level menu scene
    }

    // Method for Restart button to reload the current scene
    public void OnFinalPanelRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    void CloseScene()
    {
        Debug.Log("Closing scene...");
        // Logic for closing the scene or application
        Application.Quit();
    }
}
