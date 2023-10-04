using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleScript : MonoBehaviour
{
    public Slider timeScaleSlider;
    public TMPro.TMP_Text timeScaleText;


    void Start()
    {
        timeScaleSlider.value = 1;
    }
    void Update()
    {
        Time.timeScale = timeScaleSlider.value;
        timeScaleText.text = timeScaleSlider.value.ToString("F1");
    }
}
