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
    float zPosition = currentZ;
    float distanceToPlayer = Mathf.Abs(playerTransform.position.z - zPosition);

    if (distanceToPlayer <= 1000f)
    {
        float randomXOffset = Random.Range(-maxXOffset, maxXOffset);
        Vector3 position = new Vector3(playerTransform.position.x + randomXOffset, 0, zPosition);

        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 50, Vector3.down, out hit))
        {
            Vector3 forwardOnSlope = Vector3.ProjectOnPlane(Vector3.forward, hit.normal);

            // Instantiate the object on the slope with a slight downward adjustment
            GameObject newObject = Instantiate(objectToPlace, hit.point + Vector3.up, Quaternion.LookRotation(forwardOnSlope, hit.normal));
        }

        currentZ += spacingZ;
    }
}


void SetPlaneLength(float length)
{
    Vector3 newScale = planeTransform.localScale;
    newScale.z = Mathf.Min(length, initialPlaneLength * maxPlaneLengthMultiplier); // Ensure it doesn't exceed the max length
    planeTransform.localScale = newScale;
}


    void Update()
    {
        // Check the player's distance from the end of the plane
        float planeEndZ = planeTransform.position.z + (planeTransform.localScale.z * 0.5f); // Calculate end position of the plane
        float distanceToEndOfPlane = Mathf.Abs(playerTransform.position.z - planeEndZ);

        // If the player is within 1000 units of the end of the plane, increase the plane length by 500
        if (distanceToEndOfPlane <= 1000f)
        {
            float newPlaneLength = planeTransform.localScale.z + 500f; // Increase the plane's length
            SetPlaneLength(newPlaneLength);
        }

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
