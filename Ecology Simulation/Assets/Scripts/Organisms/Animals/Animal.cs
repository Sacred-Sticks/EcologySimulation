using Essentials.References;
using UnityEngine;

public class Animal : Organism
{
    [SerializeField] private FloatReference reproductionDesire;
    [SerializeField] private FloatReference visionDistance;
    [SerializeField] private FloatReference visionAngle;
    [SerializeField] private FloatReference aggression;
    [SerializeField] private Vector2Reference sleepingHours;
    [SerializeField] private EnumeratedObject[] prey;
    
    protected float ReproductionDesire { get; private set; }
    protected float VisionDistance { get; private set; }
    protected float VisionAngle { get; private set; }
    protected float Aggression { get; private set; }
    protected Vector2 SleepingHours { get; private set; }
    protected EnumeratedObject[] Prey { get; private set; }

    private void Awake()
    {
        ReproductionDesire = reproductionDesire.Value;
        VisionDistance = visionDistance.Value;
        VisionAngle = visionAngle.Value;
        Aggression = aggression.Value;
        SleepingHours = sleepingHours.Value;
        Prey = prey;
    }
}
