using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TMP_InputField))]
public class SpawnOnInput : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // The game object to spawn
    [SerializeField] private Transform spawnPoint; // The spawn point for the game object
    [SerializeField] private float spawnRange = 2f; // The maximum distance from the spawn point to spawn the objects
    
    private TMP_InputField inputUser; // Reference to the TextMeshPro input field

    private void Awake()
    {
        inputUser = GetComponent<TMP_InputField>();
    }

    public void SpawnObjectsOnInput()
    {
        // Attempt to parse the integer input from the TextMeshPro input field
        int.TryParse(inputUser.text, out int numObjectsToSpawn);

        // Spawn the specified number of game objects
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            // Generate a random offset vector within the specified range
            var offset = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), 0).normalized * Random.Range(0, spawnRange);

            // Instantiate the object at a random position close to the spawn point
            var randomSpawnPos = spawnPoint.position + offset;
            Instantiate(objectToSpawn, randomSpawnPos, Quaternion.identity);
            //gameObject.SetActive(false);
        }
    }
}