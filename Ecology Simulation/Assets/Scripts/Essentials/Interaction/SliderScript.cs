using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sliderContainer;
    private Slider slider;
    private TMP_Text valueText;
    // Start is called before the first frame update
    void Start()
    {
        if (sliderContainer == null)
        {
            Debug.LogWarning("SliderScript is not connected to any container.");
        }
        else
        {
            slider = sliderContainer.GetComponentInChildren<Slider>();
            valueText = sliderContainer.transform.Find("Value").GetComponent<TMP_Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slider == null)
        {
            Debug.Log("Slider is missing.");
        }
        if (valueText == null)
        {
            Debug.Log("Text is missing.");
        }
        valueText.text = $"{Math.Round(slider.value, 2)}";
    }
}
