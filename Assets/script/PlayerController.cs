using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return; // Prevent movement during freeze

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        rb.AddForce(movement);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Cube"))
        {
            Cube cube = other.gameObject.GetComponent<Cube>();
            if (cube != null)
            {
                FindObjectOfType<CubeController>().CheckJumpedCube(cube.id);
            }
        }
    }
}
