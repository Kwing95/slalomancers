using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{

    public int contactDamage = 25;
    public bool damageOnContact = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Friend") && damageOnContact)
        {
            collision.collider.GetComponent<Damageable>().TakeDamage(contactDamage, rb.velocity);
        }
    }
}
