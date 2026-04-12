using UnityEngine;

public class RotateAroundCylinderAxis : MonoBehaviour
{
    public Rigidbody sphereRigidbody; // The Rigidbody of the sphere
    public Transform cylinderTransform; // The Transform of the cylinder
    public float torqueAmount = 10f;  // The amount of torque to apply

    void Start()
    {
        // Ensure the Rigidbody is attached
        if (sphereRigidbody == null)
        {
            sphereRigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Apply torque around the cylinder's local y-axis when the space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 torqueAxis = cylinderTransform.up; // Assuming the cylinder's local y-axis is the axis of rotation
            sphereRigidbody.AddTorque(torqueAxis * torqueAmount, ForceMode.Force);
        }
    }
}
