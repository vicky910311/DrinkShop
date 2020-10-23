using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffControl 
{
   // public PlayerData Player;
    public StaffDataList Staff;
    void UnlockStaff(int i, PlayerData Player)
    {
        if (Player.Money >= Staff.StaffData[i].UnlockCost && Player.Level >= Staff.StaffData[i].UnlockLevel)
        {
            Player.Money -= Staff.StaffData[i].UnlockCost;
            Player.setHavetheStaff(i, true);
        }

        else
        {
            Debug.Log("不符解鎖條件");
        }
        
    }
}
