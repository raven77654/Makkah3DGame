using UnityEngine;
using UnityEngine.Video;

public class VideoWithAudioSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip englishAudio;  // English audio clip
    public AudioClip urduAudio;     // Urdu audio clip

    private bool isPlayingEnglish = true;

    private void Start()
    {
        if (videoPlayer != null && audioSource != null && englishAudio != null && urduAudio != null)
        {
            // Set initial audio to English
            audioSource.clip = englishAudio;
            audioSource.Play();

            // Start the video
            videoPlayer.Play();

            // Register for the video end event
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer, AudioSource, or AudioClips are not assigned!");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (isPlayingEnglish)
        {
            // Switch to Urdu audio and replay video
            isPlayingEnglish = false;
            audioSource.clip = urduAudio;
            audioSource.Play();
            videoPlayer.Play();
        }
        else
        {
            // Optional: Stop the video after Urdu audio ends
            videoPlayer.Stop();
            Debug.Log("Video playback completed in both languages.");
        }
    }
}
