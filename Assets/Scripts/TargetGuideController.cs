using UnityEngine;

public class TargetGuideController : MonoBehaviour
{
    public GameObject[] targets; // Array of target objects
    private int currentTargetIndex = 0;

    [SerializeField] private GameObject activePanels; // Active language panels

    private void Start()
    {
        //activePanels = FindObjectOfType<LanguagePanelsController>()?.GetActivePanels();

        if (targets.Length > 0)
        {
            ActivateTarget(0); // Start with the first target
        }
    }

   /* public void OnTargetReached()
    {
        if (currentTargetIndex < targets.Length)
        {
            // Display the corresponding panel
            *//*if (activePanels != null)
            {
                Transform panel = activePanels.transform.GetChild(currentTargetIndex);
                if (panel != null) panel.gameObject.SetActive(true);
            }*//*

            currentTargetIndex++;

            // Activate the next target if it exists
            if (currentTargetIndex < targets.Length)
            {
                ActivateTarget(currentTargetIndex);
            }
        }
    }*/

    public void ActivateTarget(int index)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetActive(i == index); // Activate only the current target
        }
        Debug.Log("Activated target: " + targets[index].name);
    }
}
