using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ClientControl 
{
    //public PlayerData Player;
    public DrinkDataList Drink;
    public ClientDataList Client;
   public void SelltheDrink(PlayerData Player,ref int c,ref bool isnew,ref int d)
    {
        int Select;
        List<int> CanSell = new List<int>();
        for (int i = 0; i<Player.countHavetheDrink(); i++)
        {
            if (Player.getHavetheDrink(i))
                CanSell.Add(i);
        }
        int a = UnityEngine.Random.Range(0, CanSell.Count);
        Select = CanSell[a];
        Debug.Log(Select);
        if (Drink.DrinkData[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
        {
            c = Select - (Drink.DrinkData.Count - Client.ClientData.Count);
        }
        else
        {
            c = UnityEngine.Random.Range(0, Player.Level * 3);
        }
        Debug.Log(c + "來");
        if (Player.getDrinkinStock(Select) > 0)
        {
            Player.setDrinkinStock(Select, Player.getDrinkinStock(Select) - 1);
            Player.DrinkSell++;
            Player.Money += Drink.DrinkData[Select].Price;
            if (Player.getHavetheClient(c) == false)
            {
                Player.setHavetheClient(c, true);
                Player.ClientSum++;
                isnew = true;
                //Testing.self.AddCientMenu(c);
                Debug.Log("新顧客"+c );
            }
            Debug.Log(c + "買" + Select);
        }
        d = Select;
        
    }
    public void WhenNotPlayingSell(PlayerData Player)
    {
        TimeSpan T;
        int TempMoney = 0;
        int TempSell = 0;
        if (Client.ComeTime.Leave <= 0)
        {
            Client.ComeTime.Leave = 1;
        }
        T = Player.ThisOpenTime - Player.LastEndTime;
        List<int> CanSell = new List<int>();
        for (int j = 0; j < Player.countHavetheDrink(); j++)
        {
            if (Player.getHavetheDrink(j))
                CanSell.Add(j);
        }
        for (int i = 0; i < (int)T.TotalMinutes/Client.ComeTime.Leave; i++)
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
            if (Player.getDrinkinStock(Select) > 0)
            {
                Player.setDrinkinStock(Select, Player.getDrinkinStock(Select)-1);
                Player.DrinkSell++;
                Player.Money += Drink.DrinkData[Select].Price;
                if (Player.getHavetheClient(c) == false)
                {
                    Player.setHavetheClient(c,true);
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
    public void PromoteSell(ref int Min,ref int Max,PromoteType p)
    {
        if (p == PromoteType.Manual)
        {
            Min = Client.ComeTime.ManualMin;
            Max = Client.ComeTime.ManualMax;
        }
        else if (p == PromoteType.AD)
        {
            Min = Client.ComeTime.ADMin;
            Max = Client.ComeTime.ADMax;
        }
        else
        {
            Min = Client.ComeTime.NormalMin;
            Max = Client.ComeTime.NormalMax;
        }
    }
    public enum PromoteType
    {
        Normal,
        Manual,
        AD
    }
}
