using UnityEngine;
using UnityEngine.UI;

public class MicrophoneListener : MonoBehaviour
{
    public float threshold = 0.1f; // Threshold for loudness trigger
    public int sampleWindow = 128; // Number of samples to read each time
    public Slider volumeSlider; // Reference to the UI Slider

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component needed on this game object!");
            return;
        }

        audioSource.clip = Microphone.Start(null, true, 1, 44100); // Start microphone
        audioSource.loop = true;
        // Wait until the microphone starts, then play the audio source
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    void Update()
    {
        float level = GetAverageVolume() * 100;
        if (level > threshold)
        {
            TriggerEvent();
        }

        // Update the slider based on the current volume level
        volumeSlider.value = level;
    }

    float GetAverageVolume()
    {
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // get position of mic
        if (micPosition < 0) return 0;

        audioSource.clip.GetData(waveData, micPosition);

        float level = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            level += Mathf.Abs(waveData[i]);
        }
        return level / sampleWindow;
    }

    void TriggerEvent()
    {
        // Insert the code for your event here
        Debug.Log("Loud noise detected!");
    }
}
