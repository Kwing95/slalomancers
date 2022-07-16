using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassManager : MonoBehaviour
{
    public class StatUpgrade
    {
        public string password;
        public PlayerStats.Stat stat;
        public bool used = false;

        public StatUpgrade(string _password, PlayerStats.Stat _perk)
        {
            password = _password;
            stat = _perk;
        }
    }

    public static PassManager instance;
    public PlayerStats player1;
    public static List<StatUpgrade> validPasswords;

    public static void AddPassword(string newPass, PlayerStats.Stat perk)
    {
        if (validPasswords == null)
            validPasswords = new List<StatUpgrade>();

        if (!newPass.Contains('*'))
            validPasswords.Add(new StatUpgrade(newPass, perk));
    }

    public static void ResetPasswords()
    {
        validPasswords = new List<StatUpgrade>();
    }

    public static void TryPassword(string password)
    {
        bool invalid = true;
        foreach(StatUpgrade perk in validPasswords)
        {
            if (password == perk.password)
            {
                invalid = false;
                if (perk.used)
                    ToastService.Toast("Password already used");
                else
                {
                    ToastService.Toast("Upgraded " + perk.stat.ToString());
                    if (instance.player1)
                        instance.player1.BuffStat(perk.stat);
                    perk.used = true;
                }
                break;
            }
        }

        if (invalid)
            ToastService.Toast("Invalid password");
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
