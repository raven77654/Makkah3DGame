using UnityEngine;

public class AudioSequencePlayer : MonoBehaviour
{
    public AudioSource audioSource;    // Reference to the AudioSource
    public AudioClip firstClip;        // First audio clip to play
    public AudioClip secondClip;       // Second audio clip to play
    public GameObject finalPanel;      // Reference to the final panel

    private void Start()
    {
        // Ensure the final panel is hidden at the start
        if (finalPanel != null)
        {
            finalPanel.SetActive(false);
        }

        // Play the first clip at the start
        if (firstClip != null)
        {
            audioSource.clip = firstClip;
            audioSource.Play();
            // Schedule the second clip to play after the first ends
            Invoke(nameof(PlaySecondClip), firstClip.length);
        }
    }

    private void PlaySecondClip()
    {
        if (secondClip != null)
        {
            audioSource.clip = secondClip;
            audioSource.Play();
            // Schedule the final panel to show after the second clip ends
            Invoke(nameof(ShowFinalPanel), secondClip.length);
        }
    }

    private void ShowFinalPanel()
    {
        if (finalPanel != null)
        {
            finalPanel.SetActive(true);
        }
    }
}
