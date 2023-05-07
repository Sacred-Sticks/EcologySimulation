using System;
using System.Collections.Generic;
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
        if (carrotTracker == null)
        {
            var tracker = Instantiate(textPrefab, populationInterface.transform);
            tracker.transform.localPosition = new Vector3(0, 20, 0);
            carrotTracker = new PopulationTracker("Carrots", tracker)
            {
                Population = 0,
            };
        }
        carrotTracker.Population += args.PopulationChange;
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
        rabbitTracker.Population += args.PopulationChange;
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
        foxTracker.Population += args.PopulationChange;
    }

    // void Start()
    // {
    //     if (populationInterface == null)
    //     {
    //         Debug.LogWarning("Population tracker is not connected to any interface.");
    //         enabled = false;
    //         return;
    //     }
    //     var trackers = new List<GameObject>();
    //     int yLevel = 20;
    //     for (int i = 0; i < 3; i++)
    //     {
    //         var tracker = Instantiate(textPrefab, populationInterface.transform);
    //         tracker.transform.localPosition = new Vector3(0, yLevel, 0);
    //         trackers.Add(tracker);
    //         yLevel -= 40;
    //     }
    //     carrotTracker = new PopulationTracker("Carrots", trackers[0])
    //     {
    //         Population = 0,
    //     };
    //     rabbitTracker = new PopulationTracker("Rabbits", trackers[1])
    //     {
    //         Population = 0,
    //     };
    //     foxTracker = new PopulationTracker("Foxes", trackers[2])
    //     {
    //         Population = 0,
    //     };
    // }
}

public class PopulationChangeEventArgs : EventArgs
{
    public PopulationChangeEventArgs(int populationChange)
    {
        PopulationChange = populationChange;
    }

    public int PopulationChange { get; }
}
