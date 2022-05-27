using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HatMover hatMover;
    public Shooter shooter;
    public DamageablePlayer damageable;

    public enum Stat { Health, Recovery, Speed, Damage, Reload, Penetration }

    public float healthUp = 10;
    public float recoveryUp = 2;
    public float speedUp = 2;
    public float damageUp = 1;
    public float rateOfFireUp = 0.85f;
    public float penetrationUp = 1;

    public float maxHealth = 100;
    public float recoverySpeed = 10;
    public float bulletSpeed = 10;
    public float bulletDamage = 1;
    public float rateOfFire = 0.5f;
    public float bulletPenetration = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BuffHealth()
    {
        maxHealth += healthUp;
        Refresh();
    }

    public void BuffRecovery()
    {
        recoverySpeed += recoveryUp;
        Refresh();
    }

    public void BuffSpeed()
    {
        bulletSpeed += speedUp;
        Refresh();
    }

    public void ReloadUp()
    {
        rateOfFire *= rateOfFireUp;
        Refresh();
    }

    public void DamageUp()
    {
        bulletDamage += damageUp;
        Refresh();
    }

    public void PenetrationUp()
    {
        bulletPenetration += penetrationUp;
        Refresh();
    }

    private void Refresh()
    {
        damageable.maxHealth = maxHealth;
        damageable.recoverySpeed = recoverySpeed;
        shooter.bulletSpeed = bulletSpeed;
        shooter.attackSpeed = rateOfFire;
        shooter.bulletDamage = (int)bulletDamage;
        shooter.bulletPenetration = bulletPenetration;
    }
}
