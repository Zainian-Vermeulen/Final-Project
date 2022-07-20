using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Enemy _hitTarget;

    public GameObject enemyInScene;
    public GameObject bulletInScene;

    private bool isSpawnPrefabs = false;

    public System.Action moveEnemy;

    [SerializeField]
    private Transform _enemySpawnTransform;


    void Start()
    {
        
        Instantiate(_bulletPrefab, new Vector2(0.15f, 0.7f), Quaternion.Euler(0f, 0f, -90f));
        Instantiate(_enemyPrefab, _enemySpawnTransform);
        // Instantiate(_enemyPrefab,  Vector2.zero, Quaternion.identity);
        //Instantiate(_enemyPrefab, _enemySpawnTransform);
        

        // _hitTarget = FindObjectOfType<HitTarget>();

        FindObjects();
        _hitTarget.targetHit += DestroyPrefabs;

        //enemyInScene = GameObject.FindGameObjectWithTag("Bullet");
        //bulletInScene = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnPrefabs());
    }

    private void DestroyPrefabs()
    {
        Destroy(enemyInScene);
        Destroy(bulletInScene);
        isSpawnPrefabs = true;
    }




    private IEnumerator SpawnPrefabs()
    {
        if (isSpawnPrefabs)
        { 
            isSpawnPrefabs = false;
            yield return new WaitForSecondsRealtime(0.25f);
            Instantiate(_bulletPrefab, new Vector2(0.15f, 0.7f), Quaternion.Euler(0f, 0f, -90f));

            // Instantiate(_enemyPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
            Instantiate(_enemyPrefab, _enemySpawnTransform);


            moveEnemy?.Invoke();
            FindObjects();
        }
        else
            yield break; 
    }

    private void FindObjects()
    {
        _hitTarget = FindObjectOfType<Enemy>();
        enemyInScene = GameObject.FindGameObjectWithTag("Bullet");
        bulletInScene = GameObject.FindGameObjectWithTag("Enemy");

        _hitTarget.targetHit += DestroyPrefabs;

        
    }
}
