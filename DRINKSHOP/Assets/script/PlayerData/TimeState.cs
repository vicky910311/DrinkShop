using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TimeData", menuName = "CreateTimeData")]
[Serializable]
public class TimeState : ScriptableObject
{
    public List<int> MakeTime;
    public List<int> StaffUnlockTime;
    public int DevelopTime;
    public List<int> MakeTemp;
    public List<bool> StaffUnlockTemp;
    public int DevelopTemp;
}
