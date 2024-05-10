using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalewithMicrophone : MonoBehaviour
{

    public Vector3 minScale, maxScale;
    public AudioLoudnessDetector detector;
    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;

        private void Update() {
        float loudness = detector.GetLoundessFromMicrophone()*loudnessSensibility;
        if(loudness < threshold)
        {
            loudness = 0;
        }

        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
