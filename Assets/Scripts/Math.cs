using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the math in each of the games.
/// It also sets the highscores of the player after winning / losing
/// </summary>

public class Math : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField _mathInputText;

    [SerializeField]
    private TMP_Text _mathInputTextPaceHolder, _mathText, _playerScore, _playerFinalScore;

    [SerializeField]
    private int playerCurrentScore = 0;

    public event System.Action shootEvent, MathIncorrect, MathCorrect;

    private double sumEquation = 0.0f;
    
    public string sign;
    private string playerDif;

    public int playerHighScoreEasy, playerHighScoreMedium, playerHighScoreHard;
    private int numx, numy, numz, numOp, currentScene, playerFinalScore = 0, difFactor = 0;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
       
        playerDif = PlayerPrefs.GetString("PlayerDifficulty");

        _playerScore.text = $"Score: {playerCurrentScore}";
        
        _playerFinalScore.text = $"Final score is: 0";
        playerFinalScore = playerCurrentScore;

        Numbers(1,11,1,11);

        #region Multiply | Devide
        if (currentScene == 1)
        {
            Debug.Log("Current scene is equations");
            switch (playerDif)
            {
                case "E":
                    playerHighScoreEasy = PlayerPrefs.GetInt("HighScore_Equations_Easy", 0);
                    break;
                case "M":
                    playerHighScoreMedium = PlayerPrefs.GetInt("HighScore_Equations_Medium", 0);
                    break;
                case "H":
                    playerHighScoreHard = PlayerPrefs.GetInt("HighScore_Equations_Hard", 0);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Plus | Minus
        else if (currentScene == 2)
        {

            switch (playerDif)
            {
                case "E":
                    playerHighScoreEasy = PlayerPrefs.GetInt("HighScore_Linear_Easy", 0);
                    break;
                case "M":
                    playerHighScoreMedium = PlayerPrefs.GetInt("HighScore_Linear_Medium", 0);
                    break;
                case "H":
                    playerHighScoreHard = PlayerPrefs.GetInt("HighScore_Linear_Hard", 0);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
    
    //Makes sure there can only be integers in the inputfield.
    private void Awake()
    {
        _mathInputText.characterValidation = TMP_InputField.CharacterValidation.Integer;
        _mathInputTextPaceHolder.text = "Type here.";
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OnMathClick();
        }
    }

    //Enum for the diffcent math operators
    public enum MathOperator
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    //Enum for the different difficulties
    public enum Difficulty
    {
        Easy, // Multiply or Plus
        Medium, // Devide or Minus
        Hard //Random
    }

    //Method for the answer to the equations
    public void Calculate(int num1, int num2, MathOperator op)
    {
        switch (op) {

            case MathOperator.Plus :
                {
                    sumEquation =  num1 + num2;
                    sign = "+";
                    break;
                }

            case MathOperator.Minus:
                {
                    sumEquation = num1 - num2;
                    sign = "-";
                    break;
                }

            case MathOperator.Multiply:
                {
                    sumEquation = num1 * num2;
                    sign = "x";
                    break;
                }

            case MathOperator.Divide:
                {
                    sumEquation = num1 / num2;
                    sign = "/";
                    break;
                }

            default:
                throw new InvalidOperationException("Couldn't process operation: " + op);
        }
    }

    //Method for deciding the numbers in the equations at random
    //The x and y have nothing to do with vectors, only a name
    private void Numbers(int numXMin, int numXMax, int numYMin, int numYMax)
    {
        numx = Random.Range(numXMin, numXMax);
        numy = Random.Range(numYMin, numYMax);
        numOp = Random.Range(0, 20);

        //ensures no negative numbers are used
        if (numx < numy) {
            numz = numx;
            numx = numy;
            numy = numz;
        }

        switch (playerDif) {

            case "E":
                {
                    MathDifficult(Difficulty.Easy);
                }
                break;
                
            case "M":
                {
                    MathDifficult(Difficulty.Medium);
                }
                break;

            case "H":
                {
                    MathDifficult(Difficulty.Hard);
                }
                break;
            
            default:
                break;
        }

        _mathText.text = $"{numx} {sign} {numy} = ";
        _mathInputText.text = "";
    }

    //Method for what to actually do when the difficulty has been selected
    private void MathDifficult(Difficulty dif)
    {
        #region Multiply | Devide
        if (currentScene == 1)
        {
            switch (dif)
            {
                case Difficulty.Easy: // Multiply
                    {
                        Calculate(numx, numy, MathOperator.Multiply);

                        sign = "x";
                        difFactor = 50;
                    }
                    break;

                case Difficulty.Medium: // Devide
                    {
                        int mod = numx % numy;
                        if (mod > 0)
                        {
                            Numbers(1, 11, 1, 11);
                            return;
                        }

                        Calculate(numx, numy, MathOperator.Divide);

                        sign = "/";
                        difFactor = 50;
                    }
                    break;

                case Difficulty.Hard: // Multiply or Devide
                    {
                        if(numOp <= 10)
                        {
                            Calculate(numx, numy, MathOperator.Multiply);

                            sign = "x";
                            difFactor = 100;
                        }
                        else if (numOp > 10 && numOp <= 20)
                        {

                            int mod = numx % numy;
                            if (mod > 0)
                            {
                                Numbers(1, 11, 1, 11);
                                return;
                            }

                            Calculate(numx, numy, MathOperator.Divide);
                            sign = "/";
                            difFactor = 100;
                        }          
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
        #region Plus | Minus
        else if (currentScene == 2) //Plus | minus
        {
            switch (dif)
            {

                case Difficulty.Easy: // Plus
                    {
                        Calculate(numx, numy, MathOperator.Plus);

                        sign = "+";
                        difFactor = 20;
                    }
                    break;
                case Difficulty.Medium: // minus
                    {
                        Calculate(numx, numy, MathOperator.Minus);

                        sign = "-";
                        difFactor = 20;
                    }
                    break;
                case Difficulty.Hard: //Plus or minus
                    {
                        if (numOp <= 10)
                        {

                            Calculate(numx, numy, MathOperator.Plus);

                            sign = "+";
                            difFactor = 30;
                        }
                        else if (numOp > 10 && numOp <= 20)
                        {

                            Calculate(numx, numy, MathOperator.Minus);

                            sign = "-";
                            difFactor = 30;
                        }
                    }
                    break;
                default:
                    break;
            }           
        }
        #endregion
    }

    //This method checks the math of the player compared to the equation answer
    public void OnMathClick()
    {
        int i;
        int playerNumber = 0;
        bool result = int.TryParse(_mathInputText.text, out i); //Tries to parse the answer of the player

        if (result) {
            playerNumber = int.Parse(_mathInputText.text);
            _playerScore.text = $"Score: {playerCurrentScore}";
            _playerFinalScore.text = $"Final score is: {playerFinalScore}";
        }
        else if (!result) {
            InputFieldFocus();
            _mathInputTextPaceHolder.text = "Only numbers are allowed.";
            return;
        }

        if (playerNumber != sumEquation) {
            _mathInputText.text = "";
           
            InputFieldFocus();
            _mathInputTextPaceHolder.text = "Try Again.";
           
            _playerScore.text = $"Score: {playerCurrentScore}";
            _playerFinalScore.text = $"Final score is: {playerFinalScore}";

            MathIncorrect?.Invoke();

            return;           
        }       
        else if (playerNumber == sumEquation)
        {
            Numbers(1,11,1,11);
           
            InputFieldFocus();
            _mathInputTextPaceHolder.text = "Type here.";
           
            playerCurrentScore += difFactor;
            playerFinalScore = playerCurrentScore;              
           
            _playerScore.text = $"Score: {playerCurrentScore}";
            _playerFinalScore.text = $"Final score is: {playerFinalScore}";
            
            shootEvent?.Invoke();
            MathCorrect?.Invoke();


            if (currentScene == 1) {
                switch (playerDif) {

                    case "E":
                        SetPlayerHighScore("HighScore_Equations_Easy", playerHighScoreEasy);
                        break;
                    case "M":
                        SetPlayerHighScore("HighScore_Equations_Medium", playerHighScoreMedium);
                        break;
                    case "H":
                        SetPlayerHighScore("HighScore_Equations_Hard", playerHighScoreHard);
                        break;
                    default:
                        break;
                }
            }
            else if (currentScene == 2) {
                switch (playerDif) {
                    case "E":
                        SetPlayerHighScore("HighScore_Linear_Easy", playerHighScoreEasy);
                        break;
                    case "M":
                        SetPlayerHighScore("HighScore_Linear_Medium", playerHighScoreMedium);
                        break;
                    case "H":
                        SetPlayerHighScore("HighScore_Linear_Hard", playerHighScoreHard);
                        break;
                    default:
                        break;
                }
            }        
        }
    }

    //Method to set the player's highscores
    private void SetPlayerHighScore(string playerScore, int score)
    {
        if (score <= playerFinalScore)
        {
            score = playerFinalScore;
            PlayerPrefs.SetInt(playerScore, score);
        }
    }

    //Method to focus the cursor on the input field
    private void InputFieldFocus()
    {
        _mathInputText.Select();
        _mathInputText.ActivateInputField();
    }
}
