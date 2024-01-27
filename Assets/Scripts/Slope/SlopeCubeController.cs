using UnityEngine;

public class SlopeCubeController : MonoBehaviour
{
    public float initialDownwardSpeed = 100f; // Initial speed of downward movement
    public float maxDownwardSpeed = 200f; // Maximum speed of downward movement
    public float downwardAcceleration = 5f; // Rate of speed increase over time for downward movement
    public float currentDownwardSpeed; // Current speed of downward movement
    public float constantMoveSpeed = 1500f; // Constant speed for horizontal movement
    private Rigidbody rb;
    private bool isGrounded;
    public LayerMask groundLayers; // Define which layers are considered as ground
    public float groundCheckDistance = 1f; // Distance to check for ground

    public float maxYRotation = 15f; // Maximum allowed Y rotation before reset
    public float rotationResetSpeed = 1f; // Speed at which the rotation resets

    public float rotationIncreaseSpeed = 1.5f; // Speed at which the rotation increases when moving horizontally

    private bool canIncreaseSpeed = true; // Flag to control speed increase
    private bool resettingRotation = false; // Flag to indicate if the rotation is being reset



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        currentDownwardSpeed = initialDownwardSpeed;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayers);

        if (isGrounded)
        {
            canIncreaseSpeed = true; // Reset flag when grounded
            float moveHorizontal = Input.GetAxis("Horizontal") * constantMoveSpeed;
            Vector3 horizontalMovement = transform.right * moveHorizontal;
            rb.AddForce(horizontalMovement, ForceMode.Force);

            if (moveHorizontal != 0)
            {
                AdjustYRotation(moveHorizontal);
            }
        }

        ResetYRotationIfNeeded();
    }

    void FixedUpdate()
    {
        if (isGrounded && canIncreaseSpeed)
        {
            currentDownwardSpeed += downwardAcceleration * Time.fixedDeltaTime;
            currentDownwardSpeed = Mathf.Min(currentDownwardSpeed, maxDownwardSpeed);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDistance, groundLayers))
            {
                Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.forward, hit.normal).normalized;
                rb.AddForce(slopeDirection * currentDownwardSpeed, ForceMode.Force);
            }
        }
        else if (!isGrounded)
        {
            currentDownwardSpeed = initialDownwardSpeed;
        }
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("ObjectToPlace"))
    {
        Debug.Log("Collision detected");
        currentDownwardSpeed = initialDownwardSpeed + (currentDownwardSpeed  * 0.1f);
        canIncreaseSpeed = false; // Prevent speed increase after collision

        // Reduce the momentum of the cube
        rb.velocity *= 0.5f; // Reduces the velocity to half; adjust the factor as needed
    }
}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * groundCheckDistance);
    }

    private void AdjustYRotation(float horizontalInput)
    {
        float rotationAdjustment = rotationIncreaseSpeed * horizontalInput * Time.deltaTime;
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += rotationAdjustment;
        transform.eulerAngles = currentRotation;
    }

private void ResetYRotationIfNeeded()
{
    // Get the current Y rotation (in euler angles)
    float currentYRotation = transform.eulerAngles.y;

    // Normalize the angle to be between -180 and 180
    if (currentYRotation > 180) currentYRotation -= 360;

    // Check if the Y rotation is greater than the specified limit or if rotation is being reset
    if (Mathf.Abs(currentYRotation) > maxYRotation || resettingRotation)
    {
        // Set the flag to true as we are in the process of resetting the rotation
        resettingRotation = true;

        // Determine the target rotation
        float targetRotation = 0;

        // Gradually reset the Y rotation to 0
        float newYRotation = Mathf.LerpAngle(currentYRotation, targetRotation, rotationResetSpeed * Time.deltaTime);
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y = newYRotation;
        transform.eulerAngles = currentRotation;

        // If the rotation is close enough to 0, stop resetting
        if (Mathf.Abs(newYRotation) < 0.2) // You can adjust the threshold as needed
        {
            resettingRotation = false;
        }
    }
}

}