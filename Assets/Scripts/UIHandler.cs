using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private Button _resumeBtn;

    [SerializeField]
    private Image _backgroundImg;

    [SerializeField]
    private Image _playerLife1Img;
    
    [SerializeField]
    private Image _playerLife2Img; 
    
    [SerializeField]
    private Image _playerLife3Img;

    //[SerializeField]
    private Enemy _enemy;

    private Player _playerMovement;

    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private GameObject _enemyGO;

    [SerializeField]
    private TMP_Text _gameWon;

    [SerializeField]
    private TMP_Text _questions;

    private int currentScene;

    [SerializeField]
    private Math _math;

    [SerializeField]
    private int questionsCorrect = 0,
    questionsIncorrect = 0, maxQuestions = 10;

    [SerializeField]
    private Animator _enemyAnimator;

    private bool isGameOver = false;
    private bool isGameWon = false;

    [SerializeField]
    private AudioSource _winGame;

    [SerializeField]
    private AudioSource _loseGame;

    [SerializeField]
    private AudioSource _enemyDie;
   
    [SerializeField]
    private AudioSource _click;

    [SerializeField]
    private AudioSource _clickBack;

    [SerializeField]
    private PrefabHandler prefabHandler;

    private void Start()
    {


        currentScene = SceneManager.GetActiveScene().buildIndex;

        PauseGame(false);
        _math = FindObjectOfType<Math>();
        _math.MathIncorrect += OnMathIncorrect;
        _math.MathCorrect += OnMathCorrect;

        _resumeBtn.gameObject.SetActive(false);
        _backgroundImg.gameObject.SetActive(false);

        _enemy = FindObjectOfType<Enemy>();
        //_enemyAnimator = _enemy.GetComponentInChildren<Animator>();


        if (_enemy == null)
        {
            return;
        }
         
        
       // _enemyAnimator = FindObjectOfType<Animator>();
       //_enemyAnimator = _enemyGO.GetComponent<Animator>();
        _enemyAnimator = _enemy.GetComponent<Animator>();
       
        _gameOver.gameObject.SetActive(false);

        if (currentScene == 1)
        {
            _playerLife1Img.gameObject.SetActive(true);
            _playerLife2Img.gameObject.SetActive(true);
            _playerLife3Img.gameObject.SetActive(true);
        }


        _playerMovement = FindObjectOfType<Player>();
        if (_playerMovement != null)
        {
            _playerMovement.GameWon += OnGameWon;
            _playerMovement.GameOver += OnGameOver;
        }
        else
            return;



        if (_winGame == null)
            return;

        if (_enemyDie == null)
            return;
        
        if (_loseGame == null)
            return;

        if (_click == null)
            return;

        if (_clickBack == null)
            return;
    }

    private void FixedUpdate()
    {
        _enemy = FindObjectOfType<Enemy>();

        if (_enemy != null)
        _enemy.GameOver += OnGameOver;
        else
            return;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
            OnMenuClick();

        if (_enemy == null)
        {
            return;
        }

        _enemyAnimator = _enemy.GetComponent<Animator>();

    }

    private void OnMathIncorrect()
    {

        questionsIncorrect += 1;

        if (questionsIncorrect == 0)
        {
            _playerLife1Img.gameObject.SetActive(true); 
            _playerLife2Img.gameObject.SetActive(true);
            _playerLife3Img.gameObject.SetActive(true);
        }

        if (questionsIncorrect == 1)
        {
            _playerLife3Img.gameObject.SetActive(false);
        }

        if (questionsIncorrect == 2)
        {
            _playerLife2Img.gameObject.SetActive(false);
        }

        if (questionsIncorrect == 3)
        {
            _playerLife1Img.gameObject.SetActive(false);
            
            QuestionsGameOver();
        }



        Debug.Log("Questions incorrect is: " + questionsIncorrect);
        
    }

    private void OnMathCorrect()
    {
        questionsCorrect += 1;
        
        if (_questions != null)
            _questions.text = $"{questionsCorrect} / {maxQuestions}";
        

        if (questionsCorrect == maxQuestions)
        {
            OnGameWon();

        }
        Debug.Log("Questions correct is: " + questionsCorrect);
        
    }

   
    private void QuestionsGameOver()
    {
        //_gameWon.text = "Game Over!";
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 1)
        {
            OnGameOver();
        }
    }
    
    public void OnMenuClick()
    {
        _click.Play(); 
        _resumeBtn.gameObject.SetActive(true);
        _backgroundImg.gameObject.SetActive(true);
        PauseGame(true);
    }

    public void OnResumeClick()
    {
        _clickBack.Play();
        _resumeBtn.gameObject.SetActive(false);
        _backgroundImg.gameObject.SetActive(false);
        PauseGame(false);
    }

    private void OnGameWon()
    {
        Debug.Log("Game won!");
        isGameWon = true;
        
        StartCoroutine(IWaitForAnim("Die", "You Won!"));
        //_enemyDie.Play();
    }

    private void OnGameOver()
    {
        Debug.Log("Game lost!");

        isGameWon = false;
        StartCoroutine(IWaitForAnim("Eat", "Game Over!"));
    }


    private IEnumerator IWaitForAnim(string x, string y)
    {
        
        //_gameWon.text = $"{y}";
        //_gameOver.gameObject.SetActive(true);
        //_enemyAnimator.SetTrigger($"{x}");

        //lose
        if (!isGameWon)
        {
            _gameWon.text = $"{y}";
            _gameOver.gameObject.SetActive(true);
            _enemyAnimator.SetTrigger($"{x}");
            _enemyDie.Play();
            _loseGame.Play();
            PauseGame(true);
        }
        
        //win
        if (currentScene == 2)
        {
            _gameWon.text = $"{y}";
            _gameOver.gameObject.SetActive(true);
            _enemyAnimator.SetTrigger($"{x}");
            _enemyDie.Play();
            _winGame.Play();
            PauseGame(true);
        }
        else if (prefabHandler.isSpawnPrefabs == false)
        {
            yield return new WaitForSecondsRealtime(2f);
            _gameWon.text = $"{y}";
            _gameOver.gameObject.SetActive(true);
            _enemyAnimator.SetTrigger($"{x}");
            _enemyDie.Play();
            _winGame.Play();
            PauseGame(true);
        }      
    }

    public void OnRestartClick()
    {
        _click.Play();
        SceneManager.LoadScene(currentScene);
    }

    public void OnMainMenuClick()
    {
        _clickBack.Play();
        SceneManager.LoadScene(0);
        PauseGame(false);
    }

    //i keep forgetting that i pause game somewhere, then my code won't work.
    // true = pause, false = unpause
    private void PauseGame(bool b)
    {
        if (b)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
