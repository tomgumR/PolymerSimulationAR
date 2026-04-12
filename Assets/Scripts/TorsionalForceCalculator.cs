using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsionalForceCalculator : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public Transform atom4;
    public float kTorsion = 1f; // Torsional force constant
    public float equilibriumAngle = 0f; // Equilibrium dihedral angle in degrees

    void FixedUpdate()
    {
        float dihedralAngle = CalculateDihedralAngle();
        float angleDifference = dihedralAngle - equilibriumAngle;
        float torsionalForce = -kTorsion * angleDifference;

        ApplyTorsionalForce(torsionalForce);
    }

    float CalculateDihedralAngle()
    {
        Vector3 b1 = atom2.position - atom1.position;
        Vector3 b2 = atom3.position - atom2.position;
        Vector3 b3 = atom4.position - atom3.position;

        Vector3 n1 = Vector3.Cross(b1, b2).normalized;
        Vector3 n2 = Vector3.Cross(b2, b3).normalized;

        Vector3 m1 = Vector3.Cross(n1, b2.normalized);

        float x = Vector3.Dot(n1, n2);
        float y = Vector3.Dot(m1, n2);

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return angle;
    }

    void ApplyTorsionalForce(float torsionalForce)
    {
        Rigidbody rb2 = atom2.GetComponent<Rigidbody>();
        Rigidbody rb3 = atom3.GetComponent<Rigidbody>();
        Rigidbody rb4 = atom4.GetComponent<Rigidbody>();

        Vector3 bondAxis23 = (atom3.position - atom2.position).normalized;
        Vector3 bondAxis34 = (atom4.position - atom3.position).normalized;

        rb3.AddTorque(bondAxis23 * torsionalForce);
        rb4.AddTorque(-bondAxis34 * torsionalForce);
    }
}
