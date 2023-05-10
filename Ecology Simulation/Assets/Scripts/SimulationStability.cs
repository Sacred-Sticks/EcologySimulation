using System;
using Essentials.Variables;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SimulationStability : MonoBehaviour
{
    // Straight up, this was written by ChatGPT, modified slightly to work with the codebase

    [SerializeField] private IntVariable plantPopulationVariable;
    [SerializeField] private IntVariable rabbitPopulationVariable;
    [SerializeField] private IntVariable foxPopulationVariable;

    private float plantPopulation
    {
        get
        {
            return plantPopulationVariable.Value;
        }
    }
    private float rabbitPopulation
    {
        get
        {
            return rabbitPopulationVariable.Value;
        }
    }
    private float foxPopulation
    {
        get
        {
            return foxPopulationVariable.Value;
        }
    }

    private TMP_Text text;

    [SerializeField] private float r = 0.5f;  // intrinsic growth rate of producers (carrots)
    [SerializeField] private float K = 100f;  // carrying capacity of producers
    [SerializeField] private float c1 = 0.05f; // consumption rate of primary consumers (rabbits) on producers
    [SerializeField] private float c2 = 0.1f;  // consumption rate of secondary consumers (foxes) on primary consumers
    [SerializeField] private float c3 = 0.15f; // mortality rate of foxes
    [SerializeField] private float e1 = 0.5f;  // conversion efficiency of primary consumers (rabbits)
    [SerializeField] private float e2 = 0.3f;  // conversion efficiency of secondary consumers (foxes)
    [SerializeField] private float e3 = 0.7f;  // mortality rate of rabbits

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // Calculate the change rates for each species
        float dP = r * plantPopulation * (1f - plantPopulation / K) - c1 * rabbitPopulation;
        float dR = e1 * c1 * rabbitPopulation * plantPopulation - c2 * foxPopulation - c3 * rabbitPopulation;
        float dF = e2 * c2 * foxPopulation * rabbitPopulation - c3 * e3 * foxPopulation;

        // Print the stability status
        text.text = (dP <= 0f) && (dR <= 0f) && (dF <= 0f) ? "Stable" : "Unstable";
    }
}