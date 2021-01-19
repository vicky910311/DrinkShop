using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager self;
    private SaveandLoad saveandLoad = new SaveandLoad();
    public PlayerData Player;
    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
        else if (this != self)
        {
            Destroy(gameObject);
        }
       if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsonplayersave")) != null)
       {
           saveandLoad.Load();
           Player = saveandLoad.Player;
           Debug.Log("Loading");
       }
        /*if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("notsave")) == null)
        {
            Debug.Log("NoFound");
        }*/
    }
    public void Default()
    {
        Player.DrinkSum = 1;
        Player.StaffSum = 0;
        Player.ClientSum = 0;
        Player.Money = 12000;
        Player.Level = 1;
        Player.DrinkSell = 0;
        Player.AddStockLimit = 0;
        Player.DeleteAD = false;
        Player.FirstTime = true;
        Player.Coin = 0;
        Player.ThisOpenTime = DateTime.Now;
        Player.LastEndTime = DateTime.Now;
        Player.CatchGhost = 0;
        Player.CatchSleep = 0;
    }
}
