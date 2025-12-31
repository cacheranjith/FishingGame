using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ingame; // in-game panel assgined
    public bool isgamestart = false; // bool value to check game is started or not
    public int counter;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        ingame.SetActive(false); ///in-game panel will not be shown   
        UIManager.Instance.SettingsUI.SetActive(false);

    }
    private void Update()
    {
        UIManager.Instance.SavePlayer(); ///game will be saved auto-saved every second
    }
    public void StartGame()
    {
        UIManager.Instance.CoincollectUI.SetActive(false);  //coin collect panel will not be shown
        Time.timeScale = 1; // timescale value is used to run the game if its '1' the game will run
        isgamestart = true; //bool value to check game is running or not
        UIManager.Instance.HookUpdate();
        UIManager.Instance.depthupdate();
        ingame.SetActive(true); // in game panel will be shown
    }
    public void StopGame()
    {
        Time.timeScale = 0; // timescale to stop the game
        isgamestart = false; //bool
        //FishController.Instance.FishDestory();
        counter = 0;
    }
    public void count()
    {
        counter = counter + 1;
        Debug.Log("gm" + counter);
    }
    public void showmenu()
    {
        Tapbuttonscript.instance.ShowMenu(); //tap to start script will be called from here
        UIManager.Instance.ResetFishCount();
        //FishController.Instance.LmtRched = false;
    }
}
