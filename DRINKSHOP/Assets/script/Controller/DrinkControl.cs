using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkControl
{
    public DrinkDataList Drink;
    public PlayerData Player;
    public int DevelopDrink()
    {
        int Select = -1;
        if (Player.Money >= Drink.DrinkUse.DevelopCost)
        {
            if (Player.Coin < 3 && Player.DrinkSum < Drink.DrinkData.Count)
            {
                Player.Money -= Drink.DrinkUse.DevelopCost;
                Select = Random.Range(0, Drink.DrinkData.Count);
               /* if (Player.HavetheDrink[Select] == true)
                {
                    Player.Coin++;
                    Debug.Log("代幣增加");
                }
                else
                {
                    Player.HavetheDrink[Select] = true;
                    Player.DrinkSum++;
                    Player.CanMake.Add(Select);
                    Debug.Log(Select + "研發成功");
                }*/
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
               /* Player.HavetheDrink[Select] = true;
                Player.DrinkSum++;
                Player.CanMake.Add(Select);
                Debug.Log(Select + "研發成功");*/
            }
            else
                Debug.Log("收集完畢");
        }
        else
            Debug.Log("沒錢研發");
        
        
        return Select;
    }
    public void MakingDrink(int i)
    {
        int Make = Drink.DrinkUse.StockLimit + Player.AddStockLimit - Player.DrinkinStock[i];
        if (Player.Money >= Make * Drink.DrinkData[i].Cost)
        {
            Player.DrinkinStock[i] += Make;
            Player.Money -= Make * Drink.DrinkData[i].Cost;
        }
        else
        {
            Make = Player.Money / Drink.DrinkData[i].Cost;
            Player.DrinkinStock[i] += Make;
            Player.Money -= Make * Drink.DrinkData[i].Cost;
        }
        float MakeTime = Make * 0.5f / Player.StaffSum;
    }
        

}
