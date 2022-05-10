using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public int difficulty = 0;
    public bool isSecret = false;
    // Following vars are for chunks
    public Chunk[] chunks = new Chunk[5];

    public RoomData()
    {
        List<int> vacancies = new List<int>();
        for (int i = 0; i < 5; ++i)
        {
            vacancies.Add(i);
            chunks[i] = new Chunk(true);
        }

        int numEnemies = PRNG.Range(1, 3); // 1-2, 1-3, 2-3, 2-4, 2-5
        for(int i = 0; i < numEnemies; ++i)
        {
            int chunkIndex = PRNG.Range(0, vacancies.Count);
            chunks[chunkIndex] = new Chunk();
            vacancies.RemoveAt(chunkIndex);
        }

    }
}

public class Chunk
{
    public enum Enemies { None, Asteroid, Wyvern, Geyser, Bombardier, Trap }
    public Enemies type;

    public Chunk(bool isVacant=false)
    {
        type = isVacant ? Enemies.None : (Enemies)PRNG.Range(1, 5);
    }

    public void SetTrap()
    {
        type = Enemies.Trap;
    }
}

public class Chest
{
    public enum Chests { }
    public List<string> codes;
}

public class Enemy
{

}
public class SlalomFloor : MonoBehaviour
{

    public Floor<RoomData> floor;
    public const int NUM_ROOMS = 10;

    // FLOOR CONTENTS:
    // 1-3 traps per floor, 1-2 secret rooms per floor
    // 1-5 walls per room, 2-5 enemies per room

    public void Awake()
    {
        floor = new Floor<RoomData>();

        // 1-3 secrets, 1-4 traps, {1, 2, ... NUM_ROOMS} distinct difficulties
        int numSecrets = 0;// PRNG.Range(1, 3);
        int numTraps = PRNG.Range(1, 4);
        List<int> difficulties = new List<int>();
        for (int i = 0; i < NUM_ROOMS; ++i)
            difficulties.Add(i);

        // Create normal and secret rooms
        floor.GenerateFloor(NUM_ROOMS + numSecrets);

        // Use dead ends as list of candidates for secret rooms
        List<Room<RoomData>> deadEnds = new List<Room<RoomData>>();
        foreach (Room<RoomData> room in Room<RoomData>.rooms)
        {
            // Initialize RoomData for all rooms
            room.data = new RoomData();
            if (room.numNeighbors == 1)
                deadEnds.Add(room);
        }

        // Designate [numSecrets] rooms as secret
        for (int i = 0; i < numSecrets; ++i)
        {
            int secretIndex = PRNG.Range(0, deadEnds.Count);
            deadEnds[secretIndex].data.isSecret = true;
            deadEnds.RemoveAt(secretIndex);
        }

        // Use valid and non-secret rooms as list of candidates
        List<Room<RoomData>> normalRooms = new List<Room<RoomData>>();
        foreach (Room<RoomData> room in Room<RoomData>.rooms)
            if (room.GetValid() && !room.data.isSecret && !room.isStart)
                normalRooms.Add(room);


        // Designate unique difficulty modifier to each room
        foreach (Room<RoomData> room in normalRooms)
        {
            // Set difficulty
            int difficultyIndex = PRNG.Range(0, difficulties.Count);
            room.data.difficulty = difficulties[difficultyIndex];
            difficulties.RemoveAt(difficultyIndex);
        }

        // Populate 
        foreach (Room<RoomData> room in Room<RoomData>.rooms)
        {
            if (room.GetValid() && !room.isStart)
            {
                //room.data.
            }
        }


        for (int i = 0; i < numTraps; ++i)
        {
            int trapIndex = PRNG.Range(0, normalRooms.Count);

            //validRooms[trapIndex].data.
        }

        Print();
    }

    private void Print()
    {
        string output = "";
        for (int i = 0; i < Floor<RoomData>.FLOOR_LENGTH; ++i)
        {
            string line = "";
            for (int j = 0; j < Floor<RoomData>.FLOOR_LENGTH; ++j)
            {
                //line += floor.floor[i, j].name;
                line += floor.floor[i, j].GetValid() ? "1" : "0";
            }
            output += line + "\n";
        }
        Debug.Log(output);
    }

    // Place chunks, enemies, secrets, traps
}
