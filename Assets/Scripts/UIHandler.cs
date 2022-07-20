using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    private SceneManager sceneManager;

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

    private PlayerMovement _playerMovement;

    [SerializeField]
    private GameObject _gameOver;

    private int currentScene;

    [SerializeField]
    private Math _math;

    private int questionsCorrect = 0,
    questionsIncorrect = 0;


    private void Start()
    {
        PauseGame(false);
        _math = FindObjectOfType<Math>();
        _math.MathIncorrect += OnMathIncorrect;
        _math.MathCorrect += OnMathCorrect;

        _resumeBtn.gameObject.SetActive(false);
        _backgroundImg.gameObject.SetActive(false);

        _enemy = FindObjectOfType<Enemy>();
        //_hitTarget.GameOver += OnGameOver;

        _gameOver.gameObject.SetActive(false);

        _playerLife1Img.gameObject.SetActive(true);
        _playerLife2Img.gameObject.SetActive(true);
        _playerLife3Img.gameObject.SetActive(true);

        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerMovement.GameOver += OnGameOver;


        currentScene = SceneManager.GetActiveScene().buildIndex;

    }

    private void FixedUpdate()
    {
        _enemy = FindObjectOfType<Enemy>();

        if (_enemy != null)
        _enemy.GameOver += OnGameOver;
        else
            return;


        QuestionsGameOver();
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
        }



        Debug.Log("Questions incorrect is: " + questionsIncorrect);
        
    }

    private void OnMathCorrect()
    {
        questionsCorrect += 1;
        Debug.Log("Questions correct is: " + questionsCorrect);

    }

    private void QuestionsGameOver()
    {
        if (currentScene == 1)
        {
            if (questionsCorrect == 10)
            {
                OnGameOver();
            }

            if (questionsIncorrect == 3)
            {
                OnGameOver();
            }
        }   
    }

    public void OnMenuClick()
    {
        _resumeBtn.gameObject.SetActive(true);
        _backgroundImg.gameObject.SetActive(true);
        PauseGame(true);
    }

    public void OnResumeClick()
    {
        _resumeBtn.gameObject.SetActive(false);
        _backgroundImg.gameObject.SetActive(false);
        PauseGame(false);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            OnMenuClick();
    }

    private void OnGameOver()
    {
        PauseGame(true);
        _gameOver.gameObject.SetActive(true);
    }


    public void OnRestartClick()
    {       
        SceneManager.LoadScene(currentScene);
    }

    public void OnMainMenuClick()
    {
        Debug.Log("I am here now");
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
