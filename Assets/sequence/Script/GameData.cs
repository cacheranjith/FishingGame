using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // temporary file to store game data
    public static GameData Instance;
    public int dptlvl;
    public int dptfare;
    public int coins;
    public int hookfare;
    public int hooklvl;
    public int maxfishc;
    private void awake()
    {
        Instance = this;
    }

    public GameData(UIManager player)
    {
        dptlvl = player.depthlvl;
        dptfare = player.dpth_upt_fare;
        coins = player.coins;
        hookfare = player.hook_upt_fare;
        hooklvl = player.hooklvl; 
        maxfishc = player.maxfishc;
        //Debug.Log("depthlvl" + dptlvl + "dptfare" + dptfare + "coins" +  coins + "hook upt fare" + hookfare + "hook level" + hooklvl + "maxfishc" + maxfishc);    
    }
}
