using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public GameObject _bulletPrefab;
    public event System.Action targetHit;

    private Rigidbody2D rb;

    [SerializeField]
    private PrefabHandler prefabHandler;

    public GameObject _player;

    [SerializeField]
    private GameObject gameOver;

    public event System.Action GameOver;

    private SceneManager sceneManager;
    private int currentScene;

    [SerializeField]
    private Animator _enemyAnimator;

    

    private void Start()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;
        _enemyAnimator = GetComponent<Animator>();


        if (currentScene == 2)
        {
            prefabHandler = FindObjectOfType<PrefabHandler>();
        }

       

        gameOver = GameObject.FindGameObjectWithTag("GameOver");
       
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * /*Time.deltaTime **/ 15f;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _bulletPrefab = GameObject.FindGameObjectWithTag("Bullet");
        
        

        gameOver = GameObject.FindGameObjectWithTag("GameOver");

        if (gameOver.CompareTag(other.tag))
        {
            _enemyAnimator.SetTrigger("Eat");
            Debug.Log("Game Over");
         
            GameOver?.Invoke();
            
        }

        if (_bulletPrefab != null)
        {
            if (_bulletPrefab.CompareTag(other.tag))
            {
                //_enemyAnimator.SetTrigger("Die");

                targetHit?.Invoke();
            }            
        }
           return;      
    }

    private void Update()
    {

        ApplyMovement();

        
    }

    private void ApplyMovement()
    {
         currentScene = SceneManager.GetActiveScene().buildIndex;
        

        if (currentScene == 2)
        {
            rb.velocity = -transform.right * /*Time.deltaTime **/ 0.5f;
        }
        else  
        rb.velocity = -transform.right * /*Time.deltaTime **/ 2f;
    }

}
