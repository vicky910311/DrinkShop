using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Mission 
{
    [SerializeField]
    private string name;
    public string Name 
    { 
        set { name = value; } 
        get { return name; } 
    }
    [SerializeField]
    private string narrate;
    public string Narrate
    {
        set { narrate = value; }
        get { return narrate; }
    }
    [SerializeField]
    private bool isactive = true;
    public bool isActive
    {
        set { isactive = value; }
        get { return isactive; }
    }
    [SerializeField]
    private bool isreach = false;
    public bool isReach
    {
        set { isreach = value; }
        get { return isreach; }
    }
    [SerializeField]
    private bool isrewarded = false;
    public bool isRewarded
    {
        set { isrewarded = value; }
        get { return isrewarded; }
    }
    [SerializeField]
    private MissionType type;
    public MissionType Type
    {
        set { type = value; }
        get { return type; }
    }
    [SerializeField]
    private int needamount;
    public int NeedAmount
    {
        set { needamount = value; }
        get { return needamount; }
    }
    [SerializeField]
    private int reward;
    public int Reward
    {
        set { reward = value; }
        get { return reward; }
    }
}
public enum MissionType
{
    GhostHunt,
    WakeUp,
    ReachSell,
    UnlockClient,
    Poor
}