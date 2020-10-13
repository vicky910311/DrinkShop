using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientControl 
{
   public void SelltheDrink(PlayerData Player,DrinkDataList Drink,ClientDataList Client)
    {
        int Select;
        List<int> CanSell = new List<int>();
        for (int i = 0; i<Player.HavetheDrink.Count; i++)
        {
            if (Player.HavetheDrink[i])
                CanSell.Add(i);
        }
        int a = UnityEngine.Random.Range(0, CanSell.Count);
        Select = CanSell[a];
        Debug.Log(Select);
        int c;
        if (Drink.DrinkData[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
        {
            c = Select - (Drink.DrinkData.Count - Client.ClientData.Count);
        }
        else
        {
            c = UnityEngine.Random.Range(0, Player.Level * 3);
        }
        Debug.Log(c);
        if (Player.DrinkinStock[Select] > 0 )
        {
            Player.DrinkinStock[Select]--;
            Player.DrinkSell++;
            Player.Money += Drink.DrinkData[Select].Price;
            if (Player.HavetheClient[c] == false)
            {
                Player.HavetheClient[c] = true;
                Player.ClientSum++;
            }            
        }
    }
    public void WhenNotPlayingSell(PlayerData Player, DrinkDataList Drink, ClientDataList Client)
    {
        TimeSpan T;
        int TempMoney = 0;
        int TempSell = 0;
        T = Player.ThisOpenTime - Player.LastEndTime;
        List<int> CanSell = new List<int>();
        for (int j = 0; j < Player.HavetheDrink.Count; j++)
        {
            if (Player.HavetheDrink[j])
                CanSell.Add(j);
        }
        for (int i = 0; i < (int)T.TotalMinutes/10; i++)
        {
            int Select; 
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            Debug.Log(Select);
            int c;
            if (Drink.DrinkData[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
            {
                c = Select - (Drink.DrinkData.Count - Client.ClientData.Count);
            }
            else
            {
                c = UnityEngine.Random.Range(0, Player.Level * 3);
            }
            Debug.Log(c);
            if (Player.DrinkinStock[Select] > 0)
            {
                Player.DrinkinStock[Select]--;
                Player.DrinkSell++;
                Player.Money += Drink.DrinkData[Select].Price;
                if (Player.HavetheClient[c] == false)
                {
                    Player.HavetheClient[c] = true;
                    Player.ClientSum++;
                }
            }
            else
            {
                TempMoney += (Drink.DrinkData[Select].Price - Drink.DrinkData[Select].Cost);
                TempSell++;
            }
        }
    }
}
