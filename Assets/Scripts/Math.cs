using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class Math : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField _mathInputText;

    [SerializeField]
    private TMP_Text _mathInputTextPaceHolder;

    [SerializeField]
    private TMP_Text _mathText;

    [SerializeField]
    private TMP_Text _playerScore;

    [SerializeField]
    private TMP_Text _playerFinalScore;

    public double sumEquation = 0.0;
    private int numx, numy, numz, numOp;
    public string sign;

    public event System.Action shootEvent;

    private int playerCurrentScore = 0;
    private int playerFinalScore = 0;
    public int playerHighScoreEasy, playerHighScoreMedium, playerHighScoreHard;
    private int difFactor = 0;

    public event System.Action MathIncorrect;
    public event System.Action MathCorrect;

    private string playerDif;

    private int currentScene;


    private void Start()
    {
        playerDif = PlayerPrefs.GetString("PlayerDifficulty");
        _playerScore.text = $"Score: {playerCurrentScore}";
        //playerHighScore = PlayerPrefs.GetInt("HighScore_Equations", 0);

        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current scene is: " + currentScene);
        Numbers(1,11,1,11);

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
        else if (currentScene == 2)
        {
            Debug.Log("current scene is linear");

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
    }

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


    public enum MathOperator
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }
    
    public enum Difficulty
    {
        Easy,
        Medium, 
        Hard
    }

    public void Calculate(int num1, int num2, MathOperator op)
    {
        switch (op)
        {
            case MathOperator.Plus :
                {
                    sumEquation =  num1 + num2;
                    sign = "+";
                    Debug.Log("Sum is: " + sumEquation);
                    break;

                }

            case MathOperator.Minus:
                {
                    sumEquation = num1 - num2;
                    sign = "-";
                    Debug.Log("Sum is: " + sumEquation);

                    break;
                }

            case MathOperator.Multiply:
                {
                    sumEquation = num1 * num2;
                    sign = "x";
                    Debug.Log("Sum is: " + sumEquation);

                    break;
                }

            case MathOperator.Divide:
                {
                    sumEquation = num1 / num2;
                    sign = "/";
                    Debug.Log("Sum is: " + sumEquation);
                    break;
                }

            default:
                throw new InvalidOperationException("Couldn't process operation: " + op);
        }
    }

    private void Numbers(int numXMin, int numXMax, int numYMin, int numYMax)
    {
        numx = Random.Range(numXMin, numXMax);
        numy = Random.Range(numYMin, numYMax);
        numOp = Random.Range(0, 20);

        //ensures no negative numbers
        if (numx < numy)
        {
            numz = numx;
            numx = numy;
            numy = numz;
        }

        switch (playerDif)
        {
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

            case "":
                {
                    MathDifficult(Difficulty.Hard);
                }
                break;
            
            default:
                break;
        }

        Debug.Log("Difficulty is: " + playerDif);
        _mathText.text = $"{numx} {sign} {numy} = ";
        _mathInputText.text = "";
    }

    private void MathDifficult(Difficulty dif)
    {
        //Multiply | Divide
        if (currentScene == 1)
        {
            switch (dif)
            {
                case Difficulty.Easy: // *
                    {
                        Calculate(numx, numy, MathOperator.Multiply);

                        sign = "x";
                        difFactor = 20;
                    }
                    break;

                case Difficulty.Medium: // /
                    {
                        Calculate(numx, numy, MathOperator.Divide);

                        sign = "/";
                        difFactor = 20;
                    }
                    break;

                case Difficulty.Hard: // * or /
                    {
                        if(numOp <= 10)
                        {
                            Calculate(numx, numy, MathOperator.Multiply);

                            sign = "x";
                            difFactor = 50;
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
                            difFactor = 50;
                        }          
                    }
                    break;

                default:
                    break;
            }
        }
        else if (currentScene == 2) //Plus | minus
        {
            switch (dif)
            {

                case Difficulty.Easy: // +
                    {
                        Calculate(numx, numy, MathOperator.Plus);

                        sign = "+";
                        difFactor = 20;
                    }
                    break;
                case Difficulty.Medium: // -
                    {
                        Calculate(numx, numy, MathOperator.Minus);

                        sign = "-";
                        difFactor = 20;
                    }
                    break;
                case Difficulty.Hard: // + or -
                    {
                        if (numOp <= 10)
                        {

                            Calculate(numx, numy, MathOperator.Plus);

                            sign = "+";
                            difFactor = 50;
                        }
                        else if (numOp > 10 && numOp <= 20)
                        {

                            Calculate(numx, numy, MathOperator.Minus);

                            sign = "-";
                            difFactor = 50;
                        }
                    }
                    break;
                default:
                    break;
            }           
        }
    }
 
    public void OnMathClick()
    {
        int i;
        int playerNumber = 0;
        bool result = int.TryParse(_mathInputText.text, out i);
        playerCurrentScore = 0;

        if (result)
        {
            playerNumber = int.Parse(_mathInputText.text);
        }
        else if (!result)
        {
            InputFieldFocus();
            _mathInputTextPaceHolder.text = "Only numbers are allowed.";
            return;
        }


        if (playerNumber != sumEquation)
        {
            Debug.Log("You lose");
            _mathInputText.text = "";
            InputFieldFocus();
            _mathInputTextPaceHolder.text = "Try Again.";
            _playerFinalScore.text = $"Final score is: 0";

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


            if (currentScene == 1)
            {
                switch (playerDif)
                {

                    case "E":
                        PlayerHighScore("HighScore_Equations_Easy", playerHighScoreEasy);
                        break;
                    case "M":
                        PlayerHighScore("HighScore_Equations_Medium", playerHighScoreMedium);
                        break;
                    case "H":
                        PlayerHighScore("HighScore_Equations_Hard", playerHighScoreHard);
                        break;
                    default:
                        break;
                }
            }
            else if (currentScene == 2)
            {
                switch (playerDif)
                {
                    case "E":
                        PlayerHighScore("HighScore_Linear_Easy", playerHighScoreEasy);
                        break;
                    case "M":
                        PlayerHighScore("HighScore_Linear_Medium", playerHighScoreMedium);
                        break;
                    case "H":
                        PlayerHighScore("HighScore_Linear_Hard", playerHighScoreHard);
                        break;
                    default:
                        break;
                }
            }        
        }
    }

    private void PlayerHighScore(string playerScore, int score)
    {
        if (score <= playerFinalScore)
        {
            score = playerFinalScore;
            PlayerPrefs.SetInt(playerScore, score);
        }
    }
    private void InputFieldFocus()
    {
        _mathInputText.Select();
        _mathInputText.ActivateInputField();
    }

}
