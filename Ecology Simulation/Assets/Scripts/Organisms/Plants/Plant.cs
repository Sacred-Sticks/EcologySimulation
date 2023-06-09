using System;
using Essentials.References;
using UnityEngine;

public class Plant : Organism
{
    [SerializeField] private FloatReference hydrationRange;
    [SerializeField] private FloatReference reproductionRange;
    
    protected float HydrationRange { get; set; }
    protected float ReproductionRange { get; set; }
    protected Statistic SexualSatisfaction { get; private set; }

    public void SetStatistic(Statistic statistic)
    {
        switch (statistic.StatisticType)
        {
            case Statistic.StatType.Sustenance:
                if (Sustenance)
                {
                    Debug.LogWarning($"{nameof(Sustenance)} Already Set");
                    break;
                }
                Sustenance = statistic;
                break;
            case Statistic.StatType.Hydration:
                if (Hydration)
                {
                    Debug.LogWarning($"{nameof(Hydration)} Already Set");
                    break;
                }
                Hydration = statistic;
                break;
            case Statistic.StatType.SexualSatisfaction:
                if (SexualSatisfaction)
                {
                    Debug.LogWarning($"{nameof(SexualSatisfaction)} Already Set");
                    break;
                }
                SexualSatisfaction = statistic;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statistic), statistic, null);
        }
    }
}
