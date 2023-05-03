using Essentials.References;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [SerializeField] protected FloatReference sustenance;
    [SerializeField] protected FloatReference hydration;
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

    protected float ReproductionCooldown { get; set; }

    public static Vector3 GetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        var direction = new Vector3(x, y, 0).normalized;
        return direction;
    }

    protected virtual void Awake()
    {
        Sustenance = Random.Range(20f, 40f);
        Hydration = Random.Range(20f, 40f);
        ReproductionCooldown = 0;
    }

    protected virtual void Update()
    {
        // Decrease sustenance and hydration over time
        Sustenance -= Time.deltaTime;
        Hydration -= Time.deltaTime;

        // Check for death
        if (Sustenance <= 0 || Hydration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
