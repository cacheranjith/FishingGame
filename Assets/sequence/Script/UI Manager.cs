using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text fishcounter;

    public TMP_Text coincounter;

    public int fishcount = 0;

    public int coins =500;

    public int basefare = 500;

    public int dpth_upt_fare;

    public int hook_upt_fare;

    public int depthlvl = 1;

    public int hooklvl = 1;

    public float maxdpth;

    public TMP_Text depthfare;

    public TMP_Text hookfare;

    public Button dpthbtn;

    public Button hookbtn;

    public GameObject CoincollectUI;

    public GameObject SettingsUI;

    public int maxfishc = 1;

    public GameObject inventory;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CoincollectUI.SetActive(false); // coinpanel not shown
        inventory.SetActive(false); 
        GameData data = SaveSystem.LoadPlayer(); // loads the game data if exist
        depthlvl = data.dptlvl; //loads the depth lvl
        dpth_upt_fare = data.dptfare; // loads the depth fare
        coins = data.coins; // loads the coin  
        hooklvl = data.hooklvl;
        hook_upt_fare= data.hookfare;
        maxfishc = data.maxfishc;
        maxdpth = 10 + ((depthlvl - 1) * 5); // depth update equation
        if(maxdpth !=0)
        {
            PlayerController.Instance.maxDistance = maxdpth; // assgining depth value in player controller script
        }
        fishcounter.text = fishcount.ToString() + "/" + maxfishc.ToString(); 
        coincounter.text = coins.ToString();
        depthfare.text = dpth_upt_fare.ToString();
        hookfare.text = hook_upt_fare.ToString();


    }

    void Update()
    {
        dpth_upt_fare = basefare + (300 * depthlvl - 1);
        hook_upt_fare = basefare + (500 * hooklvl - 1);
        if (fishcount >= maxfishc)
        {
            //FishController.Instance.LmtRched = true;
        }
        if (dpth_upt_fare == coins)
        {
            dpthbtn.interactable = true;

        }
        else if (dpth_upt_fare < coins)
        {
            dpthbtn.interactable = true;
        }
        else
        {
            dpthbtn.interactable = false;   
        }

        if (hook_upt_fare == coins)
        {
            hookbtn.interactable = true;

        }
        else if(hook_upt_fare < coins)
        {
            hookbtn.interactable = true;

        }
        else
        {
            hookbtn.interactable = false;
        }
    }

    public void LoadPlayer()
    {
        Debug.Log("loaded");
    }

    public void CoinUpdate()
    {
        coins = coins + (fishcount * 100); // coin calculation for non-ad 
        Debug.Log("before close");
        coincounter.text = coins.ToString();
        CoincollectUI.SetActive(false);
        GameManager.instance.showmenu();
    }

    public void CoinAdUpdate(int cc)
    {
        coins = coins + (fishcount * 300); // coin calculation for ad 
        Debug.Log("before close");
        coincounter.text = coins.ToString();
        CoincollectUI.SetActive(false);
    }
    public void IncreaseFishCount()
    {
        fishcount = fishcount + 1;
        fishcounter.text = fishcount.ToString() + "/" + maxfishc.ToString(); 
    }

    public void ResetFishCount() 
    {
        fishcount = 0;
        fishcounter.text = fishcount.ToString();
    }

    public void depthupdate()
    {
        
        // depth update button function
        if (dpthbtn.interactable == true)
        {
            Debug.Log("crtdpth" + depthlvl);
            depthlvl = depthlvl + 1;
            PlayerController.Instance.maxDistance = PlayerController.Instance.maxDistance + 5;
            coins = coins - dpth_upt_fare;
            coincounter.text = coins.ToString();
        }
        depthfare.text = dpth_upt_fare.ToString();
    }

    public void HookUpdate()
    {
        if (hookbtn.interactable == true)
        {
            hooklvl = hooklvl + 1;
            maxfishc = maxfishc + 1;
            Debug.Log("hook value" +maxfishc);
            coins = coins - hook_upt_fare;
            coincounter.text = coins.ToString();
        }
        hookfare.text = hook_upt_fare.ToString();
    }

    public void Settings()
    {
        SettingsUI.SetActive(true);
    }

    public void CloseSettings()
    { 
        SettingsUI.SetActive(false);
    }

    public void openinventory()
    {
        inventory.SetActive(true);
    }

    public void closeinventory()
    {
        inventory.SetActive(false);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("saved");
    }

    public void ResetData()
    {
        depthlvl = 0;
        coins = 500;
        dpth_upt_fare = basefare;
        Debug.Log("updated");
    }
}
