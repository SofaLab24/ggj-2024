using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class SlopeCubeController : MonoBehaviour
{
    private AudioSource audioSource;
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
    private float startingZPosition; // Starting Z position of the player
    public float score; // Score of the player
    public float scoreMultiplier = 0.00001f; // Multiplier for calculating the score
    private float timeSinceLastGrounded = 0f; // Timer to track time since last collision
    private float timeSinceLastStopped = 0f; // Timer to track time since last collision
    private bool isPlayerAlive = true; // Flag to track if the player is alive
    public const float deathTimeGrounded = 3f; // Time in seconds to die if no collision
    public const float deathTimeStopped = 7f; // Time in seconds to die if no collision
    public Volume volume;
    private Bloom bloom;
    private Vignette vignette;
    private float lastZPosition; // Tracks the last Z position
    private float timeSinceZChange = 0f; // Time since the last Z position change
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        currentDownwardSpeed = initialDownwardSpeed;

        // Record the starting Z position of the player
        startingZPosition = transform.position.z;
        audioSource = FindObjectOfType<AudioSource>();
    }

    void Update()
    {
        if (!isPlayerAlive)
        {
            return; // If the player has died, no need to execute further update code
        }

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayers);

        if (isGrounded)
        {
            timeSinceLastGrounded = 0f; // Reset timer when grounded
            float moveHorizontal = Input.GetAxis("Horizontal") * constantMoveSpeed;
            Vector3 horizontalMovement = transform.right * moveHorizontal;
            rb.AddForce(horizontalMovement, ForceMode.VelocityChange);

            if (moveHorizontal != 0)
            {
                AdjustYRotation(moveHorizontal);
            }
        }
        if (Mathf.Abs(transform.position.z - lastZPosition) > 0.001) // Use a small threshold to account for floating-point imprecision
        {
            lastZPosition = transform.position.z; // Update the last Z position
            timeSinceZChange = 0f; // Reset the timer
        }
        else
        {
            timeSinceZChange += Time.deltaTime; // Accumulate the time
        }

        if (timeSinceZChange > 1f) // Check if the Z position has not changed for more than 1 second
        {
            canIncreaseSpeed = false;
        }
        else
        {
            canIncreaseSpeed = true;
        }

        ResetYRotationIfNeeded();

        // Update the score based on the player's Z position
        score = (transform.position.z - startingZPosition) * scoreMultiplier;
        textMeshPro.text = "Cash: " + score.ToString("F2") + "Lt";

        if (!isGrounded)
        {
            // Accumulate the time since the last collision
            timeSinceLastGrounded += Time.deltaTime;
        }

        if (!canIncreaseSpeed)
        {
            // Accumulate the time since the last collision
            timeSinceLastStopped += Time.deltaTime;
        }

        // Check if the time since the last collision exceeds the death time
        if (timeSinceLastGrounded > deathTimeGrounded)
        {
            Death();
        }

        if (timeSinceLastStopped > deathTimeStopped)
        {
            Death();
        }
        audioSource.pitch = 1 + (rb.velocity.z / 100);
    }

    void Death()
    {
        Debug.Log("You died");
        float money = PlayerPrefs.GetFloat("money");
        money += float.Parse(score.ToString("F2"));
        PlayerPrefs.SetFloat("money", money);
        isPlayerAlive = false; // Set the player as dead
        SceneManager.LoadScene(0);
        // Additional logic for player death can be added here
    }
    void FixedUpdate()
    {
        if (isGrounded && canIncreaseSpeed)
        {   
            timeSinceLastStopped = 0f; // Reset timer when grounded
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
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out vignette);
        vignette.intensity.value += 0.05f;
        bloom.intensity.value *= 5f;
        currentDownwardSpeed = initialDownwardSpeed + (currentDownwardSpeed  * 0.1f);

        // Reduce the momentum of the cube
        rb.velocity *= 0.7f;
    }
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