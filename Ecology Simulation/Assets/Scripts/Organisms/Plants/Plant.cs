using Essentials.References;
using UnityEngine;

public class Plant : Organism
{
    [SerializeField] private FloatReference hydrationRange;
    [SerializeField] private FloatReference reproductionRange;
    
    protected float HydrationRange { get; private set; }
    protected float ReproductionRange { get; private set; }

    private void Awake()
    {
        HydrationRange = hydrationRange.Value;
        ReproductionRange = reproductionRange.Value;
    }
}
