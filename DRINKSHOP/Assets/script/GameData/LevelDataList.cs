using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "CreateGameData/LevelDataList")]
public class LevelDataList : ScriptableObject
{
    public List<LevelUpData> LevelUpData;
    public List<LuckyData> LuckyData;
}
