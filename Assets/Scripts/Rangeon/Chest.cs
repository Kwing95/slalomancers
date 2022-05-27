using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest
{
    public string code;
    public bool used = false;
    public PlayerStats.Stat buff;

    public Chest(string _code, PlayerStats.Stat perk)
    {
        code = _code;
        buff = perk;
    }

}

public class PartialChest : Chest
{
    public string fullCode;
    public List<PartialChest> set;
    public PartialChest(string part, string full, PlayerStats.Stat perk) : base(part, perk)
    {
        fullCode = full;
    }

    public void Use()
    {
        // Put code somewhere player can see
        foreach(PartialChest chest in set)
            chest.used = true;
    }
    public static List<PartialChest> GenerateSet(int setSize)
    {
        List<PartialChest> chests = new List<PartialChest>();
        List<string> parts = new List<string>();
        string full = "";
        PlayerStats.Stat perk = RoomData.powerupTypes[PRNG.Range(0, RoomData.powerupTypes.Length)];

        for (int i = 0; i < setSize; ++i)
        {
            string part = "";

            for(int j = 0; j < i; ++j)
                part += "**";

            string format = "00";
            string digits = PRNG.Range(0, 100).ToString(format);
            part += digits;
            for (int j = i; j < setSize - 1; ++j)
                part += "**";

            parts.Add(part);
            full += digits;
        }

        for (int i = 0; i < setSize; ++i)
            chests.Add(new PartialChest(parts[i], full, perk));

        foreach (PartialChest chest in chests)
            chest.set = chests;
            

        return chests;
    }
}