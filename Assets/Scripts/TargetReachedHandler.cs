using UnityEngine;

public class TargetReachedHandler : MonoBehaviour  // Updated class name
{
    [Header("UI and Indicators")]
    public GameObject wazuMessageUIEng;  // Renamed variable
    public GameObject wazuMessageUIUrdu;  // Renamed variable


    public GameObject indicatorObject;  // Renamed variable
    public GameObject beaconObject;  // Renamed variable

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area
        if (other.CompareTag("Player"))
        {
            // Activate the Wazu message UI
            if (wazuMessageUIEng != null)
            {
                wazuMessageUIEng.SetActive(true);
                wazuMessageUIUrdu.SetActive(true);
                Debug.Log("Wazu message displayed.");
            }

            // Hide the indicator
            if (indicatorObject != null)
            {
                indicatorObject.SetActive(false);
                Debug.Log("Indicator hidden.");
            }

            // Hide the beacon
            if (beaconObject != null)
            {
                beaconObject.SetActive(false);
                Debug.Log("Beacon hidden.");
            }
        }
    }
}
