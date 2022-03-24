using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Damageable : MonoBehaviour
{
    public enum Behavior { Stun, Split, Die };
    public Behavior behavior = Behavior.Die;

    public GameObject root;
    public Image greenBar;
    public Image redBar;

    private Stunnable stunnable;
    private Splitter splitter;

    private float greenHealth = 100;
    private float redHealth = 100;
    public float recoverySpeed = 10;
    public float lives = 3;

    private bool recovering = false;

    // Start is called before the first frame update
    void Start()
    {
        stunnable = GetComponent<Stunnable>();
        splitter = GetComponent<Splitter>();
        UpdateStun();
    }

    // Update is called once per frame
    void Update()
    {
        if (recovering)
        {
            greenHealth += Time.deltaTime * recoverySpeed;
            redHealth -= Time.deltaTime * recoverySpeed / lives;

            if(redHealth <= 0)
            {
                greenHealth = 0;
                Destroy(root);
            }
            else if(greenHealth >= 100)
            {
                greenHealth = 100;
                recovering = false;
                UpdateStun();
            }
        }

        greenBar.fillAmount = greenHealth / 100;
        redBar.fillAmount = redHealth / 100;
    }

    public void TakeDamage(int amount)
    {
        if (recovering)
            return;
        
        greenHealth -= amount;
        if(greenHealth <= 0)
        {
            greenHealth = 0;
            switch (behavior)
            {
                case Behavior.Die:
                    Destroy(gameObject);
                    break;
                case Behavior.Stun:
                    recovering = true;
                    UpdateStun();
                    break;
                case Behavior.Split:
                    if (splitter)
                        splitter.Split();
                    break;
            }
        }
    }

    public bool GetRecovering()
    {
        return recovering;
    }

    private void UpdateStun()
    {
        if (stunnable)
            stunnable.SetStunned(recovering);
    }

}
