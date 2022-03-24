using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public enum Type { Friendly, Hostile, Support };

    public int damage = 1;
    public float penetration = 1;
    public Type type = Type.Friendly;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Despawn despawn;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        despawn = GetComponent<Despawn>();
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case Type.Friendly:
                //sr.color = Color.green;
                tag = "FriendlyBullet";
                break;
            case Type.Hostile:
                //sr.color = Color.red;
                tag = "EnemyBullet";
                break;
            case Type.Support:
                //sr.color = Color.white;
                tag = "SupportBullet";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Vector2 vel, int dmg, float pen, float lifespan, Type t, Color c)
    {
        sr.color = c;
        damage = dmg;
        penetration = pen;
        type = t;
        switch(type)
        {
            case Type.Hostile:
                gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
                break;
            case Type.Friendly:
                gameObject.layer = LayerMask.NameToLayer("AllyBullets");
                break;
        }
        despawn.timeToLive = lifespan;
        rb.velocity = vel;
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
            damageable.TakeDamage(damage);

        Destroy(gameObject);
    }
}
