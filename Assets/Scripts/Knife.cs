using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    [Header("Model")]
    [SerializeField] List<Sprite> knives = new List<Sprite>();

     [Header("Throw")]
    [SerializeField] private Vector2 throwForce;
    
    private bool isActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;
    private SpriteRenderer knifeSpriteRenderer;
    

    private void Awake() 
    {   
        int index = PlayerPrefs.GetInt("Knives",0);
        knifeSpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
        knifeSpriteRenderer.sprite = knives[index];
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0) && isActive ) 
        {
            rb.AddForce(throwForce,ForceMode2D.Impulse);
            rb.gravityScale = 1;
            //TO DO: Decrement number of available knives
            GameController.Instance.GameUI.DecrementDisplayedKnifeIcount();
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!isActive) return;
        isActive = false;
        if(other.collider.tag == "Log" )
        {
            GameController.Instance.hit.Play();
            GetComponent<ParticleSystem>().Play();
            //TO DO: FLASH LOG
            GameController.Instance.Flash();
            rb.velocity = new Vector2(0,0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(other.collider.transform);

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.45f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.05f);

            //TO DO: Spawn another knife
            GameController.Instance.SetScore(1);
            GameController.SaveHighScore();
            GameController.SaveScore();
            GameController.Instance.OnSuccessfulKnifeHit();
        }
        else if(other.collider.tag == "Knife") 
        {
            GameController.Instance.fail.Play();
            rb.velocity = new Vector2(rb.velocity.x,-2f);
            GameController.SaveHighScore();
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
