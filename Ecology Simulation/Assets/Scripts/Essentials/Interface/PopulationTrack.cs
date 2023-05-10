using System;
using System.Collections.Generic;
using UnityEngine;
using Essentials.Events;
using Essentials.Variables;
using UnityEngine.Serialization;

public class PopulationTrack : MonoBehaviour
{
    [SerializeField]
    private GameObject populationInterface;

    [SerializeField]
    private EventBus foxPopulationChange;
    [SerializeField]
    private EventBus rabbitPopulationChange;
    [SerializeField]
    private EventBus carrotPopulationChange;

    [SerializeField]
    private IntVariable foxPopulationVariable;
    [SerializeField]
    private IntVariable rabbitPopulationVariable;
    [SerializeField]
    private IntVariable plantPopulationVariable;

    private PopulationTracker carrotTracker;
    private PopulationTracker rabbitTracker;
    private PopulationTracker foxTracker;

    [SerializeField]
    private GameObject textPrefab;

    private void Awake()
    {
        carrotPopulationChange.Event += OnCarrotPopulationChange;
        rabbitPopulationChange.Event += OnRabbitPopulationChange;
        foxPopulationChange.Event += OnFoxPopulationChange;
    }

    private void OnCarrotPopulationChange(object sender, EventArgs e)
    {
        if (e is not PopulationChangeEventArgs args)
            return;
        if (carrotTracker == null)
        {
            var tracker = Instantiate(textPrefab, populationInterface.transform);
            tracker.transform.localPosition = new Vector3(0, 20, 0);
            carrotTracker = new PopulationTracker("Carrots", tracker)
            {
                Population = 0,
            };
        }
        int newPopulation = plantPopulationVariable.Value + args.PopulationChange;
        if (newPopulation < 0)
            newPopulation = 0;
        carrotTracker.Population = newPopulation;
        plantPopulationVariable.Value = newPopulation;
    }

    private void OnRabbitPopulationChange(object sender, EventArgs e)
    {
        if (e is not PopulationChangeEventArgs args)
            return;
        if (rabbitTracker == null)
        {
            var tracker = Instantiate(textPrefab, populationInterface.transform);
            tracker.transform.localPosition = new Vector3(0, -20, 0);
            rabbitTracker = new PopulationTracker("Rabbits", tracker)
            {
                Population = 0,
            };
        }
        int newPopulation = rabbitPopulationVariable.Value + args.PopulationChange;
        if (newPopulation < 0)
            newPopulation = 0;
        rabbitTracker.Population = newPopulation;
        rabbitPopulationVariable.Value = newPopulation;
    }

    private void OnFoxPopulationChange(object sender, EventArgs e)
    {
        if (e is not PopulationChangeEventArgs args)
            return;
        if (foxTracker == null)
        {
            var tracker = Instantiate(textPrefab, populationInterface.transform);
            tracker.transform.localPosition = new Vector3(0, -60, 0);
            foxTracker = new PopulationTracker("Foxes", tracker)
            {
                Population = 0,
            };
        }
        int newPopulation =foxPopulationVariable.Value + args.PopulationChange;
        if (newPopulation < 0) 
            newPopulation = 0;
        foxTracker.Population = newPopulation;
        foxPopulationVariable.Value = newPopulation;
    }
}

public class PopulationChangeEventArgs : EventArgs
{
    public PopulationChangeEventArgs(int populationChange)
    {
        PopulationChange = populationChange;
    }

    public int PopulationChange { get; }
}
