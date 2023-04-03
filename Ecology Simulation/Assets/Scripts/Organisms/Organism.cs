using Essentials.References;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [SerializeField] private FloatReference sustenance;
    [SerializeField] private FloatReference hydration;
    [SerializeField] private FloatReference reproductionCooldown;
    [SerializeField] protected EnumeratedObject species;
    
    protected float Sustenance { get; set; }
    protected float Hydration { get; set; }
    protected float ReproductionCooldown { get; set; }

    protected virtual void Awake()
    {
        Sustenance = sustenance.Value;
        Hydration = hydration.Value;
        ReproductionCooldown = reproductionCooldown.Value;
    }
}
