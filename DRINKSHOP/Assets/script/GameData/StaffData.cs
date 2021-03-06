using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct StaffData 
{
    public string Name;
    public string Chara;
    public string Info;
    public TextAsset Story;
    public Sprite StoryImage;
    public Sprite Image;
    public Sprite UnImage;
    public Sprite sleepImage;
    public Sprite ArmImage;
    public Sprite AfraidImage;
    public int UnlockCost;
    public int UnlockLevel;
    public int UnlockTime;
}
