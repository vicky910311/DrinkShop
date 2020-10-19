using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.RestService;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager self;
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
    }
    public void Default()
    {
       Player.DrinkSum = 0;
       Player.HavetheDrink[0] = true;
       Player.HavetheDrink[1] = true;
       Player.HavetheDrink[2] = true;
      for (int i =3;i<Player.HavetheDrink.Count;i++)
       {
           Player.HavetheDrink[i] = false;
       }
       for (int i = 0; i < Player.HavetheDrink.Count; i++)
       {
           if(Player.HavetheDrink[i] == true)
               Player.DrinkSum++;
       }

       Player.StaffSum = 0;
       Player.HavetheStaff[0] = true;
       for (int i = 1; i < Player.HavetheStaff.Count; i++)
       {
           Player.HavetheStaff[i] = false;
       }
       for (int i = 0; i < Player.HavetheStaff.Count; i++)
       {
           if (Player.HavetheStaff[i] == true)
               Player.StaffSum++;
       }
       for (int i = 0; i < Player.HavetheClient.Count; i++)
       {
           Player.HavetheClient[i] = false;
       }
        Player.ClientSum = 0;
        Player.Money = 12000;
        Player.Level = 1;
        for (int i = 0; i < Player.DrinkinStock.Count; i++)
        {
            Player.DrinkinStock[i] = 10;
        }
        Player.DrinkSell = 0;
        Player.AddStockLimit = 0;
        Player.DeleteAD = false;
        Player.FirstTime = false;
        Player.Coin = 0;
        Player.ThisOpenTime = DateTime.Now;
        Player.LastEndTime = DateTime.Now;
        Player.CatchGhost = 0;
        Player.CatchSleep = 0;
        for (int i = 0; i < Player.HavetheDrink.Count; i++)
        {
            if (Player.HavetheDrink[i] == true)
                Player.CanMake.Add(i);
        }
    }
}
