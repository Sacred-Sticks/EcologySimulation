using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{
    public GameObject objectToSpawn; // The game object to spawn
    public GetInputOnClick val;

    // Spawn the game object based on the integer value
    public void Spawn(int value)
    {
        if (value > 0)
        {
            // Spawn the game object at the position of this transform
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
    }
}