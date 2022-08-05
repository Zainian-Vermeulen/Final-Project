using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _playerHighScoreEasyEquations;

    [SerializeField]
    private TMP_Text _playerHighScoreMediumEquations; 
    
    [SerializeField]
    private TMP_Text _playerHighScoreHardEquations;

    [SerializeField]
    private TMP_Text _playerHighScoreEasyLinear;

    [SerializeField]
    private TMP_Text _playerHighScoreMediumLinear;

    [SerializeField]
    private TMP_Text _playerHighScoreHardLinear;


    private int currentScene;
    private int playerHighScoreEasyEquations, playerHighScoreMediumEquations, playerHighScoreHardEquations;
    private int playerHighScoreEasyLinear, playerHighScoreMediumLinear, playerHighScoreHardLinear;

    [SerializeField]
    private Button _equationsGameBtn;

    [SerializeField]
    private Button _playGameBtn;

    [SerializeField]
    private Button _backBtn;

    [SerializeField]
    private Button _quitBtn;

    [SerializeField]
    private Button _highScoreBtn;

    [SerializeField]
    private GameObject _menu1;

    [SerializeField]
    private GameObject _menu2;

    [SerializeField]
    private GameObject _menu3;
    
    [SerializeField]
    private GameObject _menu4;

    private string playerDif;

    [SerializeField]
    private AudioSource _click;

    [SerializeField]
    private AudioSource _clickBack;


    [SerializeField]
    private GameObject _difficulty;


    //[SerializeField]
    //private Button _otherGameBtn;


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        _menu1.SetActive(true);
        _menu2.SetActive(false);
        _menu3.SetActive(false);
        _menu4.SetActive(false);

        if (_playerHighScoreEasyEquations != null)
        {
            playerHighScoreEasyEquations = PlayerPrefs.GetInt("HighScore_Equations_Easy", 0);
            _playerHighScoreEasyEquations.text = $"High score is: {playerHighScoreEasyEquations}";
        }
        else
            return;

        if (_playerHighScoreMediumEquations != null)
        {
            playerHighScoreMediumEquations = PlayerPrefs.GetInt("HighScore_Equations_Medium", 0);
            _playerHighScoreMediumEquations.text = $"High score is: {playerHighScoreMediumEquations}";
        }
        else
            return;

        if (_playerHighScoreHardEquations != null)
        {
            playerHighScoreHardEquations = PlayerPrefs.GetInt("HighScore_Equations_Hard", 0);
            _playerHighScoreHardEquations.text = $"High score is: {playerHighScoreHardEquations}";
        }
        else
            return;


        if (_playerHighScoreEasyLinear != null)
        {
            playerHighScoreEasyLinear = PlayerPrefs.GetInt("HighScore_Linear_Easy", 0);
            _playerHighScoreEasyLinear.text = $"High score is: {playerHighScoreEasyLinear}";
        }
        else
            return;

        if (_playerHighScoreMediumLinear != null)
        {
            playerHighScoreMediumLinear = PlayerPrefs.GetInt("HighScore_Linear_Medium", 0);
            _playerHighScoreMediumLinear.text = $"High score is: {playerHighScoreMediumLinear}";
        }
        else
            return;

        if (_playerHighScoreHardLinear != null)
        {
            playerHighScoreHardLinear = PlayerPrefs.GetInt("HighScore_Linear_Hard", 0);
            _playerHighScoreHardLinear.text = $"High score is: {playerHighScoreHardLinear}";
        }
        else
            return;

        if (_click == null)
            return;

        if (_clickBack == null)
            return;

    }
   
    public void OnSelectGame()
    {
        _menu1.SetActive(false);
        _menu2.SetActive(true);
        _menu3.SetActive(false);
        _menu4.SetActive(false);
        _click.Play();
    }

    public void OnBackClick()
    {
        _clickBack.Play();
        if (_menu2.activeInHierarchy == true)
        {
            _menu1.SetActive(true);
            _menu2.SetActive(false);
        }
        else if (_menu3.activeInHierarchy == true)
        {
            _menu2.SetActive(true);
            _menu3.SetActive(false);

        }
        else if (_menu4.activeInHierarchy == true)
        {
            _menu2.SetActive(true);
            _menu4.SetActive(false);
        }
        else
            return;
    }

    public void OnEquationClick()
    {
        _menu1.SetActive(false);
        _menu2.SetActive(false);
        _menu3.SetActive(true);
        _menu4.SetActive(false);
        _click.Play();
    }

    public void OnLinearClick()
    {
        _menu1.SetActive(false);
        _menu2.SetActive(false);
        _menu3.SetActive(false);
        _menu4.SetActive(true);
        _click.Play();
    }

    public void OnDifSelect(string difString)
    {
        switch (difString)
        {
            case "E":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "E");
                    Debug.Log("Difficulty is now Easy");
                    break;
                }
            case "M":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "M");
                    Debug.Log("Difficulty is now Medium");
                    break;
                }
            case "H":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "H");
                    Debug.Log("Difficulty is now Hard");
                    break;
                }
        }
    }

    public void OnDifSelectLoadEquations()
    {
        _click.Play();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentScene + 1);
    }

    public void OnDifSelectLoadLinear()
    {
        _click.Play();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentScene + 2);
    }

    public void QuitGame()
    {
        _clickBack.Play();
        Debug.Log("Quiting game");
        Application.Quit();
    }
}
