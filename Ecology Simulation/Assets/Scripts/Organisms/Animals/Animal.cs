using System;
using System.Collections.Generic;
using Essentials.References;
using UnityEngine;

public class Animal : Organism
{
    [SerializeField] private FloatReference reproductionDesire;
    [SerializeField] private FloatReference visionDistance;
    [SerializeField] private FloatReference visionAngle;
    [SerializeField] private FloatReference aggression;
    [SerializeField] protected LayerMask prey;
    [SerializeField] protected LayerMask water;
    [SerializeField] protected SexualCharacteristics sex;

    protected float ReproductionDesire
    {
        get
        {
            return reproductionDesire.Value;
        }
        set
        {
            reproductionDesire.Value = value;
        }
    }
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
        Mate,
    }

    protected GameObject SearchForItem(Target targetType)
    {
        GameObject output = null;

        int layer = targetType switch
        {
            Target.Food => prey,
            Target.Water => water,
            Target.Mate => gameObject.layer,
            _ => throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null),
        };

        var results = Physics2D.OverlapCircle(transform.position, VisionDistance, layer);
        if (results)
            output = results.gameObject;

        return output;
    }

    protected enum SexualCharacteristics
    {
        Male,
        Female,
    }
}
