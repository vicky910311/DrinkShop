using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData",menuName ="CreateGameData")]
public class GameDataManager : ScriptableObject
{
    public List<DrinkData> DrinkDataList;
    public List<ClientData> ClientDataList;
    public List<StaffData> StaffDataList;
    public DrinkUse drinkUse;
    public ClientComeTime ComeTime;
    public List<LevelUpData> LevelUpDataList;
    public List<LuckyData> LuckyDataList;

    public void DrinkByChara()
    {
        for (int i = 0; i < DrinkDataList.Count; i++)
        {
            for (int j = 0; j < DrinkDataList.Count - i - 1; j++)
            {
                if (DrinkDataList[j].Chara > DrinkDataList[j + 1].Chara)
                {
                    DrinkData temp;
                    temp =DrinkDataList[j];
                    DrinkDataList[j] = DrinkDataList[j + 1];
                    DrinkDataList[j + 1] = temp;
                }
            }
        }
    }
    public void ClientByLevel()
    {
        for (int i = 0; i < ClientDataList.Count; i++)
        {
            for (int j = 0; j < ClientDataList.Count - i - 1; j++)
            {
                if (ClientDataList[j].UnlockLevel > ClientDataList[j+1].UnlockLevel)
                {
                    ClientData temp;
                    temp = ClientDataList[j];
                    ClientDataList[j] = ClientDataList[j+1];
                    ClientDataList[j + 1] = temp;
                }
            }
        }
    }
    public void StaffByLevel()
    {
        for (int i = 0; i < StaffDataList.Count; i++)
        {
            for (int j = 0; j < StaffDataList.Count - i - 1; j++)
            {
                if (StaffDataList[j].UnlockLevel > StaffDataList[j + 1].UnlockLevel)
                {
                    StaffData temp;
                    temp = StaffDataList[j];
                    StaffDataList[j] = StaffDataList[j + 1];
                    StaffDataList[j + 1] = temp;
                }
            }
        }
    }
}
