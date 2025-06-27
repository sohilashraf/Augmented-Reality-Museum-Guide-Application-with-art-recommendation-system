using UnityEngine;

public class NarrationPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayNarration()
    {
        Debug.Log("🎤 PlayNarration() called");
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}
