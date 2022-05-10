using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{

    public static ObjectContainer instance;

    public GameObject players;
    public GameObject enemies;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<GameObject> GetAllPlayers()
    {
        List<GameObject> playerList = new List<GameObject>();
        for (int i = 0; i < instance.players.transform.childCount; ++i)
            playerList.Add(instance.players.transform.GetChild(i).gameObject);

        return playerList;
    }

    public static List<GameObject> GetAllEnemies()
    {
        List<GameObject> enemyList = new List<GameObject>();
        for (int i = 0; i < instance.enemies.transform.childCount; ++i)
            enemyList.Add(instance.enemies.transform.GetChild(i).gameObject);

        return enemyList;
    }
}
