using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject overPanel;

    [SerializeField] private Color whileColor;

    [Header("Dot Level")] 
    [SerializeField] private GameObject panelDotLevel;
    [SerializeField] private Color doneColor;
    [SerializeField] private Color bossColor;

    [Header("Knife Count Display")] 
    [SerializeField] private GameObject panelKnives;
    [SerializeField] private GameObject iconKnife;
    [SerializeField] private Color usedKnifeIconColor;

    public void ShowOverPanel()
    {
        overPanel.SetActive(true);
    }

    public void SetInitialDispalyedKnifeCount(int count) 
    {
        for(int i=0;i<count;i++) 
        {
            Instantiate(iconKnife,panelKnives.transform);
        }
    }

    private int knifeIconIndexToChange = 0;
    
    public void DecrementDisplayedKnifeIcount() 
    {
        panelKnives.transform.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
    }

    public void doneDot(int dot)
    {
        for(int i=0;i<=dot;i++)
        panelDotLevel.transform.GetChild(i).GetComponent<Image>().color = doneColor;
    }

    public void bossDot()
    {
        panelDotLevel.transform.GetChild(0).gameObject.SetActive(false);
        panelDotLevel.transform.GetChild(1).gameObject.SetActive(false);
        panelDotLevel.transform.GetChild(2).gameObject.SetActive(false);
        panelDotLevel.transform.GetChild(3).GetComponent<Image>().color = bossColor;
    }

    public void Restart()
    {
          Time.timeScale = 1;
         SceneManager.LoadSceneAsync(1);
    }
    
    public void Home()
    {
         Time.timeScale = 1;
         SceneManager.LoadSceneAsync(0);
    }

   

}
