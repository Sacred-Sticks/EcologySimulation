using System;
using UnityEngine;

[RequireComponent(typeof(Organism))]
public class Statistic : MonoBehaviour
{
    [SerializeField] private float statLifetime;
    [SerializeField] private StatType statisticType;

    public float Value { get; private set; }
    public StatType StatisticType
    {
        get
        {
            return statisticType;
        }
    }

    public enum StatType
    {
        Sustenance,
        Hydration,
        SexualSatisfaction,
    }

    private float changePerSecond;

    private void Awake()
    {
        var organism = GetComponent<Organism>();
        switch (organism)
        {
            case Animal animal:
                animal.SetStatistic(this);
                break;
            case Plant plant:
                plant.SetStatistic(this);
                break;
        }
    }

    private void Start()
    {
        Value = 1;
        if (statLifetime == 0)
            changePerSecond = 0;
        else
            changePerSecond = 1 / statLifetime;
    }

    private void Update()
    {
        LowerStat();
    }

    private void LowerStat()
    {
        AddToStat(-(changePerSecond * Time.deltaTime));

        if (Value <= 0)
            Destroy(gameObject);
    }

    public void AddToStat(float addition)
    {
        Value += addition;

        if (Value > 1)
            Value = 1;
    }
}
