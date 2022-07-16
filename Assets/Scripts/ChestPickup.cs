using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour
{

    public int chestGroup;
    public static List<int> openedGroups;
    public string code;
    public SpriteRenderer icon;
    // Must contain a list of all linked chests, OR a name common to all chests in collection
    // Must contain code to give player

    // Start is called before the first frame update
    void Start()
    {
        if(openedGroups == null)
            openedGroups = new List<int>();

        Debug.Log(chestGroup);
        if (openedGroups.Contains(chestGroup) && chestGroup != -1)
            Destroy(gameObject);

        switch (chestGroup)
        {
            case 0:
                icon.color = Color.red;
                break;
            case 1:
                icon.color = Color.green;
                break;
            case 2:
                icon.color = Color.cyan;
                break;
            case 3:
                icon.color = Color.yellow;
                break;
            default:
                icon.color = Color.black;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Friend") && ObjectContainer.GetAllEnemies().Count == 0)
        {
            LogService.instance.AddToLog(code);
            ToastService.Toast("FOUND A CODE");

            openedGroups.Add(chestGroup);
            Destroy(gameObject);
        }
    }
}
