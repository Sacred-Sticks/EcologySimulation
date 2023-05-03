using Essentials.References;
using UnityEngine;

public class Plant : Organism
{
    [SerializeField] protected FloatReference hydrationRange;
    [SerializeField] protected FloatReference reproductionRange;
    protected float HydrationRange { get; set; }
    protected float ReproductionRange { get; set; }

    protected override void Awake()
    {
        base.Awake();
        HydrationRange = hydrationRange.Value;
        ReproductionRange = reproductionRange.Value;
    }

    protected override void Update()
    {
        base.Update();

        // Check hydration
        if (Hydration <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Hydration -= Time.deltaTime;
        }

        // Check reproduction cooldown
        if (ReproductionCooldown > 0)
        {
            ReproductionCooldown -= Time.deltaTime;
        }
    }

    protected virtual void Reproduce()
    {
        if (ReproductionCooldown <= 0)
        {
            // Spawn a new game object nearby
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * ReproductionRange;
            Instantiate(gameObject, spawnPosition, Quaternion.identity);

            // Reset reproduction cooldown
            ReproductionCooldown = Random.Range(10f, 20f);
        }
    }
}