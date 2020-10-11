using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int a = Random.Range(0, CanSell.Count);
        Select = CanSell[a];
        int c = Random.Range(0,Player.PlayerData.Level*3);
        if (Player.PlayerData.DrinkinStock[Select] > 0 )
        {
            Player.PlayerData.DrinkinStock[Select]--;
            Player.PlayerData.Money += GameData.DrinkDataList[Select].Price;
            if (Player.PlayerData.HavetheClient[c] == false)
            {
                Player.PlayerData.HavetheClient[c] = true;
                Player.PlayerData.ClientSum++;
            }
               
        }
    }
}
