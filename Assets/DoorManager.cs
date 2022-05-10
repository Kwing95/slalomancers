using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorManager : MonoBehaviour
{

    public static DoorManager instance;
    public SlalomFloor floor;
    public Door left;
    public Door right;
    public Door up;
    public Door down;
    public TextMeshPro text;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRoom(Point offset)
    {
        // Update current location
        Point oldPoint = Room<RoomData>.current.location;
        //Debug.Log(Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);
        Room<RoomData>.current = Room<RoomData>.GetRoomFromPoint(new Point(oldPoint.x + offset.x, oldPoint.y + offset.y));
        text.text = Room<RoomData>.current.name;
        // Update doors
        UpdateDoors();
        Debug.Log(Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);
        // Unfreeze players
        foreach (GameObject player in ObjectContainer.GetAllPlayers())
            player.GetComponentInChildren<HatMover>().SetPause(false);
        // Spawn enemies
        if (!Room<RoomData>.current.GetCleared())
        {
            for(int i = 0; i < Room<RoomData>.current.data.chunks.Length; ++i)
            {
                Chunk chunk = Room<RoomData>.current.data.chunks[i];
                if(chunk.type != Chunk.Enemies.None)
                {
                    Instantiate(GameManager.instance.enemies[(int)chunk.type], ChunkIndexToPosition(i), 
                        Quaternion.identity, ObjectContainer.instance.enemies.transform);
                }
            }
        }
    }

    private Vector2 ChunkIndexToPosition(int index)
    {
        Vector2 position = new Vector2(0, 0);

        if (index < 2)
            position.y = 9;
        else if (index > 2)
            position.y = -9;

        if (index == 0 || index == 3)
            position.x = -9;
        else if (index == 1 || index == 4)
            position.x = 9;

        return position;
    }

    public void UpdateDoors()
    {
        Point point = Room<RoomData>.current.location;

        UpdateDoor(left, new Point(point.x - 1, point.y));
        UpdateDoor(right, new Point(point.x + 1, point.y));
        UpdateDoor(up, new Point(point.x, point.y + 1));
        UpdateDoor(down, new Point(point.x, point.y - 1));
    }
    private void UpdateDoor(Door door, Point point)
    {
        if(Floor<RoomData>.PointInBounds(point) && floor.floor.floor[point.y, point.x].data.isSecret)
            door.SetStatus(Door.Status.Blocked);
        else if (Floor<RoomData>.PointInBounds(point) && floor.floor.floor[point.y, point.x].GetValid())
            door.SetStatus(Room<RoomData>.current.GetCleared() ? Door.Status.Open : Door.Status.Locked);
        else
            door.SetStatus(Door.Status.Blocked);
    }
}
