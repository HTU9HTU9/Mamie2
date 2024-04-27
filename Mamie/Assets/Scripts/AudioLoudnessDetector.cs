using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{
    public int sampleWindow = 64; // moyenne de 64 échantillons de son

    private AudioClip _microphoneClip;
    private string _microphoneName;

    private void Start()
    {
        MicrophoneToAudioClip(0);
    }

    private void OnEnable() {
        MicrophoneSelecter.OnMicrophoneChoiceChanged += ChangeMicrophoneSource;
    }

    private void ChangeMicrophoneSource(int deviceIndex)
    {
        MicrophoneToAudioClip(deviceIndex);
    }
    private void OnDisable() {
        MicrophoneSelecter.OnMicrophoneChoiceChanged -= ChangeMicrophoneSource;
    }
    private void MicrophoneToAudioClip(int microphoneIndex)
    {
        _microphoneName = Microphone.devices[microphoneIndex];
        _microphoneClip = Microphone.Start(_microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoundessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(_microphoneName), _microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if(startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        foreach(var sample in waveData)
        {
            totalLoudness += Mathf.Abs(sample);
        }

        return totalLoudness / sampleWindow;
    }


}
