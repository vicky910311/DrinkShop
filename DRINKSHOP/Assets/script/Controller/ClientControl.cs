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
                Testing.self.AddCientMenu(c);
                Debug.Log("新顧客"+c );
            }
            Debug.Log(c + "買" + Select);
        }
        d = Select;
        
    }
    public void WhenNotPlayingSell(PlayerData Player,ref int TempMoney,ref int TempSell,ref string narrate)
    {
        TimeSpan T;
        int newsell = 0, newc = 0, newearn = 0;
        if (Client.ComeTime.Leave <= 0)
        {
            Client.ComeTime.Leave = 10;
        }
        T = Player.ThisOpenTime - Player.LastEndTime;
        List<int> CanSell = new List<int>();
        for (int j = 0; j < Player.countHavetheDrink(); j++)
        {
            if (Player.getHavetheDrink(j))
                CanSell.Add(j);
        }
        int LeaveSell = Mathf.Clamp((int)(T.TotalMinutes / Client.ComeTime.Leave),0,500);
        Debug.Log("離開測試" + LeaveSell);
        for (int i = 0; i < LeaveSell; i++)
        {
            int Select; 
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            //Debug.Log(Select);
            int c;

            //Debug.Log(c);
            if (Player.getDrinkinStock(Select) > 0)
            {
                if (Drink.DrinkData[Select].isSpecial == true && (int)UnityEngine.Random.Range(0, 10) == 0)
                {
                    c = Select - (Drink.DrinkData.Count - Client.ClientData.Count);
                }
                else
                {
                    c = UnityEngine.Random.Range(0, Player.Level * 3);
                }
                Player.setDrinkinStock(Select, Player.getDrinkinStock(Select)-1);
                
                newsell++;
               
                newearn += Drink.DrinkData[Select].Price;
                if (Player.getHavetheClient(c) == false)
                {
                    newc++;
                    Testing.self.AddCientMenu(c);
                    Player.setHavetheClient(c,true);
                    
                }
            }
            else
            {
                TempMoney += (Drink.DrinkData[Select].Price - Drink.DrinkData[Select].Cost);
                TempSell++;
            }
        }
        Player.DrinkSell += newsell;
        Player.Money += newearn;
        Player.ClientSum += newc;
        narrate = "在離開期間\n賣出" + newsell +"杯，賺了"+ newearn + "，新客人" + newc +"位\n因為庫存不足少賺"+ TempMoney;
        Debug.Log(narrate);
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
