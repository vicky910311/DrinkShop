using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DrinkData", menuName = "CreateGameData/DrinkDataList")]
public class DrinkDataList : ScriptableObject
{
    public List<DrinkData> DrinkData;
    public DrinkUse DrinkUse;
    public void DrinkByChara()
    {

        for (int i = 0; i < DrinkData.Count; i++)
        {
            for (int j = 0; j < DrinkData.Count - i - 1; j++)
            {
                if (DrinkData[j].Chara > DrinkData[j + 1].Chara)
                {
                    DrinkData temp;
                    temp = DrinkData[j];
                    DrinkData[j] = DrinkData[j + 1];
                    DrinkData[j + 1] = temp;
                }
            }
        }
    }
}
