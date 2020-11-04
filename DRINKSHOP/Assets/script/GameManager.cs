using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager self;
    public PlayerDataManager pm;
    public MissionState ms;
    public Timer tm;
    public GameDataManager gm;
    public UIManager ui;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    private StaffControl StaffControl = new StaffControl();
    private EventControl EventControl = new EventControl();
    private SaveandLoad saveandLoad = new SaveandLoad();
    private PurchaseControl PurchaseControl = new PurchaseControl();
    public GameObject[] staffs,missions;
    public GameObject StaffContent,MissionContent;
    public Text moneytext, selltext, leveltext;
    public float sellTime,promoteTime, promotelasting, sellbetweenTime;
    public int ComeTimeMin, ComeTimeMax;
    public float TimerTime, EventHappenTime, EventUseTime;
    // Start is called before the first frame update
    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
        else if (this != self)
        {
            Destroy(gameObject);
        }
       
    }
    void Start()
    {
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = gm.Drink;
        ClientControl.Client = gm.Client;
        EventControl.Level = gm.Level;
        StaffControl.Staff = gm.Staff;
        ComeTimeMin = gm.Client.ComeTime.NormalMin;
        ComeTimeMax = gm.Client.ComeTime.NormalMax;
        headerInfo();
        pm.Player.OnDrinkSellChanged += headerInfo;
        pm.Player.OnMoneyChanged += headerInfo;
        pm.Player.OnDrinkSellChanged += headerInfo;
        checkMission();
        pm.Player.OnCatchGhostChange += checkMission;
        pm.Player.OnCatchSleepChange += checkMission;
        pm.Player.OnDrinkSellChanged += checkMission;
        pm.Player.OnClientSumChanged += checkMission;
        pm.Player.OnMoneyChanged += checkMission;
        missions = new GameObject[ms.Mission.Missions.Count];
        MissiomMenu();
        staffs = new GameObject[gm.Staff.StaffData.Count];
        StaffMenu();
        sellTime = Time.time;
        sellbetweenTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > sellTime + sellbetweenTime)
        {
            Debug.Log("賣飲料");
            sellTime = Time.time;
            sellbetweenTime = Random.Range((float)ComeTimeMin, (float)ComeTimeMax);
            Debug.Log(sellbetweenTime);
        }
        if(Time.time > promoteTime + promotelasting)
        {
            ClientControl.PromoteSell(ref ComeTimeMin, ref ComeTimeMax, ClientControl.PromoteType.Normal);
            ui.adBtn.GetComponent<Button>().interactable = true;
            ui.manualBtn.GetComponent<Button>().interactable = true;
        }
    }
    public void headerInfo()
    {
        moneytext.text = "資金：" + pm.Player.Money;
        selltext.text = "已賣出 " + pm.Player.DrinkSell;
        leveltext.text = "等級：" + pm.Player.Level;
    }
   
    public void PressADpromote()
    {
        ui.OpenLookAD();
        ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ui.shutdownLittle(); adpromote(); });
    }
    public void adpromote() 
    {
        promoteTime = Time.time;
        promotelasting = gm.Client.ComeTime.adpromoteTime;
        ClientControl.PromoteSell(ref ComeTimeMin,ref ComeTimeMax, ClientControl.PromoteType.AD);
        ui.adBtn.GetComponent<Button>().interactable = false;
        ui.manualBtn.GetComponent<Button>().interactable = false;
        Debug.Log("來客秒數" + ComeTimeMin + "~" + ComeTimeMax);
    }
    public void manualpromote()
    {
        promoteTime = Time.time;
        promotelasting = gm.Client.ComeTime.manualpromoteTime;
        ClientControl.PromoteSell(ref ComeTimeMin, ref ComeTimeMax, ClientControl.PromoteType.Manual);
        Debug.Log("來客秒數" + ComeTimeMin + "~" + ComeTimeMax);
    }
    public void checkMission()
    {
        EventControl.PlayerAchieveMission(pm.Player,ms.Mission);
        for(int i = 0; i<ms.Mission.Missions.Count; i++)
        {
            if (ms.Mission.Missions[i].isReach == true)
            {
                //ui按鈕晃動
                ChangeMissionMenu(i);
            }
        }
        
    }
    public void MissiomMenu()
    {
        for (int i = 0; i < ms.Mission.Missions.Count; i++)
        {
            missions[i] = Instantiate(Resources.Load("Prefabs/mission"), MissionContent.transform) as GameObject;
            missions[i].transform.GetChild(0).GetComponent<Text>().text = ms.Mission.Missions[i].Name;
            missions[i].transform.GetChild(1).GetComponent<Text>().text = ms.Mission.Missions[i].Narrate;
            int a = i;
            missions[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { missionreward(a); });
            missions[i].transform.GetChild(2).GetComponent<Button>().enabled = false;
            if (EventControl.CanReward(i,ms.Mission))
            {
                missions[i].transform.SetSiblingIndex(0);
                missions[i].transform.GetChild(2).GetComponent<Button>().enabled = true;
            }
            MissionBtnText(i);
        }
    }
    public void missionreward(int i)
    {
        if (EventControl.CanReward(i, ms.Mission))
        {
            EventControl.GetReward(i,ms.Mission,pm.Player);
            missions[i].transform.GetChild(2).GetComponent<Button>().enabled = false;
            missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "已領獎";
        }
    }
        
    public void ChangeMissionMenu(int i)
    {
        missions[i].transform.SetSiblingIndex(0);
        if (EventControl.CanReward(i, ms.Mission))
            missions[i].transform.GetChild(2).GetComponent<Button>().enabled = true;
        MissionBtnText(i);
    }
    public void MissionBtnText(int i)
    {
        if (EventControl.CanReward(i,ms.Mission))
        {
            missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "領獎";
        }
        else if (ms.Mission.Missions[i].isActive == false && ms.Mission.Missions[i].isRewarded == true)
        {
            missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "已領獎";
        }
        else
        {
            missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "未完成";
        }
    }
    public void StaffMenu()
    {
        for (int i=0;i<gm.Staff.StaffData.Count;i++)
        {
            if (pm.Player.getHavetheStaff(i) == true)
            {
                staffs[i] = Instantiate(Resources.Load("Prefabs/unlockstaff"), StaffContent.transform) as GameObject;
                staffs[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Staff.StaffData[i].Image;
                staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ;/*storyBtn*/ });
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate {;/*frontBtn*/ });
                staffs[i].transform.GetChild(3).GetComponent<Text>().text =gm.Staff.StaffData[i].Info;
                staffs[i].transform.GetChild(4).GetComponent<Text>().text = gm.Staff.StaffData[i].Chara;
                staffs[i].transform.GetChild(5).GetComponent<Text>().text = gm.Staff.StaffData[i].Name;
            }
            else
            {
                staffs[i] = Instantiate(Resources.Load("Prefabs/lockstaff"), StaffContent.transform) as GameObject;
                staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {;/*fastBtn*/ });
                int a = i;
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { AddStaffMenu(a); });
                staffs[i].transform.GetChild(3).GetComponent<Text>().text = "解鎖條件\n"+gm.Staff.StaffData[i].UnlockLevel+"星\n"+"資金"+gm.Staff.StaffData[i].UnlockCost;
            }
            
        }
    }
    public void AddStaffMenu(int i)
    {
        pm.Player.setHavetheStaff(i, true);
        Destroy(staffs[i].gameObject);
        staffs[i] = Instantiate(Resources.Load("Prefabs/unlockstaff"), StaffContent.transform) as GameObject;
        staffs[i].transform.SetSiblingIndex(i);
        staffs[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Staff.StaffData[i].Image;
        staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {;/*storyBtn*/ });
        staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate {;/*frontBtn*/ });
        staffs[i].transform.GetChild(3).GetComponent<Text>().text = gm.Staff.StaffData[i].Info;
        staffs[i].transform.GetChild(4).GetComponent<Text>().text = gm.Staff.StaffData[i].Chara;
        staffs[i].transform.GetChild(5).GetComponent<Text>().text = gm.Staff.StaffData[i].Name;
    }
    public void UnlockStaff(int i)
    {
        bool b = false;
        StaffControl.UnlockStaff(i, pm.Player,ref b);
        if (b == true)
        {
            tm.TimeData.setStaffUnlockTime(i, gm.Staff.StaffData[i].UnlockTime);
            tm.TimeData.setStaffUnlockTemp(i,true);
        }
        else
        {
            Debug.Log("條件不符");
        }
    }
    public void Purchase(int type)
    {
        if(type >= 10000)
        {
            PurchaseControl.AddtheMoney(pm.Player, type);
        }
        else if(type == 1)
        {
            PurchaseControl.DeletingAD(pm.Player);
        }
        else if(type == 2)
        {
            PurchaseControl.AddingStockLimit(pm.Player);
        }
    }
    
}

