﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatMover : MonoBehaviour
{

    /* Max Health
     * Health Regen
     * Body Damage
     * Bullet Speed
     * Bullet Penetration
     * Bullet Damage
     * Reload
     * Move Speed
     */

    public float moveSpeed = 10; // min 3 max 12
    public float rotationSpeed = 5; // min 2 max 6

    public float attackSpeed = 0.5f;
    public float bulletDamage = 1;

    public int health = 3;
    public float recoveryTime = 5;

    public float bulletSpeed = 10;
    public float bulletPenetration = 1;
    public float bulletLifespan = 3;

    public Rigidbody2D rb;
    public GameObject bullet;
    public Color color;
    public List<SpriteRenderer> sprites;

    private Shooter shooter;
    private Damageable damageable;
    private bool isPressed = false;
    private float clockwise = 1;
    private bool hasTurned = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SpriteRenderer sr in sprites)
            sr.color = color;

        shooter = GetComponent<Shooter>();
        shooter.color = color;
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damageable.GetRecovering())
        {
            return;
        }
        // Following line for debugging
        // isPressed = Input.anyKey;

        if (isPressed)
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX &
                ~RigidbodyConstraints2D.FreezePositionY &
                RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = Vector2.zero;

            if (!hasTurned)
            {
                clockwise *= -1;
                hasTurned = true;
            }
            shooter.Shoot();
            
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX & 
                ~RigidbodyConstraints2D.FreezePositionY &
                ~RigidbodyConstraints2D.FreezeRotation;
            hasTurned = false;
            rb.angularVelocity = 0;
            rb.velocity = moveSpeed * UnitDirection();
            rb.rotation = rb.rotation + (clockwise * rotationSpeed);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        // rb.rotation = rb.rotation + (clockwise * rotationSpeed);
    }

    public void SetIsPressed(bool value)
    {
        isPressed = value;
    }

    private Vector3 UnitDirection()
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * (rb.rotation + 90)), Mathf.Sin(Mathf.Deg2Rad * (rb.rotation + 90)));
    }

}
