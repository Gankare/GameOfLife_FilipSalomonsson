using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScaleScript : MonoBehaviour
{
    public Slider timeScaleSlider;

    void Start()
    {
        timeScaleSlider.value = 1;
    }
    void Update()
    {
        Time.timeScale = timeScaleSlider.value;
        Debug.Log(Time.timeScale);
    }
}
