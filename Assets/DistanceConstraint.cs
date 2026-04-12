using UnityEngine;

public class DistanceConstraint : MonoBehaviour
{
    public Transform atom2;
    public Transform atom3;
    public float minDistance = 1.0f; // Minimum allowed distance

    void FixedUpdate()
    {
        Vector3 displacement = atom2.position - atom3.position;
        float currentDistance = displacement.magnitude;

        if (currentDistance < minDistance)
        {
            // Calculate the direction to move atom2 away from atom3
            Vector3 correctionDirection = displacement.normalized;
            float correctionMagnitude = minDistance - currentDistance;

            // Move atom2 to maintain the minimum distance
            Rigidbody rb2 = atom2.GetComponent<Rigidbody>();
            if (rb2 != null)
            {
                rb2.MovePosition(atom3.position + correctionDirection * minDistance);
                rb2.velocity = Vector3.zero; // Stop the velocity to avoid oscillation
            }
        }
    }
}
