using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkControl
{
    public DrinkDataList Drink;
    // public PlayerData Player;
    public int DevelopDrink(PlayerData Player)
    {
        int Select = -1;
        if (Player.Coin < 3 && Player.DrinkSum < Drink.DrinkData.Count && Player.Money >= Drink.DrinkUse.DevelopCost)
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
                if (Player.getHavetheDrink(i) != true)
                    NewDrink.Add(i);
            }
            int A = Random.Range(0, NewDrink.Count);
            Select = NewDrink[A];
            /* Player.HavetheDrink[Select] = true;
             Player.DrinkSum++;
             Player.CanMake.Add(Select);
             Debug.Log(Select + "研發成功");*/
        }
        else if (Player.DrinkSum == Drink.DrinkData.Count)
        {
            
            Debug.Log("收集完畢");
        }
        else
            Debug.Log("沒錢研發");


        return Select;
    }
    public void MakingDrink(int i, PlayerData Player,ref int Make,ref int MakeTime)
    {
        Make = GameManager.self.ReplenishAmount - Player.getDrinkinStock(i);
        if (Player.Money >= Make * Drink.DrinkData[i].Cost)
        {
            //Player.setDrinkinStock(i, Player.getDrinkinStock(i) + Make);
            Player.Money -= Make * Drink.DrinkData[i].Cost;
        }
        else
        {
            if (Drink.DrinkData[i].Cost != 0)
            {
                Make = Player.Money / Drink.DrinkData[i].Cost;
               // Player.setDrinkinStock(i, Player.getDrinkinStock(i) + Make);
                Player.Money -= Make * Drink.DrinkData[i].Cost;
            }
        }
         int g =GameManager.self.ghostin.transform.childCount;
        if(g == 0)
        {
            MakeTime = (int)(Drink.DrinkUse.normalmakePara * 5 * Make / (Player.StaffSum + 8) + 1);
        }
        else
        {
            MakeTime = (int)( Drink.DrinkUse.afraidmakePara * 5 * Make / (Player.StaffSum + 8) + g);
        }
         
    }
    public void makeAllcount(PlayerData Player,ref int t)
    {
        int makeamount = 0;
        for (int i = 0 ; i < Player.DrinkSum ; i++)
        {
            int a = Player.getCanMake(i);
            int Make = 0;
            Make = GameManager.self.ReplenishAmount - Player.getDrinkinStock(a);
            if(Make > 0)
            {
                makeamount += Make;
            }

        }
        Debug.Log("makeamount" + makeamount);
        t = makeamount /5 + 6;
    }
    public void makeAllcost(PlayerData Player)
    {
        for (int i = 0; i < Player.DrinkSum; i++)
        {
            int a = Player.getCanMake(i);
            if(Player.getDrinkinStock(a)< GameManager.self.ReplenishAmount)
            {
                int Make = GameManager.self.ReplenishAmount - Player.getDrinkinStock(a);
                if (Player.Money >=   Make* Drink.DrinkData[a].Cost)
                {
                    Player.setDrinkinStock(a, GameManager.self.ReplenishAmount);
                    Player.Money -= Make * Drink.DrinkData[a].Cost;
                }
                else
                {
                    if (Drink.DrinkData[a].Cost != 0)
                    {
                        Make = Player.Money / Drink.DrinkData[a].Cost;
                        // Player.setDrinkinStock(i, Player.getDrinkinStock(i) + Make);
                        Player.Money -= Make * Drink.DrinkData[a].Cost;
                        Player.setDrinkinStock(a, Player.getDrinkinStock(a) + Make) ;
                    }
                }
                
            }
            
        }
    }
    /*public void havenoDrink(PlayerData Player,ref string Narrate,ref string Short)
    {
        for(int i=0;i< Player.DrinkSum;i++)
        {
            int a = Player.getCanMake(i);
            if(Player.getDrinkinStock(a) == 0)
            {
                Narrate = Drink.DrinkData[a].Name+"缺貨了";
                Short = "飲料缺貨中";
            }

        }
    }*/
}
