using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCalculator : MonoBehaviour  //Testing script
{
    private DateTime Time1, Time2;
    private TimeSpan Differ;
    public GameObject time1, time2, differ;
    public PlayerData Player;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    string missionType;
    private void OnGUI()
    {
        missionType = GUILayout.TextField(missionType);
        if (GUILayout.Button("DoMission"))
        {
            Type t = Type.GetType(missionType);
            MissionData e = (MissionData)Activator.CreateInstance(t);
            e.DoEvent(null);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {


 
    }
    

    public void Time1click()
    {
        Time1 = DateTime.Now;
        Player.LastEndTime = Time1;
        time1.GetComponent<Text>().text = Time1.ToString();
    }

    public void Time2click()
    {
        Time2 = DateTime.Now;
        Player.ThisOpenTime = Time2;
        time2.GetComponent<Text>().text = Time2.ToString();
    }

    public void Differclick()
    {
        Differ = Time2 - Time1;
        differ.GetComponent<Text>().text = ((int)Differ.TotalSeconds).ToString();
    }
}
