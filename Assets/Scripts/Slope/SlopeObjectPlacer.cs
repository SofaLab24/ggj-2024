using UnityEngine;

public class SlopeObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace; // The prefab you want to place on the slope
    public float spacingZ = 2f; // Spacing between objects along the Z-axis
    public float startZ = 0f; // Starting Z position for the first object
    public float initialPlaneLength = 10f; // Initial length of the slope plane along the Z-axis
    public float maxPlaneLengthMultiplier = 2f; // Multiplier for calculating max plane length
    public float generationInterval = 1f; // Time interval between object generation
    public Transform playerTransform; // Reference to the player's transform
    public float maxXOffset = 5f; // Maximum X-axis offset for object placement

    private float currentZ; // Current Z position for object placement
    private Transform planeTransform;
    private bool hasPlacedInitialObject = false;

    void Start()
    {
        if (objectToPlace == null)
        {
            Debug.LogError("Object to place not assigned.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player transform not assigned.");
            return;
        }

        planeTransform = transform;
        currentZ = startZ;
        hasPlacedInitialObject = true; // Initialize hasPlacedInitialObject to true

        // Set the initial length of the slope plane
        SetPlaneLength(initialPlaneLength);

        // Start generating objects at intervals
        InvokeRepeating("PlaceObjectOnSlope", 0f, generationInterval);
    }

    void PlaceObjectOnSlope()
    {
        // Calculate the Z position for the object, starting from currentZ
        float zPosition = currentZ;

        // Calculate the distance between the player and the generated object
        float distanceToPlayer = Mathf.Abs(playerTransform.position.z - zPosition);

        // Check if the object is within 1000 units of the player
        if (distanceToPlayer <= 1000f)
        {
            // Generate a random X offset within the specified range
            float randomXOffset = Random.Range(-maxXOffset, maxXOffset);

            Vector3 position = new Vector3(playerTransform.position.x + randomXOffset, 0, zPosition);

            // Raycast down to find the slope
            RaycastHit hit;
            if (Physics.Raycast(position + Vector3.up * 50, Vector3.down, out hit))
            {
                // Instantiate the object on the slope
                GameObject newObject = Instantiate(objectToPlace, hit.point, Quaternion.identity);

                // Align object with the slope's normal
                // This step may need adjustments depending on your object's orientation
                // newObject.transform.up = hit.normal;
            }

            // Increase the current Z position for the next object
            currentZ += spacingZ;
        }

        // Calculate the maximum length based on player's position
        float maxPlaneLength = maxPlaneLengthMultiplier * Vector3.Distance(playerTransform.position, planeTransform.position);

        // Adjust the scale of the slope plane to match the new length
        SetPlaneLength(Mathf.Clamp(maxPlaneLength, initialPlaneLength, float.MaxValue));
    }

    void SetPlaneLength(float length)
    {
        Vector3 newScale = planeTransform.localScale;
        newScale.z = length;
        planeTransform.localScale = newScale;
    }

    void Update()
    {
        // Check if any objectToPlace has been passed by the player and destroy it
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ObjectToPlace");
        foreach (var obj in objects)
        {
            if (obj.transform.position.z < playerTransform.position.z - 10)
            {
                Destroy(obj);
            }
        }
    }
}
