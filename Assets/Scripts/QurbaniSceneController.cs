using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class QurbaniSceneController : MonoBehaviour
{


    public GameObject qurbaniInfoPanelE; // Panel for Arafat information
    public GameObject qurbaniDistributionPanelE;
    public GameObject finalPanelE;


    public GameObject qurbaniInfoPanelU;
    public GameObject qurbaniDistributionPanelU;
    public GameObject finalPanelU;
    public Transform playerHead;            // Reference to the player's head transform

    public GameObject indicator;       // Indicator that moves towards the beacon
    public GameObject beacon;          // Target beacon for the model to reach
    public GameObject model;           // The model that will move towards the beacon
    public Vector3 indicatorOffset = new Vector3(0, 2f, 0);  // Offset for indicator position above the model
    private int currentBeaconIndex = 0;     // Track which beacon the player is heading to

    private bool isMovingToBeacon = false; // Flag to control model movement to the beacon

    void Start()
    {
        currentBeaconIndex = 0;  // Start by targeting the first beacon (Wazu)
        ShowQurbaniInfoPanel();   // Show the Muzdalifah panel at the start
        indicator.SetActive(false); // Initially hide the indicator

        // ShowPanel(QurbaniInfoPanel);
      //  qurbaniDistributionPanelE.SetActive(false);
      //  finalPanelE.SetActive(false);

      //  ShowPanel(qurbaniInfoPanelU);
       // qurbaniDistributionPanelU.SetActive(false);
       // finalPanelU.SetActive(false);

        indicator.SetActive(false);
        beacon.SetActive(false);
    }

    void ShowQurbaniInfoPanel()
    {
        qurbaniInfoPanelE.SetActive(true);
        qurbaniInfoPanelU.SetActive(true);
    }


    public void OnCloseQurbaniInfoPanel()
    {
        // Close Qurbani info panel, show the indicator, and start moving the model towards the beacon
        qurbaniInfoPanelE.SetActive(false);
        qurbaniInfoPanelU.SetActive(false);
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
                ShowPanel(qurbaniDistributionPanelE);
                ShowPanel(qurbaniDistributionPanelU);
            }
        }
    }

    public void OnCloseChoicePanel()
    {
        // Close choice panel and show the final panel
        qurbaniDistributionPanelE.SetActive(false);
        qurbaniDistributionPanelU.SetActive(false);
        ShowPanel(finalPanelE);
        ShowPanel(finalPanelU);
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








   

