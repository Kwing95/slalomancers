using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject player;

    public float shootDuration = 3;
    public float restDuration = 5;

    private float counter = 0;
    private bool shooting = false;
    private Rigidbody2D rb;
    private Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        int destinationAngle = (int)Vector2.SignedAngle(Vector2.up, (Vector2)player.transform.position - (Vector2)transform.position);
        transform.rotation = Quaternion.Euler(Vector3.forward * destinationAngle);

        counter += Time.deltaTime;

        if (shooting)
            shooter.Shoot();

        if (counter >= (shooting ? shootDuration : restDuration))
        {
            shooting = !shooting;
            counter = 0; 
        }
    }
}
