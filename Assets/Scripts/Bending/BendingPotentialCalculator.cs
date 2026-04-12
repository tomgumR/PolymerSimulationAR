using UnityEngine;
using TMPro;

public class BendingPotentialCalculator : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public float kTheta = 100f; // Angle force constant
    public float theta0 = 110f; // Equilibrium bond angle in degrees
    public TMP_Text potentialText; // Use TMP_Text for TextMeshPro

    void Update()
    {
        float bendingPotential = CalculateBendingPotential();
        potentialText.text = "Bending Potential: " + bendingPotential.ToString("F2") + " mJ";
    }

    public float CalculateBendingPotential()
    {
        float currentAngle = CalculateBondAngle();
        float angleDifference = currentAngle - theta0;
        float potential = 0.5f * kTheta * angleDifference * angleDifference;
        return potential; // Potential energy in millijoules
    }

    public float CalculateBondAngle()
    {
        Vector3 v1 = atom1.position - atom2.position;
        Vector3 v2 = atom3.position - atom2.position;

        float angle = Vector3.Angle(v1, v2);
        return angle;
    }
}
