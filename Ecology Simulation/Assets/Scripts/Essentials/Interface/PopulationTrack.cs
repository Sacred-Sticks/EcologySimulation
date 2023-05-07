using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PopulationTrack : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField]
    private GameObject populationInterface;
    // Put a species name as they are referred to in the scene hierarchy as the key.
    [SerializeField]
    private Dictionary<string, string> populations = new Dictionary<string, string>()
    {
        {"Carrots","Carrot"},
        {"Rabbits","Rabbit"},
        {"Foxes","Fox"}
    };

    private List<PopulationTracker> trackers = new List<PopulationTracker>();

    [SerializeField]
    private GameObject textPrefab;
     
    void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (populationInterface == null)
        {
            Debug.LogWarning("Population tracker is not connected to any interface.");
            enabled = false;
        }
        else
        {
            int yLevel = 20;
            foreach (KeyValuePair<string, string> trackedPopulation in populations)
            {
                GameObject newTracker = Instantiate(textPrefab, populationInterface.transform);
                newTracker.name = trackedPopulation.Value;
                newTracker.transform.localPosition = new Vector3(0, yLevel, 0);
                trackers.Add(new PopulationTracker(trackedPopulation.Key, trackedPopulation.Value, newTracker));
                yLevel -= 40;
            }
        }
    }

    void Update()
    {
        // Give me a bit to think of a better way.
        GameObject[] rootObjects = currentScene.GetRootGameObjects();
        trackers.ForEach(t => t.Population = 0);
        foreach (GameObject obj in rootObjects)
        {
            foreach (PopulationTracker tracker in trackers)
            {
                if (tracker.TechName.Equals(obj.name))
                {
                    tracker.Population++;
                    break;
                }
            }
        }
    }
}