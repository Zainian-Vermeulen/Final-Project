using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Enemy _hitTarget;

    [SerializeField]
    private Math _mathCorrect;

    public GameObject enemyInScene;
    public GameObject bulletInScene;

    private bool isSpawnPrefabs = false;

    public System.Action moveEnemy;

    [SerializeField]
    private Transform _enemySpawnTransform;

    [SerializeField]
    private Animator _enemyAnimator;

    [SerializeField]
    private Animator _playerAnimator;

    private int currentScene;

    public event System.Action shootEvent;

    void Start()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;

        

        Instantiate(_bulletPrefab, new Vector2(0.08f, 0.7f), Quaternion.Euler(0f, 0f, -90f));
        Instantiate(_enemyPrefab, _enemySpawnTransform);
        FindObjects();

        _enemyAnimator = enemyInScene.GetComponent<Animator>();


        _mathCorrect.MathCorrect += PlayerAnim;
        _hitTarget.targetHit += DestroyPrefabs;

        

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
        _enemyAnimator.SetTrigger("Die");

        Destroy(bulletInScene);
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(enemyInScene);
        isSpawnPrefabs = true;
    }


    public void PlayerAnim()
    {
        StartCoroutine(IWaitForAttack());
    }


    private IEnumerator IWaitForAttack()
    {
        if (currentScene == 1)
        {
            if (_playerAnimator != null)
            {
                _playerAnimator.SetTrigger("Attack");
                yield return new WaitForSecondsRealtime(0.9f);
                _playerAnimator.SetTrigger("Idle");
                shootEvent?.Invoke();
            }
            else
               yield return null;
        }

        
    }

    private IEnumerator SpawnPrefabs()
    {
        if (isSpawnPrefabs)
        {
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
        _hitTarget = FindObjectOfType<Enemy>();
        enemyInScene = GameObject.FindGameObjectWithTag("Enemy");
        bulletInScene = GameObject.FindGameObjectWithTag("Bullet");
        _enemyAnimator = enemyInScene.GetComponent<Animator>();
        _hitTarget.targetHit += DestroyPrefabs;

        
    }
}
