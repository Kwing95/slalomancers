using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{

    public static ObjectContainer instance;

    public GameObject players;
    public GameObject enemies;
    public GameObject chests;
    public GameObject bullets;

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

    public static List<GameObject> GetAllChests()
    {
        List<GameObject> chestList = new List<GameObject>();
        for (int i = 0; i < instance.chests.transform.childCount; ++i)
            chestList.Add(instance.chests.transform.GetChild(i).gameObject);

        return chestList;
    }

    public static List<GameObject> GetAllBullets()
    {
        List<GameObject> bulletList = new List<GameObject>();
        for (int i = 0; i < instance.bullets.transform.childCount; ++i)
            bulletList.Add(instance.bullets.transform.GetChild(i).gameObject);

        return bulletList;

    }
}
