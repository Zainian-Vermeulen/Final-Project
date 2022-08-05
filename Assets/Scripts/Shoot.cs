using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Shoot : MonoBehaviour
{

    
    private Rigidbody2D rb;

    private SceneManager sceneManager;

   

    [SerializeField]
    private PrefabHandler _prefabHandler;

    [SerializeField]
    private GameObject _bulletPrefab;


    [SerializeField]
    private SpriteRenderer _bullet;

    private GameObject _bulletGO;

    private void Start()
    {
        

       FindGameObj();
        
        _bullet.enabled = false;
       

        
        if (_prefabHandler == null)
            return;

        rb = gameObject.GetComponent<Rigidbody2D>();
        _prefabHandler.shootRn += Shootnow;

    }

    public void OnBtnClick()
    {
        //Shootnow();
    }

    private void OnDestroy()
    {
        _prefabHandler.shootRn -= Shootnow;
    }

    private void FixedUpdate()
    {

       
    }

    public void Shootnow()
    {
        FindGameObj();
        _bullet.enabled = true;
       
        rb.velocity = transform.up * /*Time.deltaTime **/ 15f;      
    }

    private void FindGameObj()
    {
        _prefabHandler = FindObjectOfType<PrefabHandler>();
        _bulletGO = GameObject.FindGameObjectWithTag("Bullet");
        _bullet = _bulletGO.GetComponent<SpriteRenderer>();
        
    }
}

