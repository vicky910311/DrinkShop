using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TimeState 
{
    [SerializeField]
    private List<int> maketime;
    public void setMakeTime(int i,int t)
    {
        if(i < maketime.Count && i>=0)
            maketime[i] = t;
    }
    public int getMakeTime(int i)
    {
        if (i < maketime.Count && i >= 0)
            return maketime[i];
        else
            return -1;
    }
    [SerializeField]
    private List<int> staffunlocktime;
    public void setStaffUnlockTime(int i,int t)
    {
        if (i < staffunlocktime.Count && i >= 0)
            staffunlocktime[i] = t;
    }
    public int getStaffUnlockTime(int i)
    {
        if (i < staffunlocktime.Count && i >= 0)
            return staffunlocktime[i];
        else
            return -1;
    }
    [SerializeField]
    private int developtime;
    public int DevelopTime
    {
        set { developtime = value; }
        get { return developtime; }
    }
    [SerializeField]
    private List<int> maketemp;
    public void setMakeTemp(int i, int t)
    {
        if (i < maketemp.Count && i >= 0)
            maketemp[i] = t;
    }
    public int getMakeTemp(int i)
    {
        if (i < maketemp.Count && i >= 0)
            return maketemp[i];
        else
            return -1;
    }
    [SerializeField]
    private List<bool> staffunlocktemp;
    public void setStaffUnlockTemp(int i, bool b)
    {
        if (i < staffunlocktemp.Count && i >= 0)
            staffunlocktemp[i] = b;
    }
    public bool getStaffUnlockTemp(int i)
    {
        if (i < staffunlocktemp.Count && i >= 0)
            return staffunlocktemp[i];
        else
            return false;
    }
    public List<bool> StaffUnlockTemp;
    [SerializeField]
    private int developtemp;
    public int DevelopTemp
    {
        set { developtemp = value; }
        get { return developtemp; }
    }
}
