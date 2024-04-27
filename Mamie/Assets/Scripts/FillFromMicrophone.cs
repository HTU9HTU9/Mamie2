using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillFromMicrophone : MonoBehaviour
{

    public Image audioBar;
    public Slider sensitivitySlider;
    public AudioLoudnessDetector detector;
    public float minimumSensibility=100;
    public float maximumSensibility=1000;
    public float CurrentLoudnessSensibility = 500;
    public float threshold = 0.1f;
    public GameObject screamtext;

    //Pour l'implementation de la mecanique de cri dans le jeu
    //public static UnityAction _OnScreamDetected;

    private void Start() {
        if(sensitivitySlider == null)
        {
            return ;
        }

        sensitivitySlider.value=0.5f;
        SetLoudnessSensibility(sensitivitySlider.value);
    }

    private void Update() {
        float loudness = detector.GetLoundessFromMicrophone()*CurrentLoudnessSensibility;
        if(loudness < threshold)
        {
            loudness = 0.01f;
        }

        audioBar.fillAmount = loudness;

        if(loudness > 0.5f && !screamtext.activeInHierarchy)
        {
            screamtext.SetActive(true);
        }
        else if(loudness < 0.5f && screamtext.activeInHierarchy)
        {
            screamtext.SetActive(false);
        }
        //Invoquer un event si un cri est détecté
        //if (loudness > 0.5f) OnScreamDetected?.Invoke();
        

    }
    public void SetLoudnessSensibility(float t)
    {
        CurrentLoudnessSensibility = Mathf.Lerp(minimumSensibility, maximumSensibility, t);
    }
}
