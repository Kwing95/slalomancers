using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBound : MonoBehaviour
{

    private Rigidbody2D rb;
    public float boundary = 16;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -boundary)
            rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y);
        else if(transform.position.x > boundary)
            rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y);

        if (transform.position.y < -boundary)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y));
        else if (transform.position.y > boundary)
            rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(rb.velocity.y));
    }
}
