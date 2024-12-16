using UnityEngine;

public class BeaconTrigger : MonoBehaviour
{
    public JamaraatManager jamaraatManager; // Reference to the manager script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered
        {
            jamaraatManager.OnReachBeacon(); // Notify the manager
        }
    }
}