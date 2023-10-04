using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    //ChangeScene
    public Image fade;
    //Spawn Chance
    public Slider spawnPrecentageSlider;
    public TMPro.TMP_Text spawnPrecentageText;
    public static int spawnPrecentage;
    //Camera Size
    public Slider cameraSizeSlider;
    public TMPro.TMP_Text cameraSizeText;
    public static float cameraSize;
    void Start()
    {
        //ChangeScene
        fade.CrossFadeAlpha(1, 0, true);
        //Spawn Chance
        spawnPrecentageSlider.value = 15;
        //Camera Size
        cameraSizeSlider.value = 5;
    }

    void Update()
    {
        //Spawn Chance
        spawnPrecentageText.text = spawnPrecentageSlider.value.ToString("F0");
        spawnPrecentage = Convert.ToInt32(spawnPrecentageSlider.value);
        //Camera Size
        cameraSizeText.text = cameraSizeSlider.value.ToString("F1");
        cameraSize = cameraSizeSlider.value;

    }
    //ChangeScene
    public void StartGame()
    {
        StartCoroutine(StartGameAfter());
    }
    IEnumerator StartGameAfter()
    {
        fade.CrossFadeAlpha(255, 1, true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
