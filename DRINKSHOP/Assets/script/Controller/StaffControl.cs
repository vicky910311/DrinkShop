using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffControl 
{
   // public PlayerData Player;
    public StaffDataList Staff;
    public void UnlockStaff(int i, PlayerData Player,ref bool b)
    {
        if (Player.Money >= Staff.StaffData[i].UnlockCost && Player.Level >= Staff.StaffData[i].UnlockLevel)
        {
            Player.Money -= Staff.StaffData[i].UnlockCost;
            //Player.setHavetheStaff(i, true);
            b = true;
        }

        else
        {
            Debug.Log("不符解鎖條件");
            b = false;

        }
        
    }
}
