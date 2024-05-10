using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 5.0f;  // Speed of the tank movement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Optional: Lock cursor to center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
    }

   private void Move()
{
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
    rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
}

}
