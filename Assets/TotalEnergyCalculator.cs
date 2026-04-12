using UnityEngine;
using TMPro;

public class TotalEnergyCalculator : MonoBehaviour
{
    public BendingPotentialCalculator bendingCalculator;
    public StretchingPotentialCalculator stretchingCalculator;
    public TorsionalEnergyCalculator torsionalCalculator;
    public TMP_Text totalEnergyText; // TextMeshPro text element to display the total energy

    // Make totalEnergy a public property or field
    public float totalEnergy { get; private set; }

    void Update()
    {
        float bendingPotential = bendingCalculator.CalculateBendingPotential() * 0.001f;
        float stretchingPotential = stretchingCalculator.CalculateStretchingPotential();
        float torsionalEnergy = torsionalCalculator.CalculateTorsionalEnergy(torsionalCalculator.CalculateDihedralAngle());

        totalEnergy = bendingPotential + stretchingPotential + torsionalEnergy;
        totalEnergyText.text = "Total Energy: " + totalEnergy.ToString("F2") + " J";
    }
}
