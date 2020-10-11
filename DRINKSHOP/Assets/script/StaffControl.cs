using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffControl 
{
    void UnlockStaff(int i,PlayerDataManager Player)
    {
        Player.PlayerData.HavetheStaff[i] = true;
    }
}
