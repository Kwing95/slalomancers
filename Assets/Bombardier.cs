using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardier : MonoBehaviour
{

    public Vector2 topLeft;
    public Vector2 bottomRight;
    public GameObject landmine;
    private Rigidbody2D rb;
    private bool isIdle = true;
    public float chargeSpeed = 10;
    public float chargeDuration = 1;
    public float rotateDuration = 1.5f;

    private enum State { Rotating, Attacking };
    private State currentState;
    private float currentAngle = 0;
    public float rotateSpeed = 5;
    private float destinationAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
            switch (currentState)
            {
                case State.Attacking:
                    StartCoroutine(ChargeAttack());
                    break;
                case State.Rotating:
                    StartCoroutine(RotateCoroutine());
                    break;
            }

        if (currentState == State.Rotating)
            Rotate();

    }

    public IEnumerator ChargeAttack()
    {
        isIdle = false;
        rb.velocity = transform.up * chargeSpeed;

        // yield return 0; // wait one frame
        yield return new WaitForSeconds(chargeDuration);
        rb.velocity = Vector2.zero;

        if(Landmine.total < 3)
            Instantiate(landmine, transform.position, Quaternion.identity);
        currentState = State.Rotating;
        isIdle = true;
    }

    public void Rotate(/*float destinationAngle*/)
    {
        currentAngle = Quaternion.Lerp(Quaternion.Euler(0, 0, currentAngle), Quaternion.Euler(0, 0, destinationAngle), Time.deltaTime * rotateSpeed).eulerAngles.z;
        transform.rotation = Quaternion.Euler(Vector3.forward * currentAngle);
    }

    public void SelectTarget()
    {
        Vector2 target = new Vector2(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y));
        destinationAngle = Vector2.SignedAngle(Vector2.up, target - (Vector2)transform.position);
    }

    public IEnumerator RotateCoroutine()
    {
        isIdle = false;
        currentState = State.Rotating;
        SelectTarget();
        yield return new WaitForSeconds(rotateDuration);
        currentState = State.Attacking;
        isIdle = true;
    }
}
