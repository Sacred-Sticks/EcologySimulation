using System;
using UnityEngine;
using Essentials.Events;

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
        carrotTracker.Population += args.PopulationChange;
    }

    private void OnRabbitPopulationChange(object sender, EventArgs e)
    {
        if (e is not PopulationChangeEventArgs args)
            return;
        rabbitTracker.Population += args.PopulationChange;
    }

    private void OnFoxPopulationChange(object sender, EventArgs e)
    {
        if (e is not PopulationChangeEventArgs args)
            return;
        foxTracker.Population += args.PopulationChange;
    }

    void Start()
    {
        if (populationInterface == null)
        {
            Debug.LogWarning("Population tracker is not connected to any interface.");
            enabled = false;
            return;
        }

        int yLevel = 20;
        var newTracker = Instantiate(textPrefab, populationInterface.transform);
        newTracker.transform.localPosition = new Vector3(0, yLevel, 0);
        if (carrotTracker == null)
        {
            carrotTracker = new PopulationTracker("Carrots", newTracker)
            {
                Population = 0,
            };
        }
        yLevel -= 40;
        newTracker = Instantiate(textPrefab, populationInterface.transform);
        newTracker.transform.localPosition = new Vector3(0, yLevel, 0);
        if (rabbitTracker == null)
        {
            rabbitTracker = new PopulationTracker("Rabbits", newTracker)
            {
                Population = 0,
            };
        }
        yLevel -= 40;
        newTracker = Instantiate(textPrefab, populationInterface.transform);
        newTracker.transform.localPosition = new Vector3(0, yLevel, 0);
        if (foxTracker == null)
        {
            foxTracker = new PopulationTracker("Foxes", newTracker)
            {
                Population = 0,
            };
        }
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
