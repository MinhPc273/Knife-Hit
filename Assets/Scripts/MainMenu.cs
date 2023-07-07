using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text textHighScore;
    [SerializeField] private GameObject settings;
    private static int knive = 0;


   private void Start() {
        LoadKnives();
        textHighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore",0);
    }

    public void PlayGame() 
    {
        PlayerPrefs.SetInt("Score",0);
        PlayerPrefs.SetInt("Dot",0);
        SceneManager.LoadSceneAsync(1);
    }

    public void Settings() 
    {
        settings.SetActive(true);
    }

    public void Home() 
    {
        settings.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public static void Knive0()
    {
        knive = 0;
        SaveKnives();
    }
    public static void Knive1()
    {
        knive = 1;
        SaveKnives();
    }
    public static void Knive2()
    {
        knive = 2;
         SaveKnives();
    }
    public static void Knive3()
    {
        knive = 3;
         SaveKnives();
    }
    public static void Knive4()
    {
        knive = 4;
         SaveKnives();
    }
    public static void Knive5()
    {
        knive = 5;
         SaveKnives();
    }

    public static void SaveKnives()
    {
            PlayerPrefs.SetInt("Knives",knive);
    }

    void LoadKnives()
    {
        knive = PlayerPrefs.GetInt("Knives",knive);
    }

}
