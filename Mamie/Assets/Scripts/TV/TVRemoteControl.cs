using UnityEngine;
using UnityEngine.Video;

public class TVRemoteControl : MonoBehaviour
{
    public VideoPlayer tvVideoPlayer;  // Assign this in the inspector
    public float interactionDistance = 3f;  // Maximum distance to interact with the remote

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
    }

    void ToggleVideoPlayback()
{
    // Check if the VideoPlayer component is enabled
    if (tvVideoPlayer.enabled)
    {
        tvVideoPlayer.enabled = false;  // Disable the VideoPlayer to turn off video
    }
    else
    {
        tvVideoPlayer.enabled = true;   // Enable the VideoPlayer to turn on video
        tvVideoPlayer.Play();           // Optionally start playing immediately
    }
}

}
