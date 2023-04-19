using System;
using System.Collections.Generic;
using Essentials.References;
using UnityEngine;

public class Animal : Organism
{
    [SerializeField] private FloatReference visionDistance;
    [SerializeField] private FloatReference visionAngle;
    [SerializeField] private FloatReference aggression;
    [SerializeField] protected LayerMask prey;
    [SerializeField] protected LayerMask water;
    [SerializeField] protected LayerMask predator;
    [SerializeField] protected SexualCharacteristics sex;

    protected Statistic SexualSatisfaction;
    protected float VisionDistance
    {
        get
        {
            return visionDistance.Value;
        }
        set
        {
            visionDistance.Value = value;
        }
    }
    protected float VisionAngle
    {
        get
        {
            return visionAngle.Value;
        }
        set
        {
            visionAngle.Value = value;
        }
    }
    protected float Aggression
    {
        get
        {
            return aggression.Value;
        }
        set
        {
            aggression.Value = value;
        }
    }
    protected enum Target
    {
        Food,
        Water,
        Predator,
        Mate,
    }

    protected GameObject SearchForItem(Target targetType)
    {
        GameObject output = null;

        int layer = targetType switch
        {
            Target.Food => prey,
            Target.Water => water,
            Target.Predator => predator,
            Target.Mate => gameObject.layer,
            _ => throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null),
        };

        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, VisionDistance, new ContactFilter2D(), results);
        float closestDistance = float.PositiveInfinity;
        foreach (var result in results)
        {
            int resultLayer = result.gameObject.layer;
            if (layer != (layer | (1 << resultLayer)))
                continue;
            float distance = Vector3.Distance(transform.position, result.transform.position);
            if (!(distance < closestDistance))
                continue;
            closestDistance = distance;
            output = result.gameObject;
        }

        return output;
    }

    protected enum SexualCharacteristics
    {
        Male,
        Female,
    }

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
