using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    public static DoorManager instance;
    public SlalomFloor floor;
    public Door left;
    public Door right;
    public Door up;
    public Door down;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRoom(Point offset)
    {
        Point oldPoint = Room<RoomData>.current.location;
        Debug.Log(Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);
        Room<RoomData>.current = Room<RoomData>.GetRoomFromPoint(new Point(oldPoint.x + offset.x, oldPoint.y + offset.y));
        UpdateDoors();
        Debug.Log(Room<RoomData>.current.location.x + " " + Room<RoomData>.current.location.y);

        foreach (GameObject player in ObjectContainer.GetAllPlayers())
            player.GetComponentInChildren<HatMover>().SetPause(false);
    }

    private void UpdateDoors()
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
