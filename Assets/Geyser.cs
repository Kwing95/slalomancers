using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{

    public float rateOfFire = 0.5f;
    public float rotateAmount = 10;
    private float currentRotation = 0;
    private float cooldown = 0;
    private Shooter shooter;
    private DamageableEnemy damageable;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<Shooter>();
        damageable = GetComponent<DamageableEnemy>();
        damageable.damageHandler += _OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            ShootAndRotate();
            cooldown = rateOfFire;
        }
    }

    private void ShootAndRotate()
    {
        currentRotation += rotateAmount;
        transform.rotation = Quaternion.Euler(Vector3.forward * currentRotation);
        //Debug.Log(transform.eulerAngles);
        shooter.SingleShot();
    }

    private void _OnDamage()
    {
        cooldown = Mathf.Min(rateOfFire * 2, cooldown + (rateOfFire / 2));
    }
}
