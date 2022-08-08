using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is on the enemy prefab and it handles the movement and collision of the enemy.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOver;

    public GameObject _bulletPrefab;
    public GameObject _player;

    public event System.Action targetHit;
    public event System.Action GameOver;

    private Rigidbody2D rb;
    private int currentScene;

    private float speed;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        gameOver = GameObject.FindGameObjectWithTag("GameOver");
       
        rb = GetComponent<Rigidbody2D>();
        
        ApplyMovement();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _bulletPrefab = GameObject.FindGameObjectWithTag("Bullet");
        gameOver = GameObject.FindGameObjectWithTag("GameOver");

        if (gameOver.CompareTag(other.tag)) {        
            GameOver?.Invoke();            
        }

        if (_bulletPrefab != null) {
            if (_bulletPrefab.CompareTag(other.tag)) {
                targetHit?.Invoke();
            }            
        }
        else
           return;      
    }

    private void Update()
    {
        ApplyMovement();     
    }

    private void ApplyMovement()
    {
         currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 2) {
            speed = 0.5f;
        }
        else {
            speed = 2.0f;
        }

        rb.velocity = -transform.right * speed;
    }
}
