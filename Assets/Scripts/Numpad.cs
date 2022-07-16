using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numpad : MonoBehaviour
{

    public int maxInputLength = 8;
    public Text textObject;
    private string inputText;

    // Start is called before the first frame update
    void Start()
    {
        inputText = "";
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNumber(string number)
    {
        if(inputText.Length < maxInputLength)
            inputText += number;
        UpdateText();
    }

    public void Backspace()
    {
        if(inputText != "")
        {
            inputText = inputText.Substring(0, inputText.Length - 1);
            UpdateText();
        }
    }

    public void EnterCode()
    {
        PassManager.TryPassword(inputText);
        // Cross-check code with valid codes
        inputText = "";
        UpdateText();
    }

    private void UpdateText()
    {
        textObject.text = inputText == "" ? "ENTER CODE" : inputText;
    }

    public void ToggleNumpad()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        inputText = "";
        UpdateText();
    }
}
