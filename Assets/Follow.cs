using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public List<GameObject> subjects;

    public Vector3 offset = new Vector3(0, 0, -10);
    public float lerpIntensity = 0.1f;
    public float maxZoom = 5;
    public float minZoom = 15;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 averagePosition = Vector3.zero;
        float maxDistance = 0;

        for (int i = 0; i < subjects.Count; ++i)
        {
            averagePosition.x += subjects[i].transform.position.x;
            averagePosition.y += subjects[i].transform.position.y;

            for(int j = i; j < subjects.Count; ++j)
                maxDistance = Mathf.Max(maxDistance, Vector3.Distance(subjects[i].transform.position, subjects[j].transform.position));
        }

        if(maxDistance > maxZoom)
        {
            cam.orthographicSize = Mathf.Min(maxDistance, minZoom);
        }
        
        averagePosition = averagePosition / subjects.Count;

        transform.position = Vector3.Lerp(averagePosition + offset, transform.position, 0.1f);
    }
}
