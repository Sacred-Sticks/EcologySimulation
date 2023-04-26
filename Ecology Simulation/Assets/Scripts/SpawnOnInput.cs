using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Essentials.Variables;

public class SpawnOnInput : MonoBehaviour
{
    public Button btnClick; // Reference to the button component that triggers the input retrieval and game object spawning
    public TMP_InputField inputUser; // Reference to the TextMeshPro input field
    public IntVariable input; // Reference to the IntVariable object that stores the integer input
    public GameObject objectToSpawn; // The game object to spawn
    public Transform spawnPoint; // The spawn point for the game object

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button component for click events
        btnClick.onClick.AddListener(SpawnObjectsOnInput);
    }

    // Handler method for the button click event
    public void SpawnObjectsOnInput()
    {
        // Attempt to parse the integer input from the TextMeshPro input field
        int numObjectsToSpawn;
        int.TryParse(inputUser.text, out numObjectsToSpawn);

        // Spawn the specified number of game objects
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            // Generate a random offset vector
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f));

            // Instantiate the object at the spawn point with the random offset
            Instantiate(objectToSpawn, spawnPoint.position + offset, Quaternion.identity);
        }
    }
}