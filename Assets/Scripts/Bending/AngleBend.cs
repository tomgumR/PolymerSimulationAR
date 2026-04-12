
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBend : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;

    void Start()
    {
        // Setup hinge joint for atom1-atom2
        HingeJoint hinge1 = atom2.gameObject.AddComponent<HingeJoint>();
        hinge1.connectedBody = atom1.GetComponent<Rigidbody>();
        hinge1.axis = Vector3.down; // Adjust the axis as needed
        hinge1.anchor = Vector3.zero;

        // Setup hinge joint for atom2-atom3
        HingeJoint hinge2 = atom2.gameObject.AddComponent<HingeJoint>();
        hinge2.connectedBody = atom3.GetComponent<Rigidbody>();
        hinge2.axis = Vector3.up; // Adjust the axis as needed
        hinge2.anchor = Vector3.zero;

        // Set limits for hinge joints to simulate angular constraints
        JointLimits limits = new JointLimits();
        //float baseAngle = 110f;  // Base angle in degrees

        // Calculate min and max limits based on allowed variation
        limits.min = -10f;
        limits.max =10f;

        hinge1.limits = limits;
        hinge1.useLimits = true;

        hinge2.limits = limits;
        hinge2.useLimits = true;
    }
}
