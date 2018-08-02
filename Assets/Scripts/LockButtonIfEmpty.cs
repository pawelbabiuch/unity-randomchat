using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class LockButtonIfEmpty : MonoBehaviour
{
    private InputField inputField;
    [SerializeField]
    private Button button;

    private void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.ActivateInputField();
        CheckInputField();
    }

    public void CheckInputField()
    {
        bool isEmpty = string.IsNullOrEmpty(inputField.text);
        button.interactable = isEmpty ? false : true;
    }
}
