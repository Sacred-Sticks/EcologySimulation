using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneableObject : MonoBehaviour
{
    [SerializeField] private float cloneCooldown;
    [SerializeField] private float deleteCooldown;
    [SerializeField] private float replicationRange;
    [SerializeField] private float maxSpawnDistance; // New serialized field
 
    [SerializeField] private GameObject clonePrefab;
    private float currentCloneCooldown;
    private float currentDeleteCooldown;

    private void Start()
    {
        currentCloneCooldown = cloneCooldown;
        currentDeleteCooldown = deleteCooldown;
    }

    private void Update()
    {
        // Check clone cooldown
        if (currentCloneCooldown <= 0)
        {
            // Spawn a new clone nearby
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * maxSpawnDistance;
            Instantiate(clonePrefab, spawnPosition, Quaternion.identity);

            // Reset clone cooldown
            currentCloneCooldown = cloneCooldown;
        }
        else
        {
            currentCloneCooldown -= Time.deltaTime;
        }

        // Check delete cooldown
        if (currentDeleteCooldown <= 0)
        {
            // Destroy this game object
            Destroy(gameObject);
        }
        else
        {
            currentDeleteCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if other is a clone of this object
        if (other.gameObject.CompareTag("Clone") && other.gameObject.GetComponent<CloneableObject>() != null)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);

            // Check if other is within replication range
            if (distance <= replicationRange)
            {
                // Reduce clone cooldown
                currentCloneCooldown -= Time.deltaTime;
            }
        }
    }
}