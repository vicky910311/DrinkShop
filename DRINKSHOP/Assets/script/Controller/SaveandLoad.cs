using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveandLoad 
{
    public PlayerData Player;
    public MissionList Mission;
    public TimeState Time;
    public void Save(PlayerData Player, MissionList Mission, TimeState Time)
    {
        PlayerPrefs.SetString("jsonplayersave", JsonUtility.ToJson(Player));
        PlayerPrefs.SetString("jsonmissionsave", JsonUtility.ToJson(Mission));
        PlayerPrefs.SetString("jsontimesave", JsonUtility.ToJson(Time));
        Debug.Log("Save");
    }
    public void Load()
    {
        Player = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsonplayersave"));
        Mission = JsonUtility.FromJson<MissionList>(PlayerPrefs.GetString("jsonmissionsave"));
        Time = JsonUtility.FromJson<TimeState>(PlayerPrefs.GetString("jsontimesave"));
        Debug.Log("Load");
    }
}
