using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyService : MonoBehaviour
{

    public Text lobbyName;
    public Text customSeed;

    // Start is called before the first frame update
    void Start()
    {
        RefreshLobby();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshLobby()
    {
        PRNG.UnixSeed(0);
        lobbyName.text = PRNG.GetLobbyName();
    }

    public void ForceSeed()
    {
        bool errorState = false;
        try
        {
            PRNG.ForceSeed(Int32.Parse(customSeed.text));
        }
        catch(FormatException e)
        {
            ToastService.Toast("ERROR PARSING SEED");
            customSeed.text = "";
            errorState = true;
        }
        if (!errorState)
        {
            SceneService.instance.LoadScene("Remote");
        }
    }

}
