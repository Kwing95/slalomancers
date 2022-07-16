using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public int difficulty = 0;
    public bool isSecret = false;
    public PlayerStats.Stat powerup;
    // Following vars are for chunks
    public Chunk[] chunks = new Chunk[5];
    // 0 1
    //  2
    // 3 4

    public RoomData(int numEnemies = -1)
    {
        List<int> vacancies = new List<int>();
        for (int i = 0; i < 5; ++i)
        {
            vacancies.Add(i);
            chunks[i] = new Chunk(true);
        }

        if(numEnemies == -1)
            numEnemies = PRNG.Range(3, 4); // 1-2, 1-3, 2-3, 2-4, 2-5

        for(int i = 0; i < numEnemies; ++i)
        {
            int chunkIndex = PRNG.Range(0, vacancies.Count);
            chunks[chunkIndex] = new Chunk();
            vacancies.RemoveAt(chunkIndex);
        }

        powerup = PlayerStats.RandomStat();
    }

    public void AddChest(Chest chest)
    {
        Chunk newChunk = new Chunk();
        newChunk.type = Chunk.Enemies.Chest;
        newChunk.chest = chest;

        chunks[2] = newChunk;
    }
}

public class Chunk
{
    public enum Enemies { None, Asteroid, Wyvern, Geyser, Bombardier, Trap, Chest }
    public Enemies type;
    public Chest chest;
    public Chunk(bool isVacant=false)
    {
        type = isVacant ? Enemies.None : (Enemies)PRNG.Range(1, 5);
    }

    public void SetTrap()
    {
        type = Enemies.Trap;
    }
}
public class Enemy
{

}
public class SlalomFloor : MonoBehaviour
{
    private string cheatSheet = "";
    public Floor<RoomData> floor;

    public const int NUM_ROOMS = 10;

    // FLOOR CONTENTS:
    // 1-3 traps per floor, 1-2 secret rooms per floor
    // 1-5 walls per room, 2-5 enemies per room

    public void Awake()
    {
        PassManager.ResetPasswords();
        floor = new Floor<RoomData>();

        // 1-3 secrets, 1-4 traps, {1, 2, ... NUM_ROOMS} distinct difficulties
        // int numSecrets = 0;
        int numSecrets = PRNG.Range(1, 3);
        int numTraps = PRNG.Range(1, 4);


        // Create normal and secret rooms
        floor.GenerateFloor(NUM_ROOMS + numSecrets);

        // Add secret rooms
        AddSecretRooms(numSecrets);

        // Use valid and non-secret rooms as list of candidates
        List<Room<RoomData>> normalRooms = new List<Room<RoomData>>();
        foreach (Room<RoomData> room in Room<RoomData>.rooms)
            if (room.GetValid() && !room.data.isSecret && !room.isStart)
                normalRooms.Add(room);

        // Designate unique difficulty modifier to each room
        AssignDifficulties(normalRooms);

        // Add sets of chests to normal rooms
        AddPartialChests(normalRooms, 2, 2);

        // Add traps
        AddTraps(normalRooms, numTraps);
        
        Debug.Log(cheatSheet);
    }

    private void AddTraps(List<Room<RoomData>> normalRooms, int numTraps)
    {
        List<Room<RoomData>> roomsWithoutTraps = new List<Room<RoomData>>();
        foreach (Room<RoomData> room in normalRooms)
            roomsWithoutTraps.Add(room);

        int[] trapIndexes = new int[] { 0, 1, 3, 4 }; // Exclude 2

        for (int i = 0; i < numTraps; ++i)
        {
            int roomIndex = PRNG.Range(0, roomsWithoutTraps.Count);
            int chunkIndex = PRNG.Range(0, trapIndexes.Length);
            cheatSheet += "Added trap to chunk " + trapIndexes[chunkIndex] + " in room " +
                roomsWithoutTraps[roomIndex].location.x.ToString() + ", " +
                roomsWithoutTraps[roomIndex].location.y.ToString() + "\n";
            roomsWithoutTraps[roomIndex].data.chunks[trapIndexes[chunkIndex]].type = Chunk.Enemies.Trap;
            roomsWithoutTraps.RemoveAt(roomIndex);
        }
    }

    private void AddSecretRooms(int numSecrets)
    {
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
            int secretIndex = PRNG.Range(0, deadEnds.Count); // Select room
            deadEnds[secretIndex].data = new RoomData(0); // Create room with no enemies
            deadEnds[secretIndex].data.isSecret = true; // Mark room as secret
            deadEnds[secretIndex].Clear();

            Chest newChest = new Chest();
            cheatSheet += "Added chest with code " + newChest.code + " to secret room " +
                deadEnds[secretIndex].location.x.ToString() + ", " +
                deadEnds[secretIndex].location.y.ToString() + "\n";

            deadEnds[secretIndex].data.AddChest(newChest);
            deadEnds.RemoveAt(secretIndex);
        }
    }

    private void AssignDifficulties(List<Room<RoomData>> normalRooms)
    {
        List<int> difficulties = new List<int>();
        for (int i = 0; i < NUM_ROOMS; ++i)
            difficulties.Add(i);

        foreach (Room<RoomData> room in normalRooms)
        {
            if(difficulties.Count > 0)
            {
                int difficultyIndex = PRNG.Range(0, difficulties.Count);
                if (difficultyIndex >= difficulties.Count || difficultyIndex == -1)
                {
                    Debug.Log(difficultyIndex + " / " + difficulties.Count);
                }
                room.data.difficulty = difficulties[difficultyIndex];
                difficulties.RemoveAt(difficultyIndex);
            }
        }
    }

    private void AddPartialChests(List<Room<RoomData>> normalRooms, int numSets, int chestsPerSet)
    {
        List<Room<RoomData>> roomsWithoutChests = new List<Room<RoomData>>();
        foreach (Room<RoomData> room in normalRooms)
            roomsWithoutChests.Add(room);

        for (int j = 0; j < numSets; ++j)
        {
            List<PartialChest> chests = PartialChest.GenerateSet(chestsPerSet);

            for (int i = 0; i < chestsPerSet; ++i)
            {
                PlayerStats.Stat perk = PlayerStats.RandomStat();

                // Create PartialChest from chests array
                PartialChest newChest = new PartialChest(chests[i].code, chests[0].fullCode, perk);
                newChest.group = j;
                int roomIndex = PRNG.Range(0, roomsWithoutChests.Count);
                cheatSheet += "Added partial chest " + chests[i].code + " / " + chests[0].fullCode + " to room " +
                    roomsWithoutChests[roomIndex].location.x.ToString() + ", " +
                    roomsWithoutChests[roomIndex].location.y.ToString() + "\n";
                roomsWithoutChests[roomIndex].data.AddChest(newChest);
                roomsWithoutChests.RemoveAt(roomIndex);
            }
        }
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
