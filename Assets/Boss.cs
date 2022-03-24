using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject players;

    private Rigidbody2D rb;
    private Shooter shooter;

    public float rotateSpeed = 5;
    public float rotateDuration = 1.5f;
    public float chargeSpeed = 10;
    public float chargeDuration = 1;
    public int numShots = 3;
    public float shotDelay = 0.5f;

    private float currentAngle = 0;
    private float destinationAngle = 0;
    private Vector2 target;
    private enum State { Rotating, Attacking };
    private bool isIdle = true;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
            switch (currentState)
            {
                case State.Attacking:
                    if(Random.Range(0, 2) == 0)
                        StartCoroutine(ShootAttack());
                    else
                        StartCoroutine(ChargeAttack());
                    break;
                case State.Rotating:
                    StartCoroutine(RotateCoroutine());
                    break;
            }

        if (currentState == State.Rotating)
            Rotate();

    }

    public void SkipAttack()
    {
        isIdle = false;
        currentState = State.Rotating;
        isIdle = true;
    }

    public IEnumerator ShootAttack()
    {
        isIdle = false;
        float rawAngle = transform.localRotation.eulerAngles.z;


        for (int i = 0; i < numShots; ++i)
        {
            float offset = Random.Range(-15, 15);
            transform.eulerAngles = new Vector3(0, 0, rawAngle + offset);
            shooter.SingleShot();
            yield return new WaitForSeconds(shotDelay);
        }

        transform.eulerAngles = new Vector3(0, 0, rawAngle);
        currentState = State.Rotating;
        isIdle = true;
    }

    public IEnumerator ChargeAttack()
    {
        isIdle = false;
        rb.velocity = transform.up * chargeSpeed;

        // yield return 0; // wait one frame
        yield return new WaitForSeconds(chargeDuration);
        rb.velocity = Vector2.zero;

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
        List<GameObject> playerList = new List<GameObject>();
        for (int i = 0; i < players.transform.childCount; ++i)
        {
            GameObject player = players.transform.GetChild(i).gameObject;
            if(player.activeSelf)
                playerList.Add(players.transform.GetChild(i).gameObject);
        }

        if (playerList.Count > 0)
            target = playerList[Random.Range(0, playerList.Count)].transform.position;
        else
            target = Vector2.zero;
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

        }
    }

    // ATTACKS
    // WEDGES: List<Wedge> where a wedge has startAngle, endAngle, range, damage
    // List<ProjectileBurst> PROJECTILE: bulletSpeed, bulletSize (number and timing of projectiles)
    // BEAM: range, damage (moving beam maybe)
    // CHARGE: speed, range, isHoming, damage (range is only set for leap)
    // PLUMES: numPlumes, damage (position of plumes)

}
