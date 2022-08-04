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

    [SerializeField]
    private TMP_Text _gameWon;

    private int currentScene;

    [SerializeField]
    private Math _math;

    private int questionsCorrect = 0,
    questionsIncorrect = 0;

    [SerializeField]
    private Animator _enemyAnimator;


    
    private GameObject l;

    private bool isGameOver = false;

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
        _enemyAnimator = FindObjectOfType<Animator>();
        _gameOver.gameObject.SetActive(false);

        _playerLife1Img.gameObject.SetActive(true);
        _playerLife2Img.gameObject.SetActive(true);
        _playerLife3Img.gameObject.SetActive(true);

        _playerMovement = FindObjectOfType<PlayerMovement>();
        if (_playerMovement != null)
        {
            _playerMovement.GameWon += OnGameWon;
            _playerMovement.GameOver += OnGameOver;
        }
        else
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

         _enemyAnimator = FindObjectOfType<Animator>();
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
        if (questionsCorrect == 10)
        {
           // _gameWon.text = "You Won!";
            QuestionsGameOver();

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

    private void OnGameWon()
    {
        StartCoroutine(IWaitForAnim("Die", "Won"));
    }

    private void OnGameOver()
    {
        StartCoroutine(IWaitForAnim("Eat", "Over"));
    }


    private IEnumerator IWaitForAnim(string x, string y)
    {
        _enemyAnimator.SetTrigger($"{x}Trigger");
        yield return new WaitForSecondsRealtime(0.7f);
        _gameWon.text = $"You {y}!";
        _gameOver.gameObject.SetActive(true);
        PauseGame(true); 
    }



    public void OnRestartClick()
    {       
        SceneManager.LoadScene(currentScene);
    }

    public void OnMainMenuClick()
    {
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
