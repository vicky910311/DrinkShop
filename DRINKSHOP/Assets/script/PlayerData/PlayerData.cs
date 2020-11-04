using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerData
{
    [SerializeField]
    private List<bool> havethedrink;
    public void setHavetheDrink(int i, bool b)
    {
        if (i < havethedrink.Count && i >= 0)
        {
            havethedrink[i] = b;
        }
    }
    public bool getHavetheDrink(int i)
    {
        if (i < havethedrink.Count && i >= 0)
            return havethedrink[i];
        else
            return false;
    }
    public int countHavetheDrink()
    {
        return havethedrink.Count;
    }
    [SerializeField]
    private int drinksum;
    public int DrinkSum
    {
        set
        {
            drinksum = value;
            if (OnDrinkSumChanged != null)
            {
                OnDrinkSumChanged();
            }
        }
        get { return drinksum; }
    }
    public Action OnDrinkSumChanged;
    [SerializeField]
    private List<bool> havethestaff;
    public void setHavetheStaff(int i, bool b)
    {
        if (i < havethestaff.Count && i >= 0)
        {
            havethestaff[i] = b;
        }
    }
    public bool getHavetheStaff(int i)
    {
        if (i < havethestaff.Count && i >= 0)
            return havethestaff[i];
        else
            return false;
    }
    [SerializeField]
    private int staffsum;
    public int StaffSum
    {
        set
        {
            staffsum = value;
            if (OnStaffSumChanged != null)
            {
                OnStaffSumChanged();
            }
        }
        get { return staffsum; }
    }
    public Action OnStaffSumChanged;
    [SerializeField]
    private List<bool> havetheclient;
    public void setHavetheClient(int i, bool b)
    {
        if (i < havetheclient.Count && i >= 0)
        {
            havetheclient[i] = b;
        }
    }
    public bool getHavetheClient(int i)
    {
        if (i < havetheclient.Count && i >= 0)
            return havetheclient[i];
        else
            return false;
    }
    [SerializeField]
    private int clientsum;
    public int ClientSum
    {
        set
        {
            clientsum = value;
            if (OnClientSumChanged != null)
            {
                OnClientSumChanged();
            }
        }
        get { return clientsum; }
    }
    public Action OnClientSumChanged;
    [SerializeField]
    private int money;
    public int Money
    {
        set
        {
            money = value;
            if (OnMoneyChanged != null)
            {
                OnMoneyChanged();
            }
        }
        get
        { return money; }
    }
    public Action OnMoneyChanged;
    [SerializeField]
    private int level;
    public int Level
    {
        set
        {
            level = value;
            if (OnLevelChanged != null)
            {
                OnLevelChanged();
            }
        }
        get
        { return level; }
    }
    public Action OnLevelChanged;
    [SerializeField]
    private List<int> drinkinstock;
    public void setDrinkinStock(int i, int n)
    {
        if (i < drinkinstock.Count && i >= 0)
        {
            drinkinstock[i] = n;
            if (OnDrinkinStockChanged[i] != null)
            {
                OnDrinkinStockChanged[i]();
            }
        }
    }
    public Action[] OnDrinkinStockChanged = new Action[30];
    public int getDrinkinStock(int i)
    {
        if (i < drinkinstock.Count && i >= 0)
            return drinkinstock[i];
        else
            return 0;
    }
    [SerializeField]
    private int drinksell;
    public int DrinkSell
    {
        set
        {
            drinksell = value;
            if (OnDrinkSellChanged != null)
            {
                OnDrinkSellChanged();
            }
        }
        get
        { return drinksell; }
    }
    public Action OnDrinkSellChanged;
    [SerializeField]
    private int addstocklimit;
    public int AddStockLimit
    {
        set
        {
            addstocklimit = value;
            if (OnAddStockLimitChanged != null)
            {
                OnAddStockLimitChanged();
            }
        }
        get { return addstocklimit; }
    }
    public Action OnAddStockLimitChanged;
    [SerializeField]
    private bool deletead;
    public bool DeleteAD
    {
        set
        {
            deletead = value;
            if (OnDeletwADChanged != null)
            {
                OnDeletwADChanged();
            }
        }
        get { return deletead; }
    }
    public Action OnDeletwADChanged;
    [SerializeField]
    private DateTime lastendtime;
    public DateTime LastEndTime
    {
        set
        {
            lastendtime = value;
            if (OnLastEndTimeChanged != null)
            {
                OnLastEndTimeChanged();
            }
        }
        get { return lastendtime; }
    }
    public Action OnLastEndTimeChanged;
    [SerializeField]
    private DateTime thisopentime;
    public DateTime ThisOpenTime
    {
        set
        {
            thisopentime = value;
            if (OnThisOpenTimeChange != null)
            {
                OnThisOpenTimeChange();
            }
        }
        get { return thisopentime; }
    }
    public Action OnThisOpenTimeChange;
    [SerializeField]
    private bool firsttime;
    public bool FirstTime
    {
        set
        {
            firsttime = value;
            if (OnFirstTimeChange != null)
            {
                OnFirstTimeChange();
            }
        }
        get { return firsttime; }
    }
    public Action OnFirstTimeChange;
    [SerializeField]
    private int coin;
    public int Coin
    {
        set
        {
            coin = value;
            if (OnCoinChange != null)
            {
                OnCoinChange();
            }
        }
        get { return coin; }
    }
    public Action OnCoinChange;
    [SerializeField]
    private List<int> canmake;
    public void setCanMake(int i, int n)
    {
        if (i < canmake.Count && i >= 0)
        {
            canmake[i] = n;
        }
    }
    public int getCanMake(int i)
    {
        if (i < canmake.Count && i >= 0)
            return canmake[i];
        else
            return 0;
    }
    public void addCanMake(int n)
    {
        canmake.Add(n);
    }
    [SerializeField]
    private int catchghost;
    public int CatchGhost
    {
        set
        {
            catchghost = value;
            if (OnCatchGhostChange != null)
            {
                OnCatchGhostChange();
            }
        }
        get { return catchghost; }
    }
    public Action OnCatchGhostChange;
    [SerializeField]
    private int catchsleep;
    public int CatchSleep
    {
        set
        {
            catchsleep = value;
            if (OnCatchSleepChange != null)
            {
                OnCatchSleepChange();
            }
        }
        get { return catchsleep; }
    }
    public Action OnCatchSleepChange;

    /*private RectTransform makewindowrt;
    public RectTransform MakeWindowRT
    {
        set { MakeWindowRT = value; }
        get { return MakeWindowRT; }
    }*/
}

