using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest
{
    public string code;
    public int group;
    public PlayerStats.Stat buff;
    //public Sample(string str) : this(int.Parse(str)) { }

    public Chest() : this(PlayerStats.RandomStat())
    {

    }

    public Chest(PlayerStats.Stat perk, string _code = "", int _group=-1)
    {
        code = _code == "" ? GeneratePass() : _code;
        buff = perk;
        group = _group;

        PassManager.AddPassword(code, perk);
    }

    protected static string GeneratePass()
    {
        string format = "00";
        return PRNG.Range(0, 100).ToString(format);
    }

}

// Partial chest contains "code" which is a part and "fullCode" which is all of it
public class PartialChest : Chest
{
    public string fullCode;
    public List<PartialChest> set;
    public PartialChest(string part, string full, PlayerStats.Stat perk) : base(perk, part)
    {
        PassManager.AddPassword(full, perk);
        fullCode = full;
    }

    public static List<PartialChest> GenerateSet(int setSize)
    {
        List<PartialChest> chests = new List<PartialChest>();
        List<string> parts = new List<string>();
        string full = "";

        for (int i = 0; i < setSize; ++i)
        {
            string part = "";

            for(int j = 0; j < i; ++j)
                part += "**";

            string digits = GeneratePass();
            part += digits;
            for (int j = i; j < setSize - 1; ++j)
                part += "**";

            parts.Add(part);
            full += digits;
        }

        for (int i = 0; i < setSize; ++i)
            chests.Add(new PartialChest(parts[i], full, PlayerStats.RandomStat()));

        foreach (PartialChest chest in chests)
            chest.set = chests;
            

        return chests;
    }
}