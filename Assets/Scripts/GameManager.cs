using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int numAsteroids = 1;
    public static int numWyverns = 1;
    public static int numPlayers = 4;

    public GameObject asteroid;
    public GameObject wyvern;
    public List<GameObject> enemies;
    public List<GameObject> playerButtons;

    public Image pauseButton;
    public GameObject quitButton;
    private bool isPaused = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        for(int i = 0; i < numAsteroids; ++i)
            Instantiate(asteroid, SpawnPosition(), Quaternion.identity, ObjectContainer.instance.enemies.transform);

        for (int i = 0; i < numWyverns; ++i)
            Instantiate(wyvern, SpawnPosition(), Quaternion.identity, ObjectContainer.instance.enemies.transform);
        */

        for (int i = 0; i < playerButtons.Count; ++i)
            playerButtons[i].SetActive(i < numPlayers);
    }

    private Vector2 SpawnPosition()
    {
        return new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        quitButton.SetActive(isPaused);
        pauseButton.color = isPaused ? Color.white : Color.clear;
        Time.timeScale = isPaused ? 0 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
