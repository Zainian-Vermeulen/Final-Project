using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Shoot : MonoBehaviour
{

    
    private Rigidbody2D rb;

    private SceneManager sceneManager;

    private Math _mathScript;

    [SerializeField]
    private PrefabHandler _prefabHandler;

    [SerializeField]
    private GameObject _bulletPrefab;


    [SerializeField]
    private SpriteRenderer _bullet;

    private GameObject _bulletGO;

    private void Start()
    {
        

        _bulletGO = GameObject.FindGameObjectWithTag("Bullet");
        _bullet = _bulletGO.GetComponent<SpriteRenderer>();
        _bullet.enabled = false;
        _mathScript = FindObjectOfType<Math>();
        if (_mathScript == null)
            return;

        _prefabHandler = FindObjectOfType<PrefabHandler>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        _prefabHandler.shootEvent += Shootnow;
       // _mathScript.shootEvent += Shootnow;
       //Instantiate(_bulletPrefab, new Vector2(0f, 0.3f), Quaternion.identity);
    }

    public void OnBtnClick()
    {
        //Shootnow();
    }

    private void OnDestroy()
    {
        _mathScript.shootEvent -= Shootnow;
    }

    private void FixedUpdate()
    {
        //_mathScript = FindObjectOfType<Math>();
        //if (_mathScript == null)
        //Debug.Log("Math script is: " + _mathScript);
        //else
        //    return;
        _prefabHandler = FindObjectOfType<PrefabHandler>();
        _prefabHandler.shootEvent += Shootnow;
        _bulletGO = GameObject.FindGameObjectWithTag("Bullet");
        _bullet = _bulletGO.GetComponent<SpriteRenderer>();
        //_mathScript.shootEvent += Shootnow;
    }

    public void Shootnow()
    {
        // _bullet = this.GetComponent<SpriteRenderer>();
        _prefabHandler.shootEvent += Shootnow;
        _prefabHandler = FindObjectOfType<PrefabHandler>();
        _bulletGO = GameObject.FindGameObjectWithTag("Bullet");
        _bullet = _bulletGO.GetComponent<SpriteRenderer>();
        _bullet.enabled = true;
        _mathScript = FindObjectOfType<Math>();
        rb.velocity = transform.up * /*Time.deltaTime **/ 15f;      
    }
}

