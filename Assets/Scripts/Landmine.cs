using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{

    public static int total = 0;
    public int damage = 1;
    public GameObject root;

    // Start is called before the first frame update
    void Start()
    {
        total += 1;
    }

    private void OnDestroy()
    {
        total -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Collision(collision.gameObject);
    }

    private void Collision(GameObject obj)
    {
        Damageable damageable = obj.GetComponent<Damageable>();
        if (damageable)
        {
            // If this is a friendly bullet hitting an ally
            damageable.TakeDamage(damage, 2 * (obj.transform.position - transform.position));
        }
        Destroy(root);
    }
}
