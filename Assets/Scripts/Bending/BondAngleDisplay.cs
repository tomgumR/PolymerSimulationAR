using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BondAngleDisplay : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public TMP_Text angleText; // Use TMP_Text for TextMeshPro

    void Update()
    {
        float bondAngle = CalculateBondAngle();
        angleText.text = "Bond Angle: " + bondAngle.ToString("F2") + "°";
    }

    float CalculateBondAngle()
    {
        Vector3 v1 = atom1.position - atom2.position;
        Vector3 v2 = atom3.position - atom2.position;

        float angle = Vector3.Angle(v1, v2);
        return angle;
    }
}
