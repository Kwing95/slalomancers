using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnHit : MonoBehaviour
{

    public List<SpriteRenderer> sprites;
    private DamageableEnemy damageable;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<DamageableEnemy>();
        damageable.damageHandler += _OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _OnDamage()
    {
        foreach(SpriteRenderer sprite in sprites)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
    }
}
