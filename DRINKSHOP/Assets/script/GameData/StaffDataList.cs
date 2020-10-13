using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffData", menuName = "CreateGameData/StaffDataList")]
public class StaffDataList : ScriptableObject
{
    public List<StaffData> StaffData;
    public void StaffByLevel()
    {
        for (int i = 0; i < StaffData.Count; i++)
        {
            for (int j = 0; j < StaffData.Count - i - 1; j++)
            {
                if (StaffData[j].UnlockLevel > StaffData[j + 1].UnlockLevel)
                {
                    StaffData temp;
                    temp = StaffData[j];
                    StaffData[j] = StaffData[j + 1];
                    StaffData[j + 1] = temp;
                }
            }
        }
    }
}
