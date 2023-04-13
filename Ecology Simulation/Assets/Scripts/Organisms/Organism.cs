using Essentials.References;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [SerializeField] private FloatReference sustenance;
    [SerializeField] private FloatReference hydration;
    [SerializeField] private FloatReference reproductionCooldown;
    [SerializeField] protected EnumeratedObject species;

    protected float Sustenance
    {
        get
        {
            return sustenance.Value;
        }
        set
        {
            sustenance.Value = value;
        }
    }
    protected float Hydration
    {
        get
        {
            return hydration.Value;
        }
        set
        {
            hydration.Value = value;
        }
    }
    protected float ReproductionCooldown
    {
        get
        {
            return reproductionCooldown.Value;
        }
        set
        {
            reproductionCooldown.Value = value;
        }
    }

    public static Vector3 GetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        var direction = new Vector3(x, y, 0).normalized;
        return direction;
    }
}
