using UnityEngine;

public class OnRoad : MonoBehaviour
{
    private void Start()
{
// Raise the camera position to have a birds-eye view at the start
transform.position = new Vector3(transform.position.x, 100, transform.position.z);
}

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical 22");

        Vector3 moveDirection = new Vector3(horizontal, 100, vertical);
        transform.position = transform.position + moveDirection * Time.deltaTime;

        // Check if the camera is on the road "Ulitsa Obikolna"
        if (transform.position.x < -5 || transform.position.x > 5 ||
            transform.position.z < -5 || transform.position.z > 5)
        {
            // If not, keep the camera on the road
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -5, 5),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -5, 5)
            );
        }

      
    }
}

