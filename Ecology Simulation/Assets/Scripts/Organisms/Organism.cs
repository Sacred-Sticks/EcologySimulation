using Essentials.References;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [SerializeField] private FloatReference sustenance;
    [SerializeField] private FloatReference hydration;
    [SerializeField] private FloatReference reproductionCooldown;
    
    protected float Sustenance { get; private set; }
    protected float Hydration { get; private set; }
    protected float ReproductionCooldown { get; private set; }

    private void Awake()
    {
        Sustenance = sustenance.Value;
        Hydration = hydration.Value;
        ReproductionCooldown = reproductionCooldown.Value;
    }
}
