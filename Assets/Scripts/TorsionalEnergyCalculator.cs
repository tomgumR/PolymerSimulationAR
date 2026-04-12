using UnityEngine;
using TMPro;

public class TorsionalEnergyCalculator : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public Transform atom3;
    public Transform atom4;
    private float kTorsion = 24.36f; // Torsional force constant in J/mol/rad²
    private float equilibriumAngle = 0f; // Equilibrium dihedral angle in degrees
    public TMP_Text energyText; // TextMeshPro text element to display the torsional energy

    void FixedUpdate()
    {
        float dihedralAngle = CalculateDihedralAngle();
        float torsionalEnergy = CalculateTorsionalEnergy(dihedralAngle);

        ApplyTorsionalForce(torsionalEnergy);
        UpdateEnergyText(torsionalEnergy);
    }

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

    public float CalculateTorsionalEnergy(float dihedralAngle)
    {
        float angleDifference = dihedralAngle - equilibriumAngle;
        float torsionalEnergy = 0.5f * kTorsion * Mathf.Pow(angleDifference * Mathf.Deg2Rad, 2); // Convert angle difference to radians for calculation
        return torsionalEnergy;
    }

    void ApplyTorsionalForce(float torsionalEnergy)
    {
        Rigidbody rb3 = atom3.GetComponent<Rigidbody>();
        Rigidbody rb4 = atom4.GetComponent<Rigidbody>();

        Vector3 bondAxis = (atom4.position - atom3.position).normalized;

        float torsionalForce = Mathf.Sqrt(2 * torsionalEnergy / kTorsion);
        rb3.AddTorque(bondAxis * torsionalForce);
        rb4.AddTorque(-bondAxis * torsionalForce);
    }

    void UpdateEnergyText(float torsionalEnergy)
    {
        energyText.text = "Torsional Energy: " + torsionalEnergy.ToString("F2") + " J";
    }
}
