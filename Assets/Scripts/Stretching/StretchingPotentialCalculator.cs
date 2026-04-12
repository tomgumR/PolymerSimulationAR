using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class StretchingPotentialCalculator : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;
    public float kr = 50f; // Bond force constant
    public TMP_Text potentialText; // Use TMP_Text for TextMeshPro

    private float r0; // Equilibrium bond length

    void Start()
    {
        // Calculate the initial bond length as the equilibrium length
        r0 = Vector3.Distance(atom1.position, atom2.position);
    }

    void Update()
    {
        float stretchingPotential = CalculateStretchingPotential();
        potentialText.text = "Stretching Potential: " + stretchingPotential.ToString("F2") + " J";
    }

    public float CalculateStretchingPotential()
    {
        float r = Vector3.Distance(atom1.position, atom2.position); // Current bond length
        float displacement = r - r0;
        float potential = 0.5f * kr * displacement * displacement*10000;
        return potential;
    }
}
