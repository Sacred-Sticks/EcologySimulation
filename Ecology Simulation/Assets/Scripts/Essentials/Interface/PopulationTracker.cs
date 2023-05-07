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

    public PopulationTracker(string realName, GameObject trackerUI)
    {
        RealName = realName;
        TrackerUI = trackerUI;
        trackerUI.TryGetComponent(typeof(TMP_Text), out var uiElement);
        if (!uiElement)
            uiElement = trackerUI.AddComponent<TMP_Text>();
        if (uiElement is TMP_Text text)
            TrackerText = text;
        Population = 0;
    }

    public string RealName
    {
        get;
    }

    public int Population
    {
        get => population;
        set
        {
            population = value;
            if (TrackerText)
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
