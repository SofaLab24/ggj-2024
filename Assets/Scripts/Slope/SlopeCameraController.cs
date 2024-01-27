using UnityEngine;

public class SlopeCameraController : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float height = 10f; // Height of the camera above the target
    public float distance = 5f; // Distance behind the target

    private Vector3 offset; // Initial offset from target

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned. Please assign a target object to follow.");
            return;
        }

        // Calculate initial offset based on the desired height and distance
        offset = new Vector3(0, height, -distance);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Update position to follow the target while maintaining the offset
        Vector3 targetPosition = target.position + offset;
        transform.position = targetPosition;

        // Look at the target
        transform.LookAt(target.position);
    }
}
