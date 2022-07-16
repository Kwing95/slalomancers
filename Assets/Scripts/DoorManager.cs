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
        // Delete objects from previous room
        List<GameObject> chests = ObjectContainer.GetAllChests();
        foreach(GameObject chest in chests)
            Destroy(chest);

        List<GameObject> bullets = ObjectContainer.GetAllBullets();
        foreach (GameObject bullet in bullets)
            Destroy(bullet);


        // Update current location
        Point oldPoint = Room<RoomData>.current.location;
        //Debug.Log(Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);
        Room<RoomData>.current = Room<RoomData>.GetRoomFromPoint(new Point(oldPoint.x + offset.x, oldPoint.y + offset.y));
        text.text = Room<RoomData>.current.name + "\n" + Room<RoomData>.current.data.difficulty;
        // Update doors
        UpdateDoors();
        Debug.Log("Entering room " + Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);
        // Unfreeze players
        foreach (GameObject player in ObjectContainer.GetAllPlayers())
            player.GetComponentInChildren<HatMover>().SetPause(false);

        // Spawn enemies
        for(int i = 0; i < Room<RoomData>.current.data.chunks.Length; ++i)
        {
            Chunk chunk = Room<RoomData>.current.data.chunks[i];

            if (chunk.type == Chunk.Enemies.Chest)
            {
                // Spawn chests
                GameObject newChest = Instantiate(GameManager.instance.chests[0], ChunkIndexToPosition(2),
                    Quaternion.identity, ObjectContainer.instance.chests.transform); // No parent is set!
                ChestPickup chestPickup = newChest.GetComponent<ChestPickup>();

                chestPickup.code = Room<RoomData>.current.data.chunks[2].chest.code;
                chestPickup.chestGroup = Room<RoomData>.current.data.chunks[2].chest.group;

            } else if (chunk.type != Chunk.Enemies.None && !Room<RoomData>.current.GetCleared())
            {
                // Spawn enemies
                GameObject newEnemy = Instantiate(GameManager.instance.enemies[(int)chunk.type], ChunkIndexToPosition(i),
                    Quaternion.identity);

                // Using "chests" as parent should work
                newEnemy.transform.parent = chunk.type == Chunk.Enemies.Trap ?
                    ObjectContainer.instance.chests.transform : ObjectContainer.instance.enemies.transform;
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
        string output = "";
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; ++j)
            {
                output += floor.floor.floor[i, j] == null ? "1" : "0";
            }
            output += "\n";
        }
        if(Floor<RoomData>.PointInBounds(point) && floor.floor.floor[point.y, point.x].data.isSecret)
            door.SetStatus(Door.Status.Hidden);
        else if (Floor<RoomData>.PointInBounds(point) && floor.floor.floor[point.y, point.x].GetValid())
            door.SetStatus(Room<RoomData>.current.GetCleared() ? Door.Status.Open : Door.Status.Locked);
        else
            door.SetStatus(Door.Status.Blocked);
    }

}
