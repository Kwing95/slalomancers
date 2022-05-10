using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Door : MonoBehaviour
{

    public List<SpriteRenderer> blocks;

    public enum Direction { Left, Up, Down, Right };
    public Direction direction;
    public int secretHealth = -1;
    private Point offset;
    public enum Status { Open, Locked, Blocked };
    private Status status = Status.Blocked;

    void Awake()
    {
        if(blocks == null || blocks.Count == 0)
        {
            blocks = new List<SpriteRenderer>();
            blocks.Add(GetComponent<SpriteRenderer>());
        }
        switch (direction)
        {
            case Direction.Left:
                offset = new Point(-1, 0);
                break;
            case Direction.Right:
                offset = new Point(1, 0);
                break;
            case Direction.Up:
                offset = new Point(0, 1);
                break;
            case Direction.Down:
                offset = new Point(0, -1);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("AllyBullets") ||
            collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullets"))
        {
            secretHealth -= 1;
            if (secretHealth == 0)
                SetStatus(Status.Open);
        }
            

        if (collision.gameObject.CompareTag("Friend") && status == Status.Open)
        {
            // Debug.Log("loading with offset " + offset.x + ", " + offset.y);
            Transform player = collision.gameObject.transform.parent;
            player.position = (Vector2)player.position - (24 * new Vector2(offset.x, offset.y));
            player.GetComponentInChildren<HatMover>().SetPause(true);
            //player.gameObject.SetActive(false);
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Fader.instance.FadeTransition(() => DoorManager.instance.LoadRoom(offset));
        }
    }

    public void SetStatus(Status value)
    {
        //Debug.Log(value.ToString());
        status = value;
        Color newColor = new Color();

        switch (value)
        {
            case Status.Open:
                newColor = Color.green;
                break;
            case Status.Locked:
                newColor = Color.yellow;
                break;
            case Status.Blocked:
                newColor = Color.clear;
                break;
        }
        
        //newColor.a = 0.66f;

        foreach (SpriteRenderer elt in blocks)
            elt.color = newColor;
            
    }
}
