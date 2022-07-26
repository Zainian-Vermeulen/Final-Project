using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the prefabs in the scenes.
/// It spawns the prefabs at specific times and then play the appropriate animations.
/// </summary>

public class PrefabHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab, _enemyPrefab;

    public GameObject enemyInSceneGO;
    public GameObject bulletInSceneGO;

    [SerializeField]
    private Enemy _hitTarget;

    [SerializeField]
    private Math _mathCorrect;

    [SerializeField]
    private AudioSource _enemyDie, _shoot;

    [SerializeField]
    private Animator _enemyAnimator, _playerAnimator;

    [SerializeField]
    private Transform _enemySpawnTransform;

    public event System.Action shootRn, moveEnemy;

    public bool isSpawnPrefabs = false;
   
    private int currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        Instantiate(_bulletPrefab, new Vector2(0.08f, 0.7f), Quaternion.Euler(0f, 0f, -90f));
        Instantiate(_enemyPrefab, _enemySpawnTransform);
        
        FindObjects();

        _enemyAnimator = enemyInSceneGO.GetComponent<Animator>();

        _mathCorrect.MathCorrect += PlayerAnim;
        _hitTarget.targetHit += DestroyPrefabs;

        if (_shoot == null) {
            return;
        }

        if (_enemyDie == null) {
            return;
        }
    }

    void Update()
    {
        StartCoroutine(SpawnPrefabs());
    }

    private void DestroyPrefabs()
    {
        StartCoroutine(IDestroyPrefabs());
    }

    private IEnumerator IDestroyPrefabs()
    {
        _enemyDie.Play();
        _enemyAnimator.SetTrigger("Die");
       
        Destroy(bulletInSceneGO);
        
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(enemyInSceneGO);
        
        isSpawnPrefabs = true;
    }


    public void PlayerAnim()
    {
        StartCoroutine(IWaitForAttack());
    }


    private IEnumerator IWaitForAttack()
    {
        if (currentScene == 1) {
            if (_playerAnimator != null) {
                _playerAnimator.SetTrigger("Attack");
                
                yield return new WaitForSecondsRealtime(0.9f);
               
                _playerAnimator.SetTrigger("Idle");
                
                _shoot.Play();
                shootRn?.Invoke();
 
            }
            else
               yield return null;
        }
    }

    private IEnumerator SpawnPrefabs()
    {
        if (isSpawnPrefabs) {
            isSpawnPrefabs = false;

            yield return new WaitForSecondsRealtime(0.25f);
            
            Instantiate(_bulletPrefab, new Vector2(0.15f, 0.7f), Quaternion.Euler(0f, 0f, -90f));
            Instantiate(_enemyPrefab, _enemySpawnTransform);
           
            FindObjects();

            _enemyAnimator.Rebind();
            moveEnemy?.Invoke();
        }
        else
            yield break; 
    }

    private void FindObjects()
    {
        bulletInSceneGO = GameObject.FindGameObjectWithTag("Bullet");
        enemyInSceneGO = GameObject.FindGameObjectWithTag("Enemy");
       
        _enemyAnimator = enemyInSceneGO.GetComponent<Animator>();

        _hitTarget = FindObjectOfType<Enemy>();
        _hitTarget.targetHit += DestroyPrefabs;   
    }
}
