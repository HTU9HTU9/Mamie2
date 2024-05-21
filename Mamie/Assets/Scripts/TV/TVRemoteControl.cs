using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class TVRemoteControl : MonoBehaviour
{
    public VideoPlayer tvVideoPlayer;
    public float interactionDistance = 3f;
    public TextMeshProUGUI timeText;

    private float timeSinceTVOn = 0f;
    public bool isTVOn = false; // Change this to public

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.gameObject.CompareTag("remote"))
                {
                    ToggleVideoPlayback();
                }
            }
        }

        if (isTVOn)
        {
            timeSinceTVOn += Time.deltaTime;
        }

        timeText.text = $"Time Since TV On: {timeSinceTVOn:F2} seconds";
    }

    void ToggleVideoPlayback()
    {
        tvVideoPlayer.enabled = !tvVideoPlayer.enabled;
        isTVOn = tvVideoPlayer.enabled;

        if (isTVOn)
        {
            tvVideoPlayer.Play();
        }
        else
        {
            tvVideoPlayer.Pause();
        }
    }
}
