using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkControl
{
    public int DevelopDrink(GameDataManager Drink,PlayerDataManager Player)
    {
        int Select = -1;
        if (Player.PlayerData.Coin < 3 && Player.PlayerData.DrinkSum < Drink.DrinkDataList.Count)
        {
            Player.PlayerData.Money -= Drink.drinkUse.DevelopCost;
            Select = Random.Range(0, Drink.DrinkDataList.Count);
            if (Player.PlayerData.HavetheDrink[Select] == true)
                Player.PlayerData.Coin++;
            else
            {
                Player.PlayerData.HavetheDrink[Select] = true;
                Player.PlayerData.DrinkSum++;
            }
        }
        else if (Player.PlayerData.Coin >= 3 && Player.PlayerData.DrinkSum < Drink.DrinkDataList.Count)
        {
            List<int> NewDrink = new List<int>();
            for (int i = 0; i < Drink.DrinkDataList.Count; i++)
            {
                if (Player.PlayerData.HavetheDrink[i] != true)
                    NewDrink.Add(i);
            }
            int A = Random.Range(0, NewDrink.Count);
            Select = NewDrink[A];
            Player.PlayerData.HavetheDrink[Select] = true;
            Player.PlayerData.DrinkSum++;
        }
        else
            Debug.Log("收集完畢");
        return Select;
    }
}
