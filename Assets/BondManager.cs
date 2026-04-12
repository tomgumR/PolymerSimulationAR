using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    public Transform atom1;
    public Transform atom2;

    void Update()
    {
        AlignBond();
    }

    void AlignBond()
    {
        if (atom1 != null && atom2 != null)
        {
            // Position the cylinder at the midpoint between the two atoms
            transform.position = (atom1.position + atom2.position) / 2;

            // Calculate the direction from atom1 to atom2
            Vector3 direction = atom2.position - atom1.position;

            // Adjust the rotation of the cylinder to align with the direction
            transform.up = direction;

            // Adjust the scale of the cylinder to match the distance between the atoms
            float distance = direction.magnitude;
            transform.localScale = new Vector3(transform.localScale.x, distance / 2, transform.localScale.z);
        }
    }
}
