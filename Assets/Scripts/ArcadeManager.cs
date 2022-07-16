using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{

    [System.Serializable]
    public class ListWrapper
    {
        public List<GameObject> list;
    }

    public static ArcadeManager instance;
    public GameObject spawnOrb;
    public List<ListWrapper> levels;
    public List<GameObject> unregisteredPlayers;
    private int currentLevel = -1;
    private int numPlayers = 4;

    public GameObject gameOverPanel;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentLevel = -1;
        numPlayers = 4;
        CheckEnemyContainer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GameOver()
    {
        Time.timeScale = 0;
        instance.gameOverPanel.SetActive(true);
        instance.score.text = instance.currentLevel + " WAVE" + (instance.currentLevel != 1 ? "S" : "") +
            " CLEARED BY " + instance.numPlayers + " PLAYER" + (instance.numPlayers != 1 ? "S" : "");
    }

    public int GetWavesCleared()
    {
        return currentLevel;
    }

    public void CheckEnemyContainer()
    {
        currentLevel += 1;

        if (currentLevel == 1)
            foreach (GameObject playerButton in unregisteredPlayers)
                if (!playerButton.GetComponent<PlayerButton>().hasBeenRegistered)
                {
                    playerButton.SetActive(false);
                    numPlayers -= 1;
                }

        int modLevel = currentLevel % levels.Count;
        foreach(GameObject enemy in levels[modLevel].list)
        {
            GameObject newSpawn = Instantiate(spawnOrb, RandomPosition(), Quaternion.identity, ObjectContainer.instance.enemies.transform);
            newSpawn.GetComponent<SpawnOrb>().objectToSpawn = enemy;
            EnemyUpgrader upgrader = newSpawn.GetComponent<EnemyUpgrader>();
            
            if (upgrader)
                upgrader.SetPowerLevel(currentLevel / 3);
        }
    }

    private Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
    }

}
