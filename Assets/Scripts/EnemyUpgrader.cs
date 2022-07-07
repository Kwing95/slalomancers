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
    private int powerLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Maybe reset with higher difficulty in arcade mode
        if(GameManager.currentMode == GameManager.GameMode.SingleFloor)
        {
            powerLevel = Room<RoomData>.current.data.difficulty;
            ApplyUpgrades();
        }
    }

    public void SetPowerLevel(int amount)
    {
        powerLevel = amount;
        ApplyUpgrades();
    }

    private void ApplyUpgrades()
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

        BoostEnemy();
    }

    public void BoostEnemy()
    {
        switch (type)
        {
            case Chunk.Enemies.Asteroid:
                BoostAsteroid();
                break;
            case Chunk.Enemies.Wyvern:
                BoostWyvern();
                break;
            case Chunk.Enemies.Geyser:
                BoostGeyser();
                break;
            case Chunk.Enemies.Bombardier:
                BoostBombardier();
                break;
        }
    }

    private void BoostAsteroid()
    {
        splitter.duplicatesPerSplit += powerLevel / 4;
        powerLevel %= 4;
        contactDamager.contactDamage += 10 * powerLevel / 2;
        powerLevel %= 2;
        damageable.maxHealth = 5 * powerLevel;
        damageable.Start();
    }

    private void BoostWyvern()
    {
        wyvern.chargeSpeed += 5 * powerLevel / 4;
        powerLevel %= 4;
        contactDamager.contactDamage += 10 * powerLevel / 2;
        shooter.bulletDamage += 10 * powerLevel / 2;
        powerLevel %= 2;
        damageable.maxHealth = 5 * powerLevel;
        damageable.Start();
    }

    private void BoostGeyser()
    {
        geyser.rateOfFire -= 0.1f * (powerLevel / 4);
        powerLevel %= 4;
        shooter.bulletDamage += 10 * powerLevel / 2;
        powerLevel %= 2;
        damageable.maxHealth = 5 * powerLevel;
        damageable.Start();
    }

    private void BoostBombardier()
    {
        bombardier.mineDamage += 10 * powerLevel / 3;
        powerLevel %= 3;
        damageable.maxHealth = 5 * powerLevel;
        damageable.Start();
        Debug.Log(damageable.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
