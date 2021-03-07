using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MissionData", menuName = "CreateGameData/MissionDataList")]

public class MissionAssetList : ScriptableObject
{
    public List<MissionAsset> MissionInfo;
}
