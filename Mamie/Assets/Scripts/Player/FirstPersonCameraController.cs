using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerCamera;  // Assign the player's camera in the Unity Editor

    private float xRotation = 0f;

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Invert and clamp the vertical camera rotation to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera for looking up and down
        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // Rotate the entire GameObject this script is attached to (usually the player body)
        transform.Rotate(Vector3.up * mouseX);
    }
}
