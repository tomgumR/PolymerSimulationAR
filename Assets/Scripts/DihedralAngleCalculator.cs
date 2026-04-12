using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DihedralAngleCalculator : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public Transform atom4;
    public TMP_Text dihedralAngleText; // Use TMP_Text for TextMeshPro

    void Update()
    {
        float dihedralAngle = CalculateDihedralAngle();
        dihedralAngleText.text = "Dihedral Angle: " + dihedralAngle.ToString("F2") + "°";
    }

    // Change access modifier to public
    public float CalculateDihedralAngle()
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
}
