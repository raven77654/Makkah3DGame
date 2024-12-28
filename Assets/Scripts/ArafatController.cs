using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ArafatController : MonoBehaviour
{
    public GameObject arafatInfoPanelEng; // Panel for Arafat information
    public GameObject choicePanelEng;     // Panel for choices (prayer, dua, recite Quran)
    public GameObject finalPanelEng;      // Final panel with home, close, and restart buttons

    public GameObject arafatInfoPanelUrdu; // Panel for Arafat information
    public GameObject choicePanelUrdu;     // Panel for choices (prayer, dua, recite Quran)
    public GameObject finalPanelUrdu;      // Final panel with home, close, and restart buttons

    public GameObject indicator;       // Indicator that moves towards the beacon
    public GameObject beacon;          // Target beacon for the model to reach
    public GameObject model;           // The model that will move towards the beacon
    public Vector3 indicatorOffset = new Vector3(0, 2f, 0);  // Offset for indicator position above the model

    private bool isMovingToBeacon = false; // Flag to control model movement to the beacon

    void Start()
    {
        // Show only the Arafat info panel at the start
        ShowPanel(arafatInfoPanelEng);
        choicePanelEng.SetActive(false);
        finalPanelEng.SetActive(false);

        ShowPanel(arafatInfoPanelUrdu);
        choicePanelUrdu.SetActive(false);
        finalPanelUrdu.SetActive(false);

        indicator.SetActive(false);
        beacon.SetActive(false);
    }

    public void OnCloseArafatInfoPanel()
    {
        // Close Arafat info panel, show the indicator, and start moving the model towards the beacon
        arafatInfoPanelEng.SetActive(false);
        arafatInfoPanelUrdu.SetActive(false);
        indicator.SetActive(true);
        beacon.SetActive(true);
        isMovingToBeacon = true;
    }

    private void Update()
    {
        if (isMovingToBeacon)
        {
            // Position the indicator above the model's head
            indicator.transform.position = model.transform.position + indicatorOffset;

            // Make the indicator point towards the beacon
            Vector3 directionToBeacon = beacon.transform.position - indicator.transform.position;
            directionToBeacon.y = 0; // Keep the indicator pointing horizontally (ignore vertical rotation)
            if (directionToBeacon.sqrMagnitude > 0.01f) // Avoid jittering when very close
            {
                indicator.transform.rotation = Quaternion.LookRotation(directionToBeacon);
            }

            // Move the model towards the beacon position
            model.transform.position = Vector3.MoveTowards(model.transform.position, beacon.transform.position, Time.deltaTime * 2f);

            // Check if the model has reached the beacon
            if (Vector3.Distance(model.transform.position, beacon.transform.position) < 0.5f)
            {
                // Stop the model's movement and hide the indicator and beacon
                isMovingToBeacon = false;
                indicator.SetActive(false);
                beacon.SetActive(false);

                // Show the choice panel

                //Debug.Log("=========== ");
                ShowPanel(choicePanelEng);
                ShowPanel(choicePanelUrdu);
            }
        }
    }

    public void OnCloseChoicePanel()
    {
        // Close choice panel and show the final panel
        choicePanelEng.SetActive(false);
        choicePanelUrdu.SetActive(false);
        ShowPanel(finalPanelEng);
        ShowPanel(finalPanelUrdu);
    }

    private void ShowPanel(GameObject panel)
    {
        //Debug.Log(">>> " + panel.name);
        // Display the specified panel
        panel.SetActive(true);
    }

    private void HidePanel(GameObject panel)
    {
        // Hide the specified panel
        panel.SetActive(false);
    }

    public void OnFinalPanelHomeButton()
    {
        SceneManager.LoadScene("Select_Location");  // Load the level menu scene (replace with actual scene name)
    }

    public void OnFinalPanelRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }


}
