using UnityEngine;

public class NarrationManager : MonoBehaviour
{
    public AudioSource thutmoseAudio;
    private string currentLabel = "";

    public void SetDetectedLabel(string label)
    {
        currentLabel = label;
        Debug.Log("🎯 SetDetectedLabel called");
        Debug.Log("🧠 Detected label stored: " + currentLabel);
    }

    public void PlayCurrentNarration()
    {
        Debug.Log("🎧 PlayCurrentNarration() called");
        Debug.Log("🔍 Current label is: " + currentLabel);

        if (currentLabel == "Khedive_Ismail")
        {
            Debug.Log("✅ Thutmose detected — checking if audio is playing...");
            if (!thutmoseAudio.isPlaying)
            {
                thutmoseAudio.Play();
                Debug.Log("🔊 Thutmose narration started.");
            }
            else
            {
                Debug.Log("⏸️ Thutmose audio is already playing.");
            }
        }
        else
        {
            Debug.Log("⚠️ No valid exhibit detected. No audio will play.");
        }
    }
}
