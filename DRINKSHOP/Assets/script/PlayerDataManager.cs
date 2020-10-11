using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerDataManager
{
    public PlayerData PlayerData = new PlayerData();
    public void Default()
    {
       PlayerData.HavetheDrink[0] = true;
       PlayerData.HavetheDrink[1] = true;
       PlayerData.HavetheDrink[2] = true;
      for (int i =3;i<PlayerData.HavetheDrink.Count;i++)
       {
           PlayerData.HavetheDrink[i] = false;
       }
       for (int i = 0; i < PlayerData.HavetheDrink.Count; i++)
       {
           if(PlayerData.HavetheDrink[i] == true)
               PlayerData.DrinkSum++;
       }

      /* PlayerData.HavetheStaff[0] = true;

       for (int i = 1; i < PlayerData.HavetheStaff.Count; i++)
       {
           PlayerData.HavetheStaff[i] = false;
       }
       for (int i = 0; i < PlayerData.HavetheStaff.Count; i++)
       {
           if (PlayerData.HavetheStaff[i] == true)
               PlayerData.StaffSum++;
       }
       for (int i = 0; i < PlayerData.HavetheClient.Count; i++)
       {
           PlayerData.HavetheClient[i] = false;
       }*/
        PlayerData.ClientSum = 0;
        PlayerData.Money = 12000;
        PlayerData.Level = 1;
        /*for (int i = 0; i < PlayerData.DrinkinStock.Count; i++)
        {
            PlayerData.DrinkinStock[i] = 10;
        }*/
        PlayerData.DrinkSell = 0;
        PlayerData.AddStockLimit = 0;
        PlayerData.DeleteAD = false;
        PlayerData.FirstTime = false;
        PlayerData.Coin = 0;
        PlayerData.ThisOpenTime = DateTime.Now;
        PlayerData.LastEndTime = DateTime.Now;
    }
}
