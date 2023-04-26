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
        int layer = GetLayer(targetType);

        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, VisionDistance, new ContactFilter2D(), results);

        ReduceByAngle(results, VisionAngle);

        FindClosestGameObject(results, layer, out var output);

        return output;
    }
    
    private void ReduceByAngle(List<Collider2D> targets, float angleOfVision) {
        var visibleTargets = new List<Collider2D>();
        var forward = Vector3.up;

        foreach (var target in targets) {
            var direction = target.transform.position - transform.position;
            direction = transform.InverseTransformDirection(direction);
            float angle = Vector3.Angle(forward, direction);

            if (angle <= angleOfVision && angle >= -angleOfVision) {
                visibleTargets.Add(target);
            }
        }

        targets.Clear();
        targets.AddRange(visibleTargets);
    }

    private void FindClosestGameObject(List<Collider2D> results, int layer, out GameObject output)
    {
        float closestDistance = float.PositiveInfinity;
        output = null;
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
    }

    private Vector3 RotatePoint(Vector3 point, Vector3 angles) {
        var direction = point - transform.position;
        direction = Quaternion.Euler(angles) * direction;
        point = direction + transform.position;
        return point;
    }

    private int GetLayer(Target targetType)
    {
        return targetType switch
        {
            Target.Food => prey,
            Target.Water => water,
            Target.Predator => predator,
            Target.Mate => gameObject.layer,
            _ => throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null),
        };
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
