using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // Sensitivity of mouse movement

    // Rotation limits for X and Y axes
    public float upperLimitX = 80f;
    public float lowerLimitX = -80f;
    public float upperLimitY = 270f;  // Maximum rotation to the right from the initial position
    public float lowerLimitY = 90f;   // Maximum rotation to the left from the initial position

    private float xRotation = 0f;  // To keep track of the accumulated X rotation
    private float yRotation = 0f;  // To keep track of the initial Y rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
        Cursor.visible = false;  // Hide the cursor during gameplay

        // Initialize rotations based on current camera orientation
        Vector3 initialRotation = transform.eulerAngles;
        xRotation = initialRotation.x;
        yRotation = initialRotation.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the xRotation for vertical mouse movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lowerLimitX, upperLimitX);  // Clamping the X rotation

        // Adjust the yRotation for horizontal mouse movement
        yRotation += mouseX;
        yRotation = ClampAngle(yRotation, lowerLimitY, upperLimitY);

        // Apply rotations to the camera
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);
    }

    // Method to clamp angles within a specific range given the initial rotation angle
    private float ClampAngle(float angle, float min, float max)
    {
        angle %= 360;
        if (angle < 0)
            angle += 360;

        float minAngle = (360 + min) % 360;
        float maxAngle = (360 + max) % 360;

        if (minAngle < maxAngle)
            return Mathf.Clamp(angle, minAngle, maxAngle);
        return Mathf.Clamp(angle, maxAngle, minAngle);
    }

    void OnGUI()
    {
        // Draw a simple crosshair in the center of the screen
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        GUI.DrawTexture(new Rect(x - 2, y - 2, 4, 4), Texture2D.whiteTexture);
    }
}
