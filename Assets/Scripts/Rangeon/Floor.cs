using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Point
{
    public int x;
    public int y;

    public Point(int xIn, int yIn)
    {
        x = xIn;
        y = yIn;
    }
}

public class Room<TContent>
{
    public static Room<TContent> current;
    public static List<Room<TContent>> rooms;
    public enum Map { Hidden, Secret, Seen, Visited, Current }
    public readonly List<Color> mapColors = new List<Color> { Color.clear, Color.clear, Color.gray, Color.white, Color.red };
    public Map mapStatus = Map.Hidden;
    public readonly Point location;
    public readonly string name;
    public bool isStart;
    public TContent data;

    private bool valid = false;
    private bool cleared = false;
    public int probability = 0;
    public int numNeighbors = 0;

    public Room(Point _location, string _name)
    {
        name = _name;
        location = _location;

        if (rooms == null)
            rooms = new List<Room<TContent>>();
        rooms.Add(this);
    }

    public static Room<TContent> GetRoomFromPoint(Point point)
    {
        //Debug.Log("get room from point");
        foreach(Room<TContent> room in rooms)
        {
            //Debug.Log(room.location.x + " " + room.location.y);
            if (room.location.x == point.x && room.location.y == point.y)
                return room;
        }


        return new Room<TContent>(new Point(-1, -1), "ERROR");
    }

    public static List<int> NUniques(int n, int min, int max)
    {
        List<int> set = new List<int>();

        for (int i = min; i < max; ++i)
            set.Add(i);

        List<int> uniques = new List<int>();

        for (int i = 0; i < n; ++i)
        {
            int index = PRNG.Range(0, set.Count);
            uniques.Add(set[index]);
            set.RemoveAt(index);
        }

        return uniques;
    }

    public void Clear()
    {
        cleared = true;
        if(DoorManager.instance)
            DoorManager.instance.UpdateDoors();
    }
    public bool GetCleared()
    {
        return cleared;
    }

    public void Activate()
    {
        valid = true;
    }

    public bool GetValid()
    {
        return valid;
    }
}

public class Floor<TContent>
{

    public const int FLOOR_LENGTH = 8;
    private readonly int[] probs = new int[] { 0, 10000, -2, -2, -2 };
    public Room<TContent>[,] floor = new Room<TContent>[FLOOR_LENGTH, FLOOR_LENGTH];

    private void CreateEmptyFloor()
    {
        for (int y = 0; y < FLOOR_LENGTH; ++y)
            for (int x = 0; x < FLOOR_LENGTH; ++x)
                floor[y, x] = new Room<TContent>(new Point(x, y), (char)('A' + x) + (y + 1).ToString());
    }

    public virtual void GenerateFloor(int numRooms)
    {
        CreateEmptyFloor(); // This will create starting floor
        Room<TContent>.current = floor[FLOOR_LENGTH / 2, FLOOR_LENGTH / 2];
        SetStatus(Room<TContent>.current.location, Room<TContent>.current.mapStatus);

        AddRoom(true);
        Room<TContent>.current.Clear();

        for (int i = 0; i < numRooms; ++i)
        {
            AddRoom();
            // PrintFloor(); // for step-by-step analysis
        }

        UpdateNumNeighbors();

        LoadRoom(0, 0);
        // ConfigureDoors();
    }

    // Randomly selects a room based on existing rooms
    private Point SelectRoom()
    {
        List<Point> candidates = new List<Point>();
        int total = 0;

        for (int i = 0; i < FLOOR_LENGTH; ++i)
            for (int j = 0; j < FLOOR_LENGTH; ++j)
                if (floor[i, j].probability > 0)
                {
                    candidates.Add(new Point(j, i));
                    total += floor[i, j].probability;
                }

        int selection = PRNG.Range(0, total);
        int currentCandidate = 0;

        while (selection > 0)
        {
            selection -= floor[candidates[currentCandidate].y, candidates[currentCandidate].x].probability;
            currentCandidate += 1;
        }

        currentCandidate = currentCandidate % candidates.Count;
        return new Point(candidates[currentCandidate].x, candidates[currentCandidate].y);
    }

    public virtual void LoadRoom(int dx, int dy)
    {
        SetStatus(Room<TContent>.current.location, Room<TContent>.Map.Visited);
        Room<TContent>.current.location.x += dx;
        Room<TContent>.current.location.y += dy;
        SetStatus(Room<TContent>.current.location, Room<TContent>.Map.Current);
        
        // ConfigureDoors();

        // PrintFloor();

        // ClearChunks();
        // LoadChunks(floor[Room.currentLocation.y, Room.currentLocation.x].chunks);

        /*
        if (PrefabManager.instance.enemies.transform.childCount == 0)
        {
            PlayerMover.instance.cooldownActive = false;
            floor[Room.currentLocation.y, Room.currentLocation.x].Clear();
        }
        else
            PlayerMover.instance.cooldownActive = true;*/
    }

    private void AddRoom(bool isStart = false)
    {
        Point point = isStart ? new Point(FLOOR_LENGTH / 2, FLOOR_LENGTH / 2) : SelectRoom();
        floor[point.y, point.x].isStart = isStart;
        // TODO: Initialize enemy and room layout

            // Dummy value; can no longer add a room here because it's taken
        floor[point.y, point.x].Activate();
        floor[point.y, point.x].probability = -1;

        //floor[y, x].powerup = Passcode.AddRandomCode(new Point(point.x, point.y));
        //floor[y, x].chunks = isStart ? EmptyChunks() : RandomChunks();

        AdvanceProbability(new Point(point.x + 1, point.y));
        AdvanceProbability(new Point(point.x - 1, point.y));
        AdvanceProbability(new Point(point.x, point.y - 1));
        AdvanceProbability(new Point(point.x, point.y + 1));
    }

    private void UpdateNumNeighbors()
    {
        for(int y = 0; y < FLOOR_LENGTH; ++y)
        {
            for(int x = 0; x < FLOOR_LENGTH; ++x)
            {
                if(floor[x, y].GetValid())
                {
                    floor[x, y].numNeighbors = CountNeighbors(new Point(x, y));
                }
            }
        }
    }

    private int CountNeighbors(Point point)
    {
        int total = 0;

        total += floor[Mathf.Clamp(point.x - 1, 0, FLOOR_LENGTH - 1), point.y].GetValid() ? 1 : 0;
        total += floor[Mathf.Clamp(point.x + 1, 0, FLOOR_LENGTH - 1), point.y].GetValid() ? 1 : 0;
        total += floor[point.x, Mathf.Clamp(point.y - 1, 0, FLOOR_LENGTH - 1)].GetValid() ? 1 : 0;
        total += floor[point.x, Mathf.Clamp(point.y + 1, 0, FLOOR_LENGTH - 1)].GetValid() ? 1 : 0;

        return total;
    }

    // Advance the probability to reflect an additional neighbor to a space
    // Does nothing if point is out of bounds or point is occupied
    private void AdvanceProbability(Point point)
    {
        if (!PointInBounds(point))
            return;

        int oldProbability = floor[point.y, point.x].probability;
        if (oldProbability > -1)
            floor[point.y, point.x].probability = probs[Array.IndexOf(probs, oldProbability) + 1];
    }

    // Return true if and only if (x, y) is in bounds
    public static bool PointInBounds(Point point)
    {
        return (point.x >= 0 && point.y >= 0 && point.x < FLOOR_LENGTH && point.y < FLOOR_LENGTH);
    }

    private void SetStatus(Point location, Room<TContent>.Map status)
    {
        floor[location.y, location.x].mapStatus = status;
        //UIMap.SetColor(FLOOR_LENGTH - 1 - location.y, FLOOR_LENGTH - 1 - location.x, status);
    }

}
