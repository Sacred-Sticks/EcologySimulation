using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationTracker
{
    private string realName;
    private string techName;
    private int population;
    private GameObject trackerUI;
    private TMP_Text trackerText;

    public PopulationTracker(string realName, string techName, GameObject trackerUI)
    {
        RealName = realName;
        TechName = techName;
        TrackerUI = trackerUI;
        TrackerText = TrackerUI.GetComponent<TMP_Text>();
        Population = 0;
    }

    public string RealName
    {
        get;
    }

    public string TechName
    {
        get;
    }

    public int Population
    {
        get => population;
        set
        {
            population = value;
            if (TrackerUI != null & TrackerText != null)
            {
                TrackerText.text = $"{RealName}: {Population}";
            }
        }
    }

    public GameObject TrackerUI
    {
        get;
        set;
    }

    public TMP_Text TrackerText
    {
        get;
        set;
    }
}
