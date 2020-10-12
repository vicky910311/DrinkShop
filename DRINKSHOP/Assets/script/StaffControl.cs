using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffControl 
{
    void UnlockStaff(int i,PlayerDataManager Player,GameDataManager GameData)
    {
        if ( Player.PlayerData.Money >= GameData.StaffDataList[i].UnlockCost && Player.PlayerData.Level >= GameData.StaffDataList[i].UnlockLevel)
        {
            Player.PlayerData.Money -= GameData.StaffDataList[i].UnlockCost;
            Player.PlayerData.HavetheStaff[i] = true;
        }
        else
        {
            Debug.Log("不符解鎖條件");
        }
        
    }
}
