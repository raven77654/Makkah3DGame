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

    [Space(10)]
    public GameObject minaPanelU;
    public GameObject namazTimingPanelU;
    public GameObject wazuPanelU;
    public GameObject namazPanelU;
    public GameObject finalPanelU; // The final panel to be displayed after Namaz

    [Space(10)]
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
        StartSequence();
    }

    void StartSequence()
    {
        Debug.Log("Starting sequence...");

        // Step 1: Show Mina Panel and wait for it to close
        minaPanelE.SetActive(true);
        minaPanelU.SetActive(true);
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

    public void OnMinaClosed()
    {
        minaClosed = true;
        minaPanelE.SetActive(false);
        minaPanelU.SetActive(false);

        namazTimingPanelE.SetActive(true);
        namazTimingPanelU.SetActive(true);
    }

    public void OnNamazTimingClosed()
    {
        namazTimingClosed = true;
        namazTimingPanelE.SetActive(false);
        namazTimingPanelU.SetActive(false);

        // Step 3: Activate indicator and start azan sound
        indicator.SetActive(true);
        azanAudioSource.Play();
        MoveIndicatorToBeacon();
        Debug.Log("Indicator and azan activated.");

        // Start coroutine to stop azan audio after 22 seconds
        StartCoroutine(StopAzanAfterDelay(22f));
    }

    public void OnWazuClosed()
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

    public void OnNamazClosed()
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
        StartSequence();
    }

    // Method for Restart button to reload the current scene
    public void OnFinalPanelRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }
}
