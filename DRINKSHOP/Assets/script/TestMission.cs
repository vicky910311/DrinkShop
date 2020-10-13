using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMission : MonoBehaviour
{
    string missionType;
    private void OnGUI()
    {
        missionType = GUILayout.TextField(missionType);
        if (GUILayout.Button("DoMission"))
        {
            Type t = Type.GetType(missionType);
            BaseMission e = (BaseMission)Activator.CreateInstance(t);
            e.DoEvent(null);
        }
    }
}
