using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;

public class Testing : MonoBehaviour
{
    public PlayerDataManager pm;
    public MissionState ms;
    public Timer tm;
    public GameDataManager gm;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    private StaffControl StaffControl = new StaffControl();
    private EventControl EventControl = new EventControl();
    private SaveandLoad saveandLoad = new SaveandLoad();
    public float NowTime;
    public float EventHappenTime,EventUseTime;
    
    // Start is called before the first frame update
    void Start()
    {
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = gm.Drink;   
        ClientControl.Client = gm.Client;
        EventControl.Level = gm.Level;
        StaffControl.Staff = gm.Staff;
        EventUseTime = UnityEngine.Random.Range(5f, 10f);
        NowTime = EventHappenTime = Time.time;
        TimeSpan During = pm.Player.ThisOpenTime - pm.Player.LastEndTime; 
        if((int)(During).TotalSeconds > 0 && tm.TimeData.DevelopTime > 0)
        {
            tm.TimeData.DevelopTime -= (int)(During).TotalSeconds;
            tm.TimeData.DevelopTime = (int)Mathf.Clamp(tm.TimeData.DevelopTime, 0, 36000f);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Time.time > NowTime + 1.0f)
        {
            if (tm.TimeData.DevelopTime > 0)
            {
                tm.TimeData.DevelopTime--;
                Debug.Log(tm.TimeData.DevelopTime);
            }
            else if(tm.TimeData.DevelopTime == 0)
            {
                if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) != true)
                {
                    pm.Player.setHavetheDrink(tm.TimeData.DevelopTemp,true);
                    AddDrinkSum();
                    //Player.DrinkSum++;
                    pm.Player.addCanMake(tm.TimeData.DevelopTemp);
                    Debug.Log(tm.TimeData.DevelopTemp + "研發成功");
                }
                else if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) == true)
                {
                    pm.Player.Coin++;
                    Debug.Log("代幣增加" + pm.Player.Coin);
                }
                tm.TimeData.DevelopTime = -1;
               

              //  HaveDevelop();

            }
          
            NowTime = Time.time;
        }
        if (Time.time > EventHappenTime + EventUseTime)
        {
            Incidenthappen();
            EventHappenTime = Time.time;
            EventUseTime = UnityEngine.Random.Range(5f,10f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveandLoad.Save();
            
            Debug.Log("save" );
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            saveandLoad.Load();
            pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData  = saveandLoad.Time;

            Debug.Log("load  "+ pm.Player.getDrinkinStock(0));
        }
    }
   
    public void Incidenthappen()
    {
        int i = UnityEngine.Random.Range(1, 5);
        string n = "沒事";
        EventControl.IncidentHappen(i, n, pm.Player);
        if(i == 1)
        {
            GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), transform) as GameObject;
        }
    }
    public void SellDrinks()
    {
        ClientControl.SelltheDrink(pm.Player);
        Debug.Log(pm.Player.getDrinkinStock(0));
    }
    public void Develop()
    {
        tm.TimeData.DevelopTemp = DrinkControl.DevelopDrink(pm.Player);
        if (tm.TimeData.DevelopTemp != -1)
            tm.TimeData.DevelopTime = gm.Drink.DrinkData[tm.TimeData.DevelopTemp].DevelopTime;
    }
    public void AddDrinkSum()
    {
        pm.Player.DrinkSum++;
    }
    public void HaveDevelop()
    {
       
       if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) != true)
        {
            pm.Player.setHavetheDrink(tm.TimeData.DevelopTemp, true);
            AddDrinkSum();
            pm.Player.addCanMake(tm.TimeData.DevelopTemp);
            Debug.Log(tm.TimeData.DevelopTemp + "研發成功");
            tm.TimeData.DevelopTime = -1;
        }
       else if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) == true)
        {
            pm.Player.Coin++;
            Debug.Log("代幣增加" + pm.Player.Coin);
            tm.TimeData.DevelopTime = -1;
        }
       

    }
    public void MakeAll()
    {
        for (int i = 0;i< pm.Player.DrinkSum;i++)
        {
            DrinkControl.MakingDrink(pm.Player.getCanMake(i), pm.Player);
            Debug.Log(pm.Player.getCanMake(i) + "補齊了");
        }
    }
   
}
