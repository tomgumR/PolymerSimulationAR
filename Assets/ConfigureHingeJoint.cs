using UnityEngine;

public class ConfigureHingeJoints : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public Transform atom4;
    public float hingeSpring = 50f; // Spring value for hinge joints
    public float hingeDamper = 5f; // Damper value for hinge joints

    void Start()
    {
        // Add hinge joint for Atom2 to rotate around the bond between Atom1 and Atom2
        AddHingeJoint(atom2, atom1, Vector3.right); // Assuming the bond axis is along the x-axis

        // Add hinge joint for Atom3 to rotate around the bond between Atom2 and Atom3
        AddHingeJoint(atom3, atom2, Vector3.right); // Assuming the bond axis is along the x-axis

        // Add hinge joint for Atom4 to rotate around the bond between Atom3 and Atom4
        AddHingeJoint(atom4, atom3, Vector3.right); // Assuming the bond axis is along the x-axis
    }

    void AddHingeJoint(Transform atom, Transform connectedAtom, Vector3 axis)
    {
        HingeJoint hinge = atom.gameObject.AddComponent<HingeJoint>();
        hinge.connectedBody = connectedAtom.GetComponent<Rigidbody>();

        // Set the axis of rotation to be aligned with the bond axis
        hinge.axis = axis;

        // Set the spring and damper values to control rotation behavior
        JointSpring spring = new JointSpring
        {
            spring = hingeSpring,
            damper = hingeDamper,
            targetPosition = 0f
        };
        hinge.spring = spring;
        hinge.useSpring = true;

        // Set angular limits if necessary
        JointLimits limits = new JointLimits
        {
            min = -45f, // Minimum rotation angle
            max = 45f // Maximum rotation angle
        };
        hinge.limits = limits;
        hinge.useLimits = true;
    }
}
