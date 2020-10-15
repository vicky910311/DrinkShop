using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreatePlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
{
    public List<bool> HavetheDrink;
    public int DrinkSum;
    public List<bool> HavetheStaff;
    public int StaffSum;
    public List<bool> HavetheClient;
    public int ClientSum;
    public int Money;
    public int Level;
    public List<int> DrinkinStock;
    public int DrinkSell;
    public int AddStockLimit;
    public bool DeleteAD;
    public DateTime LastEndTime;
    public DateTime ThisOpenTime;
    public bool FirstTime = true;
    public int Coin;
    public int CatchGhost;
    public int CatchSleep;
}
