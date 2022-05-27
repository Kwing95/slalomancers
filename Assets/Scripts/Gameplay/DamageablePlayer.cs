using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageablePlayer : Damageable
{

    public Image greenBar;
    public Image redBar;

    // Update is called once per frame
    new private void Update()
    {
        base.Update();
        RefreshBars();
    }

    public override IEnumerator Ragdoll(Vector2 force)
    {
        ragdolling = true;
        rb.AddForce(force * 50);

        HatMover player = rb.gameObject.GetComponentInChildren<HatMover>();
        if (player)
        {
            player.enabled = false;
        }

        yield return new WaitForSeconds(1);

        ragdolling = false;
        if (player)
            player.enabled = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    public override void TakeDamage(int amount, Vector2 force)
    {
        if (recovering || ragdolling)
            return;

        if (rb && canRagdoll)
            StartCoroutine(Ragdoll(force));

        greenHealth -= amount;

        if (greenHealth <= 0)
        {
            greenHealth = 0;
            recovering = true;
            UpdateStun();
        }
    }

    private void RefreshBars()
    {
        if (greenBar)
        {
            greenBar.fillAmount = greenHealth / maxHealth;
            redBar.fillAmount = redHealth / maxHealth;
        }
    }

    public override void Heal(int amount)
    {
        if (recovering)
            greenHealth = Mathf.Min(100, greenHealth + amount);
    }

    protected override void Recover()
    {
        base.Recover();

        if (greenHealth >= 100)
        {
            greenHealth = 100;
            recovering = false;
            UpdateStun();
        }
    }

}
