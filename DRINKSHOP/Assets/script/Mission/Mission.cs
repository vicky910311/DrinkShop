using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Mission 
{
    public string Name;
    public string Narrate;
    public bool isActive = true;
    public bool isReach = false;
    public bool isRewarded = false;
    public MissionType Type;
    public int NeedAmount;
    public int Reward;
}
public enum MissionType
{
    GhostHunt,
    WakeUp,
    ReachSell,
    UnlockClient,
    Poor
}