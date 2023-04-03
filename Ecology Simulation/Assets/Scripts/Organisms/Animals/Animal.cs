using Essentials.References;
using UnityEngine;

public class Animal : Organism
{
    [SerializeField] private FloatReference reproductionDesire;
    [SerializeField] private FloatReference visionDistance;
    [SerializeField] private FloatReference visionAngle;
    [SerializeField] private FloatReference aggression;
    [SerializeField] private Vector2Reference sleepingHours;
    [SerializeField] protected EnumeratedObject[] prey;
    [SerializeField] protected SexualCharacteristics sex;
    
    protected float ReproductionDesire { get; set; }
    protected float VisionDistance { get; set; }
    protected float VisionAngle { get; set; }
    protected float Aggression { get; set; }
    protected Vector2 SleepingHours { get; set; }

    protected enum SexualCharacteristics
    {
        Male,
        Female,
    }

    protected override void Awake()
    {
        base.Awake();
        ReproductionDesire = reproductionDesire.Value;
        VisionDistance = visionDistance.Value;
        VisionAngle = visionAngle.Value;
        Aggression = aggression.Value;
        SleepingHours = sleepingHours.Value;
    }
}
