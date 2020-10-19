using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;

public class Testing : MonoBehaviour
{
    private PlayerData Player;
    public ClientDataList Client;
    public DrinkDataList Drink;
    public LevelDataList Level;
    public StaffDataList Staff;
    private MissionList Mission;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    private StaffControl StaffControl = new StaffControl();
    private EventControl EventControl = new EventControl();
    private SaveandLoad saveandLoad = new SaveandLoad();
    private TimeState TimeData;
    public float NowTime;
    public float EventHappenTime,EventUseTime;
    // Start is called before the first frame update
    void Start()
    {
        Player = saveandLoad.Player = PlayerDataManager.self.Player;
        Mission = saveandLoad.Mission = MissionState.self.Mission;
        TimeData = saveandLoad.Time = Timer.self.TimeData;
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = Drink;   
        ClientControl.Client = Client;
        EventControl.Level = Level;
        StaffControl.Staff = Staff;
        EventUseTime = UnityEngine.Random.Range(5f, 10f);
        NowTime = EventHappenTime = Time.time;
        TimeSpan During = Player.ThisOpenTime - Player.LastEndTime; 
        if((int)(During).TotalSeconds > 0 && TimeData.DevelopTime > 0)
        {
            TimeData.DevelopTime -= (int)(During).TotalSeconds;
            TimeData.DevelopTime = (int)Mathf.Clamp(TimeData.DevelopTime, 0, 36000f);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        EventControl.PlayerAchieveMission(Player,Mission);
        if (Time.time > NowTime + 1.0f)
        {
            if (TimeData.DevelopTime > 0)
            {
                TimeData.DevelopTime--;
                Debug.Log(TimeData.DevelopTime);
            }
            else if(TimeData.DevelopTime == 0)
            {
                if (Player.HavetheDrink[TimeData.DevelopTemp] == true)
                {
                    Player.Coin++;
                    Debug.Log("代幣增加");
                }
                else if( TimeData.DevelopTemp != -1)
                {
                    Player.HavetheDrink[TimeData.DevelopTemp] = true;
                    Player.DrinkSum++;
                    Player.CanMake.Add(TimeData.DevelopTemp);
                    Debug.Log(TimeData.DevelopTemp + "研發成功");
                }
                TimeData.DevelopTime = -1;
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
            
            Debug.Log("save" + Player.DrinkinStock[0] + "?" + PlayerDataManager.self.Player.DrinkinStock[0]);
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            saveandLoad.Load();
            Player = PlayerDataManager.self.Player = saveandLoad.Player;
            Mission = MissionState.self.Mission = saveandLoad.Mission;
            TimeData = Timer.self.TimeData = saveandLoad.Time;

            Debug.Log("load  " + Player.DrinkinStock[0] + "?" + PlayerDataManager.self.Player.DrinkinStock[0]);
        }
    }
   
    public void Incidenthappen()
    {
        int i = UnityEngine.Random.Range(1, 5);
        string n = "沒事";
        EventControl.IncidentHappen(i, n,Player);
        if(i == 1)
        {
            GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), transform) as GameObject;
        }
    }
    public void SellDrinks()
    {
        ClientControl.SelltheDrink(Player);
        Debug.Log(Player.DrinkinStock[0]+"?"+ PlayerDataManager.self.Player.DrinkinStock[0]);
    }
    public void Develop()
    {
        TimeData.DevelopTemp = DrinkControl.DevelopDrink(Player);
        if (TimeData.DevelopTemp != -1)
            TimeData.DevelopTime = Drink.DrinkData[TimeData.DevelopTemp].DevelopTime;
    }
    public void MakeAll()
    {
        for (int i = 0;i<Player.CanMake.Count;i++)
        {
            DrinkControl.MakingDrink(Player.CanMake[i],Player);
            Debug.Log(Player.CanMake[i] + "補齊了");
        }
    }
   
}
