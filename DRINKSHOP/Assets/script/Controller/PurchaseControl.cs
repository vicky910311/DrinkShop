using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseControl
{
    public void AddtheMoney(PlayerData Player,int Add)
    {
        if (Add == 10000)
        {
            Player.Money += Add;
        }
        else if (Add == 30000)
        {
            Player.Money += Add;
        }
        else if (Add == 50000)
        {
            Player.Money += Add;
        }
        else if (Add == 100000)
        {
            Player.Money += Add;
        }
    }
    public void DeletingAD(PlayerData Player)
    {
        if(Player.DeleteAD == false)
        {
            Player.DeleteAD = true;
            Debug.Log("免除廣告");
        }
        else
        {
            Debug.Log("已免除廣告");
        }
    }
    public void AddingStockLimit(PlayerData Player)
    {
        if(Player.AddStockLimit >= 75)
        {
            Debug.Log("已達上限");
        }
        else
        {
            Player.AddStockLimit += 25;
        }
    }
}
