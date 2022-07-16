using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrb : MonoBehaviour
{

    public float maxSize = 4;
    public GameObject objectToSpawn;
    private float startSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        startSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * (transform.localScale.x + (Time.deltaTime * (maxSize - startSize)));
        if(transform.localScale.x > maxSize)
        {
            if (objectToSpawn)
                Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform.parent);

            Destroy(gameObject);
        }
    }
}
