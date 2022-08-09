using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// This script handles the UI the player seens in the different scenes.
/// </summary>
public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private Button _resumeBtn;

    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private Image _backgroundImg, _playerLife1Img, _playerLife2Img, _playerLife3Img;

    [SerializeField]
    private TMP_Text _gameWon, _questions;

    [SerializeField]
    private Math _math;
   
    [SerializeField]
    private AudioSource _winGame, _loseGame, _enemyDie, _click, _clickBack;

    [SerializeField]
    private Animator _enemyAnimator;

    [SerializeField]
    private PrefabHandler prefabHandler;

    private Enemy _enemy;
    [SerializeField]
    private Player _playerMovement;

    private int currentScene, questionsCorrect = 0, questionsIncorrect = 0, maxQuestions = 10;
    private bool isGameWon = false;

    private void Start()
    {
        PauseGame(false);
        currentScene = SceneManager.GetActiveScene().buildIndex;

        _math = FindObjectOfType<Math>();
        _math.MathIncorrect += OnMathIncorrect;
        _math.MathCorrect += OnMathCorrect;

        _resumeBtn.gameObject.SetActive(false);
        _backgroundImg.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);

        _enemy = FindObjectOfType<Enemy>();

        if (_playerMovement != null)
        {

            _playerMovement.GameWon += OnGameWon;
            _playerMovement.GameOver += OnGameOver;
        }
        else
            return;


        if (_enemy == null) {
            return;
        } 
        
        _enemyAnimator = _enemy.GetComponent<Animator>();


        if (currentScene == 1) {
            _playerLife1Img.gameObject.SetActive(true);
            _playerLife2Img.gameObject.SetActive(true);
            _playerLife3Img.gameObject.SetActive(true);
        }
        else
            return;


      // _playerMovement = FindObjectOfType<Player>();

       
           



        if (_winGame == null) { 
            return; 
        }

        if (_enemyDie == null) { 
            return;
        }

        if (_loseGame == null) {
            return;
        }

        if (_click == null) {
            return;
        }

        if (_clickBack == null) {
            return;
        }
    }

    private void FixedUpdate()
    {
        _enemy = FindObjectOfType<Enemy>();

        if (_enemy != null) {
            _enemy.GameOver += OnGameOver;
        }
        else
            return;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            OnMenuClick(); 
        }

        if (_enemy == null) {
            return;
        }

        _enemyAnimator = _enemy.GetComponent<Animator>();
    }

    private void OnMathIncorrect()
    {
        questionsIncorrect += 1;

        if (questionsIncorrect == 0) {
            _playerLife1Img.gameObject.SetActive(true); 
            _playerLife2Img.gameObject.SetActive(true);
            _playerLife3Img.gameObject.SetActive(true);
        }

        if (questionsIncorrect == 1) {
            _playerLife3Img.gameObject.SetActive(false);
        }

        if (questionsIncorrect == 2) {
            _playerLife2Img.gameObject.SetActive(false);
        }

        if (questionsIncorrect == 3) {
            _playerLife1Img.gameObject.SetActive(false);
            QuestionsGameOver();
        }
    }

    private void OnMathCorrect()
    {
        questionsCorrect += 1;

        if (_questions != null) {
            _questions.text = $"{questionsCorrect} / {maxQuestions}";
        }

        if (questionsCorrect == maxQuestions) {
            OnGameWon();
        }
    }
 
    private void QuestionsGameOver()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        
        if (currentScene == 1) {
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
        isGameWon = true;
        
        StartCoroutine(IWaitForAnim("Die", "You Won!"));
    }

    private void OnGameOver()
    {
        isGameWon = false;
        
        StartCoroutine(IWaitForAnim("Eat", "Game Over!"));
    }

    //Wait for animations to complete before proceding to win / lose
    private IEnumerator IWaitForAnim(string x, string y)
    {
        //Lose
        if (!isGameWon) {
            _gameWon.text = $"{y}";
            _gameOver.gameObject.SetActive(true);
            _enemyAnimator.SetTrigger($"{x}");
           
            _enemyDie.Play();
            _loseGame.Play();
           
            PauseGame(true);
        }
        
        //Win
        if (currentScene == 2) {
            _gameWon.text = $"{y}";
            _gameOver.gameObject.SetActive(true);
            _enemyAnimator.SetTrigger($"{x}");
           
            _enemyDie.Play();
            _winGame.Play();
           
            PauseGame(true);
        }
        else if (prefabHandler.isSpawnPrefabs == false) {
           
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
        if (b) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
}
