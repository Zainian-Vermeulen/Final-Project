using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script handles the UI of the main menu.
/// It displays the highscores the player has achieved in the past for each difficulty / mode
/// It also activates / deactivates the ui elements when certain buttons are pressed.
/// </summary>

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _equationsGameBtn, _playGameBtn, _backBtn, _highScoreBtn;

    //Multiply | Devide
    [SerializeField]
    private TMP_Text _playerHighScoreEasyEquations, _playerHighScoreMediumEquations, _playerHighScoreHardEquations;

    //Plus | Minus
    [SerializeField]
    private TMP_Text _playerHighScoreEasyLinear, _playerHighScoreMediumLinear, _playerHighScoreHardLinear;

    //Highscore Multiply | Divide
    private int playerHighScoreEasyEquations, playerHighScoreMediumEquations, playerHighScoreHardEquations;

    //Highscore Plus | Minus
    private int playerHighScoreEasyLinear, playerHighScoreMediumLinear, playerHighScoreHardLinear;

    [SerializeField]
    private GameObject _menu1, _menu2, _menu3, _menu4, _difficulty;


    [SerializeField]
    private AudioSource _click, _clickBack;

    private int currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        _menu1.SetActive(true);
        _menu2.SetActive(false);
        _menu3.SetActive(false);
        _menu4.SetActive(false);

        #region Multiply | Devide
        GetHighScores(_playerHighScoreEasyEquations, playerHighScoreEasyEquations, "Equations_Easy");
        
        GetHighScores(_playerHighScoreMediumEquations, playerHighScoreMediumEquations, "Equations_Medium");

        GetHighScores(_playerHighScoreHardEquations, playerHighScoreHardEquations, "Equations_Hard");
        #endregion

        #region Plus | Minus
        GetHighScores(_playerHighScoreEasyLinear, playerHighScoreEasyLinear, "Linear_Easy");
        
        GetHighScores(_playerHighScoreMediumLinear, playerHighScoreMediumLinear, "Linear_Medium");
        
        GetHighScores(_playerHighScoreHardLinear, playerHighScoreHardLinear, "Linear_Hard");
        #endregion

        if (_click == null)
            return;

        if (_clickBack == null)
            return;

    }

    private void GetHighScores(TMP_Text text, int difInt, string difString)
    {
        if (text != null) {
            difInt = PlayerPrefs.GetInt($"HighScore_{difString}", 0);
            text.text = $"High score is: {difInt}";
        }
        else
            return;
    }
   
    //Sets 2nd menu active
    public void OnSelectGame()
    {
        _menu1.SetActive(false);
        _menu2.SetActive(true);

        _click.Play();
    }


    //Based on current menu active, when back is clicked it goes to the previous menu
    public void OnBackClick()
    {
        _clickBack.Play();

        if (_menu2.activeInHierarchy == true) {
            _menu1.SetActive(true);
            _menu2.SetActive(false);
        }
        else if (_menu3.activeInHierarchy == true) {
            _menu2.SetActive(true);
            _menu3.SetActive(false);

        }
        else if (_menu4.activeInHierarchy == true) {
            _menu2.SetActive(true);
            _menu4.SetActive(false);
        }
        else
            return;
    }

    //Multiply | Devide menu activated
    public void OnEquationClick()
    {
        _menu2.SetActive(false);
        _menu3.SetActive(true);
        _click.Play();
    }

    //Plus | Minus menu activated
    public void OnLinearClick()
    {
        _menu2.SetActive(false);
        _menu3.SetActive(false);
        _menu4.SetActive(true);
        _click.Play();
    }


    //Sets player pref for what the current difficulty is
    //Difficulty is actually just what math type to use:
    
    //E = Multiply | M = Divide | H = Randombetween E and M
    //                  or
    //E = Plus | M = Minus | H = Random between E and M
    public void OnDifSelect(string difString)
    {
        switch (difString) {
            case "E":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "E");
                    break;
                }
            case "M":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "M");
                    break;
                }
            case "H":
                {
                    PlayerPrefs.SetString("PlayerDifficulty", "H");
                    break;
                }
        }
    }

    //Loads the Multiply | Divide scene
    public void OnDifSelectLoadEquations()
    {
        _click.Play();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentScene + 1);
    }

    //Loads the Plys | Minus scene
    public void OnDifSelectLoadLinear()
    {
        _click.Play();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentScene + 2);
    }

    public void QuitGame()
    {
        #if DEBUG
        Debug.Log("Quiting game");
        #endif

        _clickBack.Play();
        Application.Quit();
    }
}
