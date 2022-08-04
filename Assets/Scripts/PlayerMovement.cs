using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Math _mathScript;
    private Rigidbody2D rb;

    private GameObject gameWon;
    private GameObject enemy;

    public event System.Action GameOver;
    public event System.Action GameWon;
    
    private Animator _playerAnimator;
    //make player anim like enemy

    // Start is called before the first frame update
    void Start()
    {
        _mathScript = FindObjectOfType<Math>();
        _mathScript.MathCorrect += StartMovingPlayer;

        rb = GetComponent<Rigidbody2D>();

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        gameWon = GameObject.FindGameObjectWithTag("GameOver");


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartMovingPlayer()
    {
        StartCoroutine(MovePlayer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameWon != null)
        {
            if (gameWon.CompareTag(other.tag))
            {
                Debug.Log("Game is won");
                
                GameWon?.Invoke();

            }

        }

        if (enemy != null)
        {
            if (enemy.CompareTag(other.tag))
            {
                Debug.Log("Game Over");
                GameOver?.Invoke();

            }
        }
        
    }


    private IEnumerator MovePlayer()
    {
        rb.velocity = -transform.right * /*Time.deltaTime **/ 0.9f;
        yield return new WaitForSecondsRealtime(0.9f);
        rb.velocity = -transform.right * 0;
        Debug.Log("Player moving now");
    }
}
