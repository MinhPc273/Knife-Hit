using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public GameUI GameUI {get ; private set ; }
    public static GameController Instance {get ; private set; }

    private int knifeCount;

    [Header("Sounds")]
    public AudioSource brek;
    public AudioSource hit;
    public AudioSource fail;

    [Header("Score")]
    private static int score = 0;
    private static int highScore;

    private static int dot = 0;

    [SerializeField] private TMP_Text Ingame_text_score;
    [SerializeField] private TMP_Text text_score;
    [SerializeField] private TMP_Text text_highScore;
   
    [Header("Knife Spawning")]
    [SerializeField] private Vector2 knifeSpawnPosition;
    [SerializeField] private GameObject knifeObject;

    [Header("Log Flash")]
    [SerializeField] private GameObject logMotorObject;
    [SerializeField] private GameObject logObject;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material flashMaterial;
    [SerializeField] Coroutine flashRoutine;

    private void Awake()
    {
        LoadScore();
        LoadDot();
        LoadHighScore();  
        Instance = this; 
        knifeCount = (int)Random.Range(6,8);
        GameUI = GetComponent<GameUI>();
        Ingame_text_score.text = "" + GetScore();
        if(dot!=3) GameUI.doneDot(dot);
        else GameUI.bossDot();
    }

    private void Start() 
    {  
        animator = logMotorObject.GetComponent<Animator>();
        GameUI.SetInitialDispalyedKnifeCount(knifeCount);
        SpawnKnife();
        spriteRenderer = logObject.GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        animator.SetBool("Hit",true);
        yield return new WaitForSecondsRealtime(0.02f);
        animator.SetBool("Hit",false);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }

    public void Flash()
    {
        if(flashRoutine != null) StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public void OnSuccessfulKnifeHit() 
    {
         Ingame_text_score.text = "" + GetScore();
        if(knifeCount>0) 
        {
            SpawnKnife();
            
        }
        else 
        {
            StartGameOverSequence(true);
        }
    }

    private void SpawnKnife() 
    {
        knifeCount--;
        Instantiate(knifeObject,knifeSpawnPosition,Quaternion.identity);
    }

    public void StartGameOverSequence(bool win) 
    {
        StartCoroutine("GameOverSequenceCoroutine",win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if(win) 
        {
            //brek.Play();
            yield return new WaitForSecondsRealtime(0.3f);
            //TO DO:NEXT LEVEL
            int Level = SceneManager.GetActiveScene().buildIndex + (int)Random.Range(1,3);
            if(Level>5) Level-=5;
           SetDot();
           SaveDot();
           SceneManager.LoadSceneAsync(Level);
        }
        else 
        {
            text_score.text = "Score: " + GetScore();
            text_highScore.text = "HighScore: " + GetHighScore();
            GameUI.ShowOverPanel();
            ResetScore();
            SaveScore();
            SaveDot();
        }
    }

    public void RestartGame()
    {
         SceneManager.LoadSceneAsync(1);
    }

      public void SetScore(int newScore)
    {
        score += newScore;
        if (score > highScore) highScore = score;
    }

    public void ResetScore()
    {
        score = 0;
        dot = 0;
    }

    public int GetScore()
    {
        return score;
    }

     public static int GetHighScore()
    {
        return highScore;
    }

    public static void SaveHighScore()
    {
            PlayerPrefs.SetInt("HighScore",highScore);
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore",0);
    }

    public static void SaveScore()
    {
            PlayerPrefs.SetInt("Score",score);
    }

    void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score",score);
    }

    public static void SaveDot()
    {
            PlayerPrefs.SetInt("Dot",dot);
    }

    void LoadDot()
    {
        dot = PlayerPrefs.GetInt("Dot",dot);
    }

       public void SetDot()
    {
        dot += 1;
        if (dot > 3) dot = 0;
    }

}
