using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{

    public List<GameObject> linkedObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(linkedObjects != null)
            foreach(GameObject elt in linkedObjects)
                Destroy(elt);
    }
}
