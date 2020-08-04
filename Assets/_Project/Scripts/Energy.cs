using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float _energyPoints = 5f;

    public float EnergyPoints { get => _energyPoints;  }
}
