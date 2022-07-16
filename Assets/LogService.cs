using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogService : MonoBehaviour
{

    public Text logText;
    public static LogService instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ClearLog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToLog(string entry)
    {
        logText.text += entry + "\n";
    }

    public void ClearLog()
    {
        logText.text = "";
    }


}
