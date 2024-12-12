using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PanelManager : MonoBehaviour
{
    public GameObject mainPanel;     // Assign the MainPanel in Inspector
    public GameObject finalPanel;    // Assign the FinalPanel in Inspector
    public VideoPlayer videoPlayer;  // Assign the VideoPlayer in Inspector
    public AudioSource audioSource;  // Assign the AudioSource in Inspector
    public AudioClip firstAudio;     // First audio clip
    public AudioClip secondAudio;    // Second audio clip

    private bool crossButtonClicked = false;  // Flag to track if the cross button was clicked
    private bool isFirstAudioFinished = false; // Flag to track if the first audio has finished

    void Start()
    {
        // Ensure the final panel is hidden at the start
        finalPanel.SetActive(false);

        // Register for the video end event
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        // Start playing the video with the first audio
        PlayVideoWithAudio(firstAudio);
    }

    // Method to handle cross button click
    public void OnCrossButtonClick()
    {
        crossButtonClicked = true;  // Set flag when cross button is clicked

        // Stop both the video and audio
        videoPlayer.Stop();
        audioSource.Stop();

        // Show the final panel with the Home and Restart buttons
        mainPanel.SetActive(false); // Hide the main panel
        finalPanel.SetActive(true); // Show the final panel
    }

    // This method will be called when the video finishes
    private void OnVideoEnd(VideoPlayer vp)
    {
        if (!crossButtonClicked)
        {
            if (!isFirstAudioFinished)
            {
                // The first audio finished, now switch to the second audio and replay the video
                isFirstAudioFinished = true;
                PlayVideoWithAudio(secondAudio);
            }
            else
            {
                // After second audio ends, show the final panel
                mainPanel.SetActive(false); // Hide the main panel
                finalPanel.SetActive(true); // Show the final panel
            }
        }
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

    // Method for Cancel button to hide the final panel
    public void OnFinalPanelCancelButton()
    {
        finalPanel.SetActive(false);  // Hide final panel
    }

    // Method for Close button in final panel to exit the game
    public void OnFinalPanelCloseButton()
    {
        Debug.Log("Closing the application.");
        Application.Quit();  // This will exit the game in a build
    }

    // Play the video with the provided audio clip
    private void PlayVideoWithAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        videoPlayer.Play();
    }

    void OnDestroy()
    {
        // Unregister from the video end event when this object is destroyed
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
