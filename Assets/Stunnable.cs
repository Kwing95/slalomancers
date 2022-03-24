using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunnable : MonoBehaviour
{
    public GameObject aliveSprite;
    public GameObject deadSprite;

    private bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStunned(bool value)
    {
        stunned = value;
        aliveSprite.SetActive(!stunned);
        deadSprite.SetActive(stunned);
    }
}
