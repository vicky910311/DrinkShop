using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "CreateGameData/MissionDataList")]
public class MissionList : ScriptableObject
{
    public List<Mission> Missions;
    
}


