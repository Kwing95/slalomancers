using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastService : MonoBehaviour
{

    public float displayTime = 5;
    public static ToastService instance;
    private Text text;
    public Image panel;
    public Queue<string> queue;

    // Start is called before the first frame update
    void Start()
    {
        panel.color = Color.clear;
        instance = this;
        text = GetComponent<Text>();
        queue = new Queue<string>();
    }

    public static void Toast(string message)
    {
        instance.queue.Enqueue(message);

        if(instance.queue.Count == 1)
            instance.StartCoroutine(instance.ToastCoroutine());
    }

    public IEnumerator ToastCoroutine()
    {
        while(queue.Count > 0)
        {
            panel.color = new Color(0, 0, 0, 0.5f);
            text.text = queue.Dequeue();
            yield return new WaitForSeconds(displayTime);
        }
        panel.color = Color.clear;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
