using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class RomanNumbers : MonoBehaviour
{
    public TMP_InputField takeValue;
    private int correctValue;
    private int parsedValue;
    public InputCollider inputCollider;

    void Start()
    {
        if (inputCollider == null)
        {
            inputCollider = GameObject.FindObjectOfType<InputCollider>();
        }
    }

    void TakeRandomValue()
    {
        GameObject objWithTag = GameObject.FindGameObjectWithTag("RandomNumber");

        if (objWithTag != null)
        {
            TextMeshProUGUI textMeshPro = objWithTag.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                string textValue = textMeshPro.text;

                if (int.TryParse(textValue, out parsedValue))
                {
                    //Debug.Log("Parsed value" + parsedValue);

                    correctValue = parsedValue;
                }
                else
                {
                    Debug.LogError("Int conversion failed");
                }
            }
            else
            {
                Debug.LogError("TextMeshPro not found");
            }
        }
        else
        {
            Debug.LogError("RandomNumber not found");
        }
    }

    public void ButtonPressed()
    {
        TakeRandomValue();

        string inputText = takeValue.text;
        int convertedValue = RomanToInt(inputText);
        correctValue = convertedValue;
        //Debug.Log("Converted value: " + convertedValue);

        if (correctValue == parsedValue)
        {
            Debug.Log("Win");
        }
        else
        { 
            RestartLevel();
        }

        inputCollider.HideObject();
        ActivatePlayer();
        takeValue.text = "";
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ActivatePlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            Animator playerAnimator = playerObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.enabled = true;
            }
            else
            {
                Debug.LogError("Animator nor found");
            }

            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerMovement not found");
            }

            Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
            }
            else
            {
                Debug.LogError("Rigidbody2D not found");
            }
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

    static int RomanToInt(string roman)
    {
        Dictionary<char, int> romanNumerals = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        int result = 0;
        int prevValue = 0;

        for (int i = roman.Length - 1; i >= 0; i--)
        {
            char currentChar = roman[i];

            if (romanNumerals.ContainsKey(currentChar))
            {
                int value = romanNumerals[currentChar];

                if (value < prevValue)
                {
                    result -= value;
                }
                else
                {
                    result += value;
                }
                prevValue = value;
            }
            else
            {
                Debug.LogError("Not roman: " + currentChar);
            }
        }
        return result;
    }
}