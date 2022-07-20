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
    private GameObject _bulletPrefab;


    private void Start()
    {

        _mathScript = FindObjectOfType<Math>();
        if (_mathScript == null)
            return;


        rb = gameObject.GetComponent<Rigidbody2D>();
        _mathScript.shootEvent += Shootnow;
       //Instantiate(_bulletPrefab, new Vector2(0f, 0.3f), Quaternion.identity);
    }

    public void OnBtnClick()
    {
        Shootnow();
    }

    private void OnDestroy()
    {
        _mathScript.shootEvent -= Shootnow;
    }

    private void FixedUpdate()
    {
        _mathScript = FindObjectOfType<Math>();
        if (_mathScript == null)
        Debug.Log("Math script is: " + _mathScript);
        else
            return;

        _mathScript.shootEvent += Shootnow;
    }

    public void Shootnow()
    {
        _mathScript = FindObjectOfType<Math>();
        rb.velocity = transform.up * /*Time.deltaTime **/ 15f;      
    }
}

