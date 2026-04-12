using UnityEngine;

public class AlignHingeJointWithBond1 : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public Transform bond1; // The object whose y-axis will be used for the hinge joint axis

    void Start()
    {
        // Ensure atoms have Rigidbody components
        AddRigidbodyIfNeeded(atom1);
        AddRigidbodyIfNeeded(atom2);
        AddRigidbodyIfNeeded(atom3);

        // Setup hinge joint for atom1-atom2
        HingeJoint hinge1 = atom2.gameObject.AddComponent<HingeJoint>();
        hinge1.connectedBody = atom1.GetComponent<Rigidbody>();
        hinge1.axis = bond1.up; // Set axis to match the bond1's up (y-axis)
        hinge1.anchor = Vector3.zero;

        // Setup hinge joint for atom2-atom3
        HingeJoint hinge2 = atom2.gameObject.AddComponent<HingeJoint>();
        hinge2.connectedBody = atom3.GetComponent<Rigidbody>();
        hinge2.axis = bond1.up; // Set axis to match the bond1's up (y-axis)
        hinge2.anchor = Vector3.zero;

        // Set limits for hinge joints to simulate angular constraints
        JointLimits limits = new JointLimits();
        //float baseAngle = 109.5f;  // Base angle in degrees
        //float variation = 20f;     // Allowed variation in degrees

        // Calculate min and max limits based on allowed variation
        limits.min = -20f;
        limits.max = 50f;

        hinge1.limits = limits;
        hinge1.useLimits = true;

        hinge2.limits = limits;
        hinge2.useLimits = true;
    }

    private void AddRigidbodyIfNeeded(Transform atom)
    {
        if (atom.GetComponent<Rigidbody>() == null)
        {
            atom.gameObject.AddComponent<Rigidbody>();
        }
    }
}
