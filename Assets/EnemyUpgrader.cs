using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpgrader : MonoBehaviour
{

    private ContactDamager contactDamager;
    private DamageableEnemy damageable;
    private Shooter shooter;
    private Splitter splitter;
    private Bombardier bombardier;
    private Boss wyvern;
    private Geyser geyser;
    private Chunk.Enemies type;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<DamageableEnemy>();
        shooter = GetComponent<Shooter>();
        contactDamager = GetComponent<ContactDamager>();

        splitter = GetComponent<Splitter>();
        if (splitter)
            type = Chunk.Enemies.Asteroid;

        bombardier = GetComponent<Bombardier>();
        if (bombardier)
            type = Chunk.Enemies.Bombardier;

        wyvern = GetComponent<Boss>();
        if (wyvern)
            type = Chunk.Enemies.Wyvern;

        geyser = GetComponent<Geyser>();
        if (geyser)
            type = Chunk.Enemies.Geyser;
    }

    public void BoostEnemy()
    {
        switch (type)
        {
            case Chunk.Enemies.Asteroid:
                BoostAsteroid();
                break;
        }
    }

    private void BoostAsteroid()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
