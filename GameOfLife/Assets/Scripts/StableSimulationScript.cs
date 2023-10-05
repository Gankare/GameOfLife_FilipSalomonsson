using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StableSimulationScript : MonoBehaviour
{
    public Image fade;
    public GameObject simStableMenu;
    public GameObject timeSlider;
    public TMPro.TMP_Text generationText;
    void Start()
    {
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
        StartCoroutine(MenuAfterTime());
    }
    IEnumerator MenuAfterTime()
    {
        fade.CrossFadeAlpha(255, 1, true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
