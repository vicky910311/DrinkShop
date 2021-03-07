using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPUT : MonoBehaviour
{
    public MissionAssetList mission;
    void Start()
    {
        for(int i = 0; i < MissionState.self.Mission.Missions.Count; i++)
        {
            mission.MissionInfo[i].Name = MissionState.self.Mission.Missions[i].Name;
            mission.MissionInfo[i].Narrate = MissionState.self.Mission.Missions[i].Narrate;
            mission.MissionInfo[i].Type = MissionState.self.Mission.Missions[i].Type;
            mission.MissionInfo[i].NeedAmount = MissionState.self.Mission.Missions[i].NeedAmount;
            mission.MissionInfo[i].Reward = MissionState.self.Mission.Missions[i].Reward;
        }
    }
}
