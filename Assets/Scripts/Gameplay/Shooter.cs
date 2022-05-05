using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shooter : MonoBehaviour
{

    public GameObject bullet;
    public Color color;

    public Bullet.Type bulletType = Bullet.Type.Friendly;
    public float attackSpeed = 0.5f;
    public int bulletDamage = 1;

    public float bulletSpeed = 10;
    public float bulletPenetration = 1;
    public float bulletLifespan = 3;

    public Rigidbody2D rb;
    private float cooldownCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownCounter -= Time.deltaTime;
    }

    private Vector3 UnitDirection()
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * (rb.rotation + 90)), Mathf.Sin(Mathf.Deg2Rad * (rb.rotation + 90)));
    }

    public void Shoot()
    {
        if (cooldownCounter > 0)
            return;

        SingleShot();

        cooldownCounter = attackSpeed;
    }

    public void SingleShot()
    {
        Vector2 unitDirection = UnitDirection();
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + unitDirection, unitDirection);
        Bullet.Type type = bulletType;// (hit.collider != null && hit.collider.CompareTag("Friend")) ? Bullet.Type.Support : Bullet.Type.Friendly;

        Vector2 vel = bulletSpeed * unitDirection;

        GameObject temp = Instantiate(bullet, (Vector2)transform.position + unitDirection * 2, Quaternion.identity);
        temp.GetComponent<Bullet>().Initialize(vel, bulletDamage, bulletPenetration, bulletLifespan, type, color);
        //temp.GetComponent<SpriteRenderer>().color = bulletType == Bullet.Type.Friendly ? Color.green : Color.red;
    }
}
