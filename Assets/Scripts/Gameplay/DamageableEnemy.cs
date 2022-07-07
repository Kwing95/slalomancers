using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : Damageable
{
    public delegate void DamageHandler();
    public DamageHandler damageHandler;

    private void OnDestroy()
    {
        // Debug.Log(ObjectContainer.GetAllEnemies().Count);
        if(ObjectContainer.GetAllEnemies().Count == 1)
        {
            if(GameManager.currentMode == GameManager.GameMode.SingleFloor)
            {
                Room<RoomData>.current.Clear();
            }
            else if(GameManager.currentMode == GameManager.GameMode.Arcade)
            {
                ArcadeManager.instance.CheckEnemyContainer();
            }
        }
    }

    public override void TakeDamage(int amount, Vector2 force)
    {
        //Debug.Log(greenHealth + " " + redHealth);
        if(damageHandler != null)
            damageHandler.Invoke();

        if (recovering)
        {
            if(redHealth < 2)
                redHealth = 0;
            
            redHealth -= amount;
        }
        else
            greenHealth -= amount;

        // Debug.Log(greenHealth + " " + redHealth);

        if (greenHealth <= 0 && !recovering)
        {
            if (rb && canRagdoll)
                StartCoroutine(Ragdoll(force));

            Splitter splitter = GetComponent<Splitter>();
            if (splitter)
            {
                splitter.Split();
                Destroy(gameObject);
                return;
            }

            greenHealth = 0;
            recovering = true;
            UpdateStun();
        }
    }

    public override IEnumerator Ragdoll(Vector2 force)
    {
        ragdolling = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(force * 500);

        Boss boss = rb.gameObject.GetComponent<Boss>();
        if (boss)
        {
            boss.enabled = false;
        }

        yield return new WaitForSeconds(maxHealth / recoverySpeed);

        ragdolling = false;
        rb.isKinematic = true;
        if (boss)
            boss.enabled = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    protected override void Recover()
    {
        if (redHealth <= 0)
        {
            greenHealth = 0;
            Destroy(root);
        }

        bool enoughHealth = greenHealth >= (redHealth * 1.5f);

        if(!enoughHealth && ragdolling)
        {
            greenHealth += Time.deltaTime * recoverySpeed;
            redHealth -= Time.deltaTime * recoverySpeed / lives;
        }

        if(enoughHealth && !ragdolling)
        {
            greenHealth = redHealth;
            recovering = false;
            UpdateStun();
        }
    }

}
