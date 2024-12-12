using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public GameObject indicator; // The visual indicator (e.g., arrow above the player's head)
    private GameObject currentTarget;

    public void SetTarget(GameObject target)
    {
        currentTarget = target;

        if (indicator != null)
        {
            if (currentTarget != null)
            {
                indicator.SetActive(true);
                Debug.Log("Indicator is now pointing to target: " + currentTarget.name);
            }
            else
            {
                indicator.SetActive(false);
                Debug.Log("Indicator disabled. No target assigned.");
            }
        }
        else
        {
            Debug.LogError("Indicator object is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        if (indicator != null && currentTarget != null)
        {
            // Update the indicator's position to always be above the player or point to the target
            Vector3 direction = currentTarget.transform.position - indicator.transform.position;
            indicator.transform.rotation = Quaternion.LookRotation(direction);

            // Adjust indicator position (e.g., above player's head)
            indicator.transform.position = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y + 2, currentTarget.transform.position.z);
        }
    }
}
