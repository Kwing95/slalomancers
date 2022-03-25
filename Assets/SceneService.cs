using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneService : MonoBehaviour
{

    public Text playerField;
    public Text asteroidField;
    public Text wyvernField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        bool errorState = false;

        try
        {
            GameManager.numPlayers = int.Parse(playerField.text);
        }
        catch
        {
            playerField.text = "ENTER A NUMBER";
            errorState = true;
        }
        try
        {
            GameManager.numAsteroids = int.Parse(asteroidField.text);
        }
        catch
        {
            asteroidField.text = "ENTER A NUMBER";
            errorState = true;
        }
        try
        {
            GameManager.numWyverns = int.Parse(wyvernField.text);
        }
        catch
        {
            wyvernField.text = "ENTER A NUMBER";
            errorState = true;
        }

        if(!errorState)
            LoadScene("Sandbox");
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
