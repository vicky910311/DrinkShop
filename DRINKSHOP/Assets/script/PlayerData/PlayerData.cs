using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreatePlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
{
    public List<bool> HavetheDrink = new List<bool>();
    public int DrinkSum;
    public List<bool> HavetheStaff = new List<bool>();
    public int StaffSum;
    public List<bool> HavetheClient = new List<bool>();
    public int ClientSum;
    public int Money;
    public int Level;
    public List<int> DrinkinStock = new List<int>();
    public int DrinkSell;
    public int AddStockLimit;
    public bool DeleteAD;
    public DateTime LastEndTime;
    public DateTime ThisOpenTime;
    public bool FirstTime = true;
    public int Coin;
}
