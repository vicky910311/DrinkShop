using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffControl 
{
    void UnlockStaff(int i,PlayerData Player,StaffDataList Staff)
    {
        if ( Player.Money >= Staff.StaffData[i].UnlockCost && Player.Level >= Staff.StaffData[i].UnlockLevel)
        {
            Player.Money -= Staff.StaffData[i].UnlockCost;
            Player.HavetheStaff[i] = true;
        }
        else
        {
            Debug.Log("不符解鎖條件");
        }
        
    }
}
