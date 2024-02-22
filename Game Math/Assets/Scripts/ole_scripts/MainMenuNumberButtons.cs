using UnityEngine;
using TMPro;

public class MainMenuNumberButtons : MonoBehaviour
{
    [SerializeField] private GameObject numbersInputField;
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = numbersInputField.GetComponent<TMP_InputField>();
    }
}