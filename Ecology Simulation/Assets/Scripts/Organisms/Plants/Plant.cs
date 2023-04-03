using Essentials.References;
using UnityEngine;

public class Plant : Organism
{
    [SerializeField] private FloatReference hydrationRange;
    [SerializeField] private FloatReference reproductionRange;
    
    protected float HydrationRange { get; set; }
    protected float ReproductionRange { get; set; }

    protected override void Awake()
    {
        base.Awake();
        HydrationRange = hydrationRange.Value;
        ReproductionRange = reproductionRange.Value;
    }
}
