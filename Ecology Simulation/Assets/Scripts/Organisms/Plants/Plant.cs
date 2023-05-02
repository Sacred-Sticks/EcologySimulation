using Essentials.References;
using UnityEngine;

public class Plant : Organism
{
    [SerializeField] protected FloatReference hydrationRange;
    [SerializeField] protected FloatReference reproductionRange;
    protected virtual void Awake()
    {
        // ...
    }
    protected virtual void Update()
    {
        // ...
    }
    protected float HydrationRange { get; set; }
    protected float ReproductionRange { get; set; }
}
