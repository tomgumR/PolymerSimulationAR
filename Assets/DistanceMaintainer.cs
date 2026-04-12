using UnityEngine;

public class DistanceMaintainer : MonoBehaviour
{
    public Transform atom3;
    public Transform atom4;
    public float targetDistance = 2.0f; // The distance that should be maintained

    void FixedUpdate()
    {
        Vector3 displacement = atom4.position - atom3.position;
        float currentDistance = displacement.magnitude;

        if (currentDistance < targetDistance)
        {
            // Calculate the correction direction and position
            Vector3 correctionDirection = displacement.normalized;
            Vector3 targetPosition = atom3.position + correctionDirection * targetDistance;

            // Move atom4 to the target position
            atom4.position = targetPosition;

            // If atom4 has a Rigidbody, set its velocity to zero to prevent further movement
            Rigidbody rb4 = atom4.GetComponent<Rigidbody>();
            if (rb4 != null)
            {
                rb4.velocity = Vector3.zero;
            }
        }
    }
}
