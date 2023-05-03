using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials.References;

public class SunflowerPlant : Plant
{
    public FloatReference hydrationRange2;
    public FloatReference reproductionRange2;
    public float hydrationTimer;
    public float reproductionCooldown;
    private float reproductionTimer;

    protected override void Awake()
    {
        base.Awake();

        hydrationTimer = hydrationRange2.Value;
        reproductionTimer = 0f;
    }

    protected override void Update()
    {
        base.Update();

        // Check hydration timer
        if (hydrationTimer <= 0)
        {
            // Destroy the game object
            Destroy(gameObject);
        }
        else
        {
            hydrationTimer -= Time.deltaTime;
        }

        // Check reproduction timer
        if (reproductionTimer <= 0)
        {
            // Spawn a new game object nearby
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * reproductionRange2.Value;
            Instantiate(gameObject, spawnPosition, Quaternion.identity);

            // Reset reproduction timer
            reproductionTimer = reproductionCooldown;
        }
        else
        {
            reproductionTimer -= Time.deltaTime;
        }
    }
}