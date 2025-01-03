using UnityEngine;
using UnityEngine.UI;

public class AroundAround : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float radius = 5f; // Radius of the circle
    public int circleCount = 0; // Counter for completed circles
    public int circleCountMax = 7; // Total circles to complete

    private bool isPlayerInside = false; // Tracks if the player is inside the circle
    private Vector3 entryPoint; // Stores the entry point of the player
    private bool hasCrossedLine = false; // Tracks if the player has crossed the reference line

    // Reference to TawafManager
    public TawafManager tawafManager;

    [Header("UI Settings")]
    public Text roundText; // Text component to display the round count
    public float textDisplayDuration = 2f; // Time to display the text

    private float textTimer = 0f;

    void Update()
    {
        // Calculate distance between player and the center of the circle
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is within the circle radius
        if (distance <= radius)
        {
            if (!isPlayerInside)
            {
                isPlayerInside = true;
                OnPlayerEnterCircle();
            }

            Vector3 toEntryPoint = entryPoint - transform.position;
            Vector3 toPlayer = player.position - transform.position;

            // Calculate cross product to detect a crossing
            float crossProduct = Vector3.Cross(toEntryPoint.normalized, toPlayer.normalized).y;

            if (crossProduct > 0 && !hasCrossedLine)
            {
                hasCrossedLine = true;
                circleCount++; // Increment the round counter

                Debug.Log("Circle completed! Total count: " + circleCount);

                // Display the current round count
                DisplayRoundText(circleCount);

                // Check if rounds are completed
                if (circleCount >= circleCountMax)
                {
                    AllRoundsCompleted();
                }
            }
            else if (crossProduct <= 0)
            {
                hasCrossedLine = false; // Reset crossing flag when moving back
            }
        }
        else
        {
            // Player left the circle
            if (isPlayerInside)
            {
                isPlayerInside = false;
                hasCrossedLine = false;
                OnPlayerExitCircle();
            }
        }

        // Handle text hiding after the duration
        if (roundText != null && textTimer > 0)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0)
            {
                roundText.gameObject.SetActive(false);
            }
        }
    }

    private void AllRoundsCompleted()
    {
        Debug.Log("All 7 rounds completed!");

        // Notify TawafManager to show the final panel
        if (tawafManager != null)
        {
            tawafManager.ShowFinalPanel();
        }
        else
        {
            Debug.LogError("TawafManager is not assigned.");
        }
    }

    private void OnPlayerEnterCircle()
    {
        entryPoint = player.position; // Record the entry point
        Debug.Log("Player entered circle at: " + entryPoint);
    }

    private void OnPlayerExitCircle()
    {
        Debug.Log("Player exited the circle.");
    }

    public void StartRounds()
    {
        circleCount = 0; // Reset the round counter
        Debug.Log("Round counting started.");
    }

    private void DisplayRoundText(int round)
    {
        if (roundText != null)
        {
            roundText.text = "Round: " + round; // Update the text
            roundText.gameObject.SetActive(true); // Show the text
            textTimer = textDisplayDuration; // Reset the timer
        }
        else
        {
            Debug.LogError("RoundText UI element is not assigned.");
        }
    }

    private void OnDrawGizmos()
    {
        if (transform != null)
        {
            // Draw the circle in the editor
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            if (isPlayerInside)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, entryPoint);
            }
        }
    }
}