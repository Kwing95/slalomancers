using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public int numSplits = 2;
    public int duplicatesPerSplit = 2;
    public int damage = 1;

    public Vector2 initialMomentum = Vector2.zero;
    public GameObject duplicate;

    private Rigidbody2D rb;
    private float timeAlive = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if(initialMomentum == Vector2.zero)
        {
            // fix; currently only goes northeast
            rb.velocity = RandomVector(3, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable && collision.gameObject.layer == LayerMask.NameToLayer("Allies"))
            damageable.TakeDamage(damage, rb.velocity);

        Vector2 point = collision.GetContact(0).point;
        float dx = point.x - transform.position.x;
        float dy = point.y - transform.position.y;

        bool flipX = (rb.velocity.x > 0 && dx > 0) || (rb.velocity.x < 0 && dx < 0);
        bool flipY = (rb.velocity.y > 0 && dy > 0) || (rb.velocity.y < 0 && dy < 0);

        rb.velocity = new Vector2(rb.velocity.x * (flipX ? -1 : 1), rb.velocity.y * (flipY ? -1 : 1));
    }

    public void Split()
    {
        if (timeAlive < 0.2f)
            return;
        
        if (numSplits > 0)
            for (int i = 0; i < duplicatesPerSplit; ++i) {
                GameObject newDuplicate = Instantiate(duplicate, transform.position, Quaternion.identity);
                newDuplicate.transform.parent = transform.parent;
                //newDuplicate.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                newDuplicate.transform.localScale = transform.localScale * 0.66f;
                newDuplicate.GetComponent<Splitter>().numSplits = numSplits - 1;
            }

        // Destroy(gameObject);
    }

    public static Vector2 RandomVector(float min, float max)
    {
        return new Vector2(Random.Range(min, max) * (Random.Range(0, 2) == 0 ? -1 : 1),
            Random.Range(min, max) * (Random.Range(0, 2) == 0 ? -1 : 1));
    }

    public void OnDestroy()
    {
        //Split();
    }
}
