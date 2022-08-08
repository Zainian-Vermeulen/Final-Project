using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script handles the player movement in the Plus | Minus scene
/// </summary>
/// 
public class Player : MonoBehaviour
{
    public event System.Action GameOver, GameWon;
    
    private Animator _playerAnimator;

    private GameObject gameWon, enemy;

    private Rigidbody2D rb;

    private Math _mathScript;
    //private GameObject enemy;

   // public event System.Action GameWon;



    void Start()
    {
        _mathScript = FindObjectOfType<Math>();
        _mathScript.MathCorrect += StartMovingPlayer;

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameWon = GameObject.FindGameObjectWithTag("GameOver");

        rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }



    private void StartMovingPlayer()
    {
        StartCoroutine(MovePlayer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameWon != null) {
            if (gameWon.CompareTag(other.tag)) {
                GameWon?.Invoke();
            }
        }

        if (enemy != null) {
            if (enemy.CompareTag(other.tag)) {
                GameOver?.Invoke();
            }
        }
    }

    //Method to play animation and to move the player
    private IEnumerator MovePlayer()
    {
        _playerAnimator.SetTrigger("Running");
       
        rb.velocity = -transform.right * 0.9f;
        
        _playerAnimator.SetTrigger("Idle");
        
        yield return new WaitForSecondsRealtime(1.2f);
        rb.velocity = -transform.right * 0;
    }
}
