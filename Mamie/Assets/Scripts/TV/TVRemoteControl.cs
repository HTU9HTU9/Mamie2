using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement; // Add this to manage scene transitions

public class TVRemoteControl : MonoBehaviour
{
    public VideoPlayer tvVideoPlayer;  // Assign the TV's VideoPlayer component in the inspector
    public float interactionDistance = 3f;  // Maximum distance to interact with the remote
    public TextMeshProUGUI timeText; // Assign the TextMeshPro UI component for the timer display
    public GameObject tvScreen; // Reference to the TV screen GameObject
    public MonsterSpawner monsterSpawner; // Reference to the MonsterSpawner
    public float winTime = 20f; // Configurable win time in seconds

    private float timeSinceTVOn = 0f;
    public bool isTVOn = false; // Keep this public to be accessible by other scripts
    private bool tvActivatedOnce = false; // Track if TV has been turned on at least once

    void Start()
    {
        if (tvVideoPlayer == null)
        {
            Debug.LogError("TV VideoPlayer component not assigned!");
        }

        if (tvScreen == null)
        {
            Debug.LogError("TV Screen GameObject not assigned!");
        }

        if (timeText == null)
        {
            Debug.LogError("Time Text component not assigned!");
        }

        if (monsterSpawner == null)
        {
            Debug.LogError("MonsterSpawner component not assigned!");
        }

        tvVideoPlayer.enabled = false; // Ensure the TV starts in the off state
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Check if the left mouse button was pressed
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

        if (isTVOn && IsTVInView())
        {
            timeSinceTVOn += Time.deltaTime;
        }

        timeText.text = $"Time Since TV On: {timeSinceTVOn:F2} seconds";

        // Check for win condition
        if (timeSinceTVOn >= winTime)
        {
            SceneManager.LoadScene("winscene");
        }
    }

    void ToggleVideoPlayback()
    {
        isTVOn = !isTVOn;

        if (isTVOn)
        {
            tvVideoPlayer.enabled = true;
            tvVideoPlayer.Play();
        }
        else
        {
            tvVideoPlayer.Pause();
            tvVideoPlayer.enabled = false;
        }

        if (!tvActivatedOnce && isTVOn)
        {
            tvActivatedOnce = true;
            monsterSpawner.StartSpawning();
        }
    }

    bool IsTVInView()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(tvScreen.transform.position);
        return viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;
    }
}
