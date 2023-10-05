using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StableSimulationScript : MonoBehaviour
{
    public GameObject simStableMenu;
    public GameObject timeSlider;
    public TMPro.TMP_Text generationText;
    void Start()
    {
        timeSlider.SetActive(true);
        simStableMenu.SetActive(false);
    }

    void Update()
    {
        if(LifeScript.simStable)
        {
            timeSlider.SetActive(false);
            simStableMenu.SetActive(true);
            Time.timeScale = 1;
            generationText.text = "It took " + LifeScript.generations.ToString() + " generations";
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
