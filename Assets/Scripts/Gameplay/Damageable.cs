using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Damageable : MonoBehaviour
{
    public GameObject root;

    public Rigidbody2D rb;
    public float maxHealth = 100;

    private Stunnable stunnable;

    protected float greenHealth = 100;
    protected float redHealth = 100;
    public bool canRagdoll = true;
    public float recoverySpeed = 10;
    public float lives = 3;

    protected bool ragdolling = false;
    protected bool recovering = false;

    // Start is called before the first frame update
    public void Start()
    {
        greenHealth = redHealth = maxHealth;
        
        stunnable = GetComponent<Stunnable>();
        UpdateStun();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (recovering)
            Recover();
    }

    protected virtual void Recover()
    {
        greenHealth += Time.deltaTime * recoverySpeed;
        redHealth -= Time.deltaTime * recoverySpeed / lives;

        if (redHealth <= 0)
        {
            greenHealth = 0;
            Destroy(root);
        }
    }

    public virtual IEnumerator Ragdoll(Vector2 force)
    {
        yield return new WaitForSeconds(1);
    }

    public virtual void TakeDamage(int amount, Vector2 force)
    {

    }

    public bool GetRecovering()
    {
        return recovering;
    }

    protected void UpdateStun()
    {
        if (stunnable)
            stunnable.SetStunned(recovering);
    }

    public virtual void Heal(int amount)
    {

    }

}
