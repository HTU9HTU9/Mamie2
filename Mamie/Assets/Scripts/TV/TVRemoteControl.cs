using UnityEngine;
using UnityEngine.Video;
using TMPro; // Include the TextMeshPro namespace

public class TVRemoteControl : MonoBehaviour
{
    public VideoPlayer tvVideoPlayer;  // Assign this in the inspector
    public float interactionDistance = 3f;  // Maximum distance to interact with the remote
    public TextMeshProUGUI timeText; // Assign your TextMeshPro UI component here

    private float timeSinceTVOn = 0f;       // Timer to keep track of the time since the TV was turned on
    private bool isTVOn = false;            // State to check if the TV is currently on

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Check if the left mouse button was pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Check if the object hit is the TV remote
                if (hit.collider.gameObject.CompareTag("remote"))
                {
                    ToggleVideoPlayback();
                }
            }
        }

        // Update the timer if the TV is on
        if (isTVOn)
        {
            timeSinceTVOn += Time.deltaTime;
        }

        // Always update the TextMeshPro text to display the timer, regardless of TV state
        timeText.text = $"You survived {timeSinceTVOn:F2} seconds";
    }

    void ToggleVideoPlayback()
    {
        // Toggle the enabled state of the VideoPlayer
        tvVideoPlayer.enabled = !tvVideoPlayer.enabled;
        isTVOn = tvVideoPlayer.enabled; // Update isTVOn based on the VideoPlayer's state

        // If the TV is turned off, stop the video player but do not reset the timer
        if (!isTVOn)
        {
            tvVideoPlayer.Pause(); // Ensure the video is paused when the TV is off
        }
        else
        {
            tvVideoPlayer.Play(); // Start playing the video if the TV is turned on
        }
    }
}
