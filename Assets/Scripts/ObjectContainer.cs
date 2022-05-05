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
        List<GameObject> corpseList = new List<GameObject>();
        for (int i = 0; i < instance.players.transform.childCount; ++i)
            corpseList.Add(instance.players.transform.GetChild(i).gameObject);

        return corpseList;
    }
}
