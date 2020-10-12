using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientControl 
{
   public void SelltheDrink(PlayerDataManager Player,GameDataManager GameData)
    {
        int Select;
        List<int> CanSell = new List<int>();
        for (int i = 0; i<Player.PlayerData.HavetheDrink.Count; i++)
        {
            if (Player.PlayerData.HavetheDrink[i])
                CanSell.Add(i);
        }
        int a = UnityEngine.Random.Range(0, CanSell.Count);
        Select = CanSell[a];
        Debug.Log(Select);
        int c;
        if (GameData.DrinkDataList[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
        {
            c = Select - (GameData.DrinkDataList.Count - GameData.ClientDataList.Count);
        }
        else
        {
            c = UnityEngine.Random.Range(0, Player.PlayerData.Level * 3);
        }
        Debug.Log(c);
        if (Player.PlayerData.DrinkinStock[Select] > 0 )
        {
            Player.PlayerData.DrinkinStock[Select]--;
            Player.PlayerData.DrinkSell++;
            Player.PlayerData.Money += GameData.DrinkDataList[Select].Price;
            if (Player.PlayerData.HavetheClient[c] == false)
            {
                Player.PlayerData.HavetheClient[c] = true;
                Player.PlayerData.ClientSum++;
            }            
        }
    }
    public void WhenNotPlayingSell(PlayerDataManager Player, GameDataManager GameData)
    {
        TimeSpan T;
        int TempMoney = 0;
        int TempSell = 0;
        T = Player.PlayerData.ThisOpenTime - Player.PlayerData.LastEndTime;
        List<int> CanSell = new List<int>();
        for (int j = 0; j < Player.PlayerData.HavetheDrink.Count; j++)
        {
            if (Player.PlayerData.HavetheDrink[j])
                CanSell.Add(j);
        }
        for (int i = 0; i < (int)T.TotalMinutes/10; i++)
        {
            int Select; 
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            Debug.Log(Select);
            int c;
            if (GameData.DrinkDataList[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
            {
                c = Select - (GameData.DrinkDataList.Count - GameData.ClientDataList.Count);
            }
            else
            {
                c = UnityEngine.Random.Range(0, Player.PlayerData.Level * 3);
            }
            Debug.Log(c);
            if (Player.PlayerData.DrinkinStock[Select] > 0)
            {
                Player.PlayerData.DrinkinStock[Select]--;
                Player.PlayerData.DrinkSell++;
                Player.PlayerData.Money += GameData.DrinkDataList[Select].Price;
                if (Player.PlayerData.HavetheClient[c] == false)
                {
                    Player.PlayerData.HavetheClient[c] = true;
                    Player.PlayerData.ClientSum++;
                }
            }
            else
            {
                TempMoney += (GameData.DrinkDataList[Select].Price - GameData.DrinkDataList[Select].Cost);
                TempSell++;
            }
        }
    }
}
