using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the shooting the player does when a question is answered correctly.
/// </summary>

public class Shoot : MonoBehaviour
{

    [SerializeField]
    private PrefabHandler _prefabHandler;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private SpriteRenderer _bullet;

    private GameObject _bulletGO;

    private Rigidbody2D rb;

    private void Start()
    {
       FindGameObj();
        
        if (_prefabHandler == null) {
            return;
        }

        _bullet.enabled = false;

        rb = gameObject.GetComponent<Rigidbody2D>();
        _prefabHandler.shootRn += Shootnow;
    }


    private void OnDestroy()
    {
        _prefabHandler.shootRn -= Shootnow;
    }

    public void Shootnow()
    {
        FindGameObj();
        _bullet.enabled = true;
       
        rb.velocity = transform.up * 15f;      
    }

    private void FindGameObj()
    {
        _prefabHandler = FindObjectOfType<PrefabHandler>();
        _bulletGO = GameObject.FindGameObjectWithTag("Bullet");
        _bullet = _bulletGO.GetComponent<SpriteRenderer>();
        
    }
}

