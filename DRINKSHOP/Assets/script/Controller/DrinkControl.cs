using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkControl
{
    public int DevelopDrink(DrinkDataList Drink,PlayerData Player)
    {
        int Select = -1;
        if (Player.Money >= Drink.DrinkUse.DevelopCost)
        {
            if (Player.Coin < 3 && Player.DrinkSum < Drink.DrinkData.Count)
            {
                Player.Money -= Drink.DrinkUse.DevelopCost;
                Select = Random.Range(0, Drink.DrinkData.Count);
                if (Player.HavetheDrink[Select] == true)
                    Player.Coin++;
                else
                {
                    Player.HavetheDrink[Select] = true;
                    Player.DrinkSum++;
                }
            }
            else if (Player.Coin >= 3 && Player.DrinkSum < Drink.DrinkData.Count)
            {
                Player.Coin = 0;
                List<int> NewDrink = new List<int>();
                for (int i = 0; i < Drink.DrinkData.Count; i++)
                {
                    if (Player.HavetheDrink[i] != true)
                        NewDrink.Add(i);
                }
                int A = Random.Range(0, NewDrink.Count);
                Select = NewDrink[A];
                Player.HavetheDrink[Select] = true;
                Player.DrinkSum++;
            }
            else
                Debug.Log("收集完畢");
        }
        else
            Debug.Log("沒錢研發");
        
        
        return Select;
    }
}
