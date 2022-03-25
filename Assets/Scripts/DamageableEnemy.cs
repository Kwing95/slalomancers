using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : Damageable
{

    public override void TakeDamage(int amount, Vector2 force)
    {
        if (recovering)
            redHealth -= amount;
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

        bool enoughHealth = greenHealth >= redHealth;

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
