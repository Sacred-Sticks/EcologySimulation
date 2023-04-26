using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject timeUI;
    private TMP_Text dateDisplay;
    private TMP_Text timeDisplay;
    private DateTime currentTime;
    [SerializeField]
    private int minute;
    [SerializeField]
    private int hour;
    [SerializeField]
    private int day;
    [SerializeField]
    private int month;
    [SerializeField]
    private double timeScale;
    private int year = 2001;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = new DateTime(year, month, day, hour, minute, 0);
        if (timeUI == null)
        {
            Debug.LogWarning("TimeManager is not connected to any valid UI.");
        }
        else
        {
            dateDisplay = timeUI.transform.Find("DateDisplay").GetComponent<TMP_Text>();
            timeDisplay = timeUI.transform.Find("TimeDisplay").GetComponent<TMP_Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        double passedTime = (double)(Time.deltaTime * Time.timeScale * timeScale);
        currentTime = currentTime.AddMinutes(passedTime);
        minute = currentTime.Minute;
        hour = currentTime.Hour;
        day = currentTime.Day;
        month = currentTime.Month;
        year = currentTime.Year - 2000;
        if (timeUI != null)
        {
            dateDisplay.text = $"{month}/{day}, Year {year}";
            timeDisplay.text = $"{hour}:{Math.Round((double)minute, 0)}";
        }
    }

    public short Minute
    {
        get;
    }

    public short Hour
    {
        get;
    }

    public short Day
    {
        get;
    }

    public short Month
    {
        get;
    }

    public short Year
    {
        get;
    }
}
