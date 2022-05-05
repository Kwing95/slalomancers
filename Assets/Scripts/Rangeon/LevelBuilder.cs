using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game manager for maintaining state of different classes
// Settles race conditions
public class LevelBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RoomArranger.instance.GenerateFloor(10);
    }

    public static void BuildFromSeed()
    {
        RoomArranger.instance.GenerateFloor(10);
        // Debug.Log(JSONHandler.LevelToJSON());
    }

    public static void BuildFromFile(string json)
    {
        
    }

    public static void ResetAll()
    {
        Passcode.ClearPasscodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
