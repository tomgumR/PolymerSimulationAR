using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAtoms : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public float bondLength = 1.54f; // Bond length in meters
    public float bondAngle = 109.5f; // Bond angle in degrees
    
    void Start()
    {
        // Position atom1 at origin
        atom1.position = Vector3.zero;

        // Position atom2 at bond length along x-axis
        atom2.position = new Vector3(bondLength, 0, 0);

        // Calculate position of atom3 using bond angle and length
        float angleRadians = bondAngle * Mathf.Deg2Rad;
        float x3 = bondLength * Mathf.Cos(angleRadians);
        float y3 = bondLength * Mathf.Sin(angleRadians);

        // Position atom3
        atom3.position = new Vector3(x3, y3, 0);
    }
}
