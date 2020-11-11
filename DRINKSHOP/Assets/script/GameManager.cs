using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

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
    public int storynum;
    public float NowTime;
    public GameObject Content,  MenuContent, MakeContent, ClientContent, SCContent;
    public GameObject DevelopBtn, DrinkDevelop, DoneDevelopText, csText, limitWindow, coinText;
    public bool DrinkhaveDevelop;
    public GameObject[] drinks, clients;
    public List<GameObject> drinksmake = new List<GameObject>();
    public int TempMoney;
    public string LeaveNarrate;
    public bool Back;
    public GameObject ghostin;
    int[] UST;

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

       /*if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsonplayersave")) != null)
        {
            saveandLoad.Load();
            pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData = saveandLoad.Time;
            Debug.Log("Loading");
        }*/
        Back = true;
        /*if (pm.Player.FirstTime == false && pm.Player.Endtimestring != null)
            pm.Player.LastEndTime = DateTime.Parse(pm.Player.Endtimestring);*/
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
        pm.Player.OnLevelChanged += headerInfo;
        checkLevel();
        pm.Player.OnDrinkSumChanged += checkLevel;
        pm.Player.OnDrinkSellChanged += checkLevel;
        
        missions = new GameObject[ms.Mission.Missions.Count];
        MissiomMenu();
        staffs = new GameObject[gm.Staff.StaffData.Count];
        StaffMenu();
        sellTime = Time.time;
        sellbetweenTime = 5f;
        EventUseTime = UnityEngine.Random.Range(5f, 10f);
        NowTime = EventHappenTime = Time.time;
        pm.Player.OnCoinChange += CoinChange;
        drinks = new GameObject[gm.Drink.DrinkData.Count];
        clients = new GameObject[gm.Client.ClientData.Count];
        DrinkMenu();
        DrinkMakeMenu();
        ClientMenu();
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-451, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 580 + pm.Player.DrinkSum / 3 * 580);
        coinText.GetComponent<Text>().text = "代幣數量：" + pm.Player.Coin;
        ClientControl.WhenNotPlayingSell(pm.Player, ref TempMoney, ref LeaveNarrate);
        Debug.Log("少賺：" + TempMoney);
        ui.OpenNotice();
        for (int i = 0; i < gm.Drink.DrinkData.Count; i++)
        {
            pm.Player.OnDrinkinStockChanged[i] += StockAmount;
        }
        StockAmount();
        Back = true;
        checkMission();
        pm.Player.OnCatchGhostChange += checkMission;
        pm.Player.OnCatchSleepChange += checkMission;
        pm.Player.OnDrinkSellChanged += checkMission;
        pm.Player.OnClientSumChanged += checkMission;
        pm.Player.OnMoneyChanged += checkMission;
        if (tm.TimeData.DevelopTime > 30)
        {
            ui.developfastBtn.SetActive(true);
        }
        else
        {
            ui.developfastBtn.SetActive(false);
        }
        UST = new int[gm.Staff.StaffData.Count];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ui.OpenQuit();
        }
        if (Time.time > sellTime + sellbetweenTime)
        {
            sellDrinks();
            sellTime = Time.time;
            sellbetweenTime = UnityEngine.Random.Range((float)ComeTimeMin, (float)ComeTimeMax);
            Debug.Log("賣飲料" + sellbetweenTime);
        }
        if(Time.time > promoteTime + promotelasting)
        {
            ClientControl.PromoteSell(ref ComeTimeMin, ref ComeTimeMax, ClientControl.PromoteType.Normal);
            ui.adBtn.GetComponent<Button>().interactable = true;
            ui.manualBtn.GetComponent<Button>().interactable = true;
        }
        if (pm.Player.LastEndTime > pm.Player.ThisOpenTime)
        {
            if (Back == true)
            {
                leaveback();
                TimeSpan During = pm.Player.ThisOpenTime - pm.Player.LastEndTime;
                if ((int)(During).TotalSeconds > 0 && tm.TimeData.DevelopTime > 0)
                {
                    tm.TimeData.DevelopTime -= (int)During.TotalSeconds;
                    tm.TimeData.DevelopTime = (int)Mathf.Clamp(tm.TimeData.DevelopTime, 0, 36000f);
                }
            }
        }
        if (Back == true)
            Back = false;
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveandLoad.Save(pm.Player, ms.Mission, tm.TimeData);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            saveandLoad.Load();
            pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData = saveandLoad.Time;
        }

        if (tm.TimeData.DevelopTime > 0)
        {
            
            DrinkDevelop.transform.Rotate(0, 0, -0.4f);
        }
        else if (tm.TimeData.DevelopTime == 0)
        {
            DrinkDevelop.transform.DORotate(new Vector3(0, 0, 0), 1.5f);
        }
        if (Time.time > NowTime + 1.0f)
        {
            if (tm.TimeData.DevelopTime > 0)
            {
                tm.TimeData.DevelopTime--;
                Debug.Log(tm.TimeData.DevelopTime);
                int T = tm.TimeData.DevelopTime;
                DevelopBtn.GetComponentInChildren<Text>().text = (T/3600).ToString("00") + ":" + (T%3600/60).ToString("00") + ":" + (T%3600%60).ToString("00");
                if (tm.TimeData.DevelopTime <= 30)
                {
                    ui.developfastBtn.SetActive(false);
                }
            }
            else if (tm.TimeData.DevelopTime == 0)
            {
                if (ui.DrinkWindow.activeSelf == true && ui.DevelopWindow.activeSelf == true && ui.levelupWindow.activeSelf == false)
                {
                    GameObject FX = Instantiate(Resources.Load("Prefabs/CFX_Star"), transform) as GameObject;
                }
                HaveDevelop();
            }
            for (int i = 0; i < pm.Player.DrinkSum; i++)
            {
                if (tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) > 0)
                {
                    tm.TimeData.setMakeTime(pm.Player.getCanMake(i), tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) - 1);
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作中";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = false;
                }
                else if (tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) == 0)
                {
                    int a = pm.Player.getCanMake(i);
                    pm.Player.setDrinkinStock(a, tm.TimeData.getMakeTemp(a) + pm.Player.getDrinkinStock(a));
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = true;
                    tm.TimeData.setMakeTime(a, -1);
                    //drinksmake[i].transform.GetChild(4).GetComponent<Text>().text = pm.Player.getDrinkinStock(i).ToString();
                }
            }
            for(int i = 0; i < gm.Staff.StaffData.Count; i++)
            {
                
                if (tm.TimeData.getStaffUnlockTime(i) > 0)
                {
                    tm.TimeData.setStaffUnlockTime(i, tm.TimeData.getStaffUnlockTime(i) - 1);
                    UST[i] = tm.TimeData.getStaffUnlockTime(i);
                    staffs[i].transform.GetChild(2).GetComponentInChildren<Text>().text = (UST[i] / 3600).ToString("00") + ":" + (UST[i] % 3600 / 60).ToString("00") + ":" + (UST[i] % 3600 % 60).ToString("00");
                    if (tm.TimeData.getStaffUnlockTime(i)>30)
                    {
                        staffs[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                    }
                
                }
                else if(tm.TimeData.getStaffUnlockTime(i) == 0)
                {
                    tm.TimeData.setStaffUnlockTime(i, -1);
                    AddStaffMenu(i);
                }
            }


            NowTime = Time.time;
        }
        if (Time.time > EventHappenTime + EventUseTime)
        {
            Incidenthappen();
            EventHappenTime = Time.time;
            EventUseTime = UnityEngine.Random.Range(5f, 10f);
        }

    }
    public void sellDrinks()
    {
        bool isnew = false;
        int c = -1, d = -1;
        ClientControl.SelltheDrink(pm.Player, ref c, ref isnew, ref d);
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
    public void checkLevel()
    {
        if (EventControl.LevelUp(pm.Player))
        {
            ui.OpenLevelup();
        }
        Debug.Log("checkLevel");
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
        Debug.Log("MissiomMenu");
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
        if(i < ms.Mission.Missions.Count && i >= 0)
        { 
            if (EventControl.CanReward(i, ms.Mission))
            {
                missions[i].transform.SetSiblingIndex(0);
                missions[i].transform.GetChild(2).GetComponent<Button>().enabled = true;
            }
            MissionBtnText(i);
        }
        
    }
    public void MissionBtnText(int i)
    {
        if (i < ms.Mission.Missions.Count && i >= 0)
        {
            if (EventControl.CanReward(i, ms.Mission))
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
            
    }
    public void StaffMenu()
    {
        for (int i=0;i<gm.Staff.StaffData.Count;i++)
        {
            if (pm.Player.getHavetheStaff(i) == true)
            {
                staffs[i] = Instantiate(Resources.Load("Prefabs/unlockstaff"), StaffContent.transform) as GameObject;
                staffs[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Staff.StaffData[i].Image;
                int a = i;
                staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ReadStory(a); });
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate {;/*frontBtn*/ });
                staffs[i].transform.GetChild(3).GetComponent<Text>().text =gm.Staff.StaffData[i].Info;
                staffs[i].transform.GetChild(4).GetComponent<Text>().text = gm.Staff.StaffData[i].Chara;
                staffs[i].transform.GetChild(5).GetComponent<Text>().text = gm.Staff.StaffData[i].Name;
            }
            else
            {
                staffs[i] = Instantiate(Resources.Load("Prefabs/lockstaff"), StaffContent.transform) as GameObject;
                staffs[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
                int a = i;
                staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { unlockstaffFast(a); });
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { UnlockStaff(a); });
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
        staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ReadStory(i); });
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
    public void unlockstaffFast(int i)
    {
        tm.TimeData.setStaffUnlockTime(i,tm.TimeData.getStaffUnlockTime(i)/120);
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
    public void ReadStory(int i)
    {
        storynum = i;
        UIManager.self.OpenStory();
      
    }
    public void Quit()
    {
       
        Application.Quit();
    }
    public void leaveback()
    {
        pm.Player.ThisOpenTime = DateTime.Now;
        Debug.Log(pm.Player.ThisOpenTime);
        ClientControl.WhenNotPlayingSell(pm.Player, ref TempMoney, ref LeaveNarrate);
        ui.OpenNotice();
    }
    public void Recapture()
    {
        //播放廣告
        ui.shutdownNotice();
        pm.Player.Money += TempMoney;
    }
    public void Incidenthappen()
    {
        int i = UnityEngine.Random.Range(1, 5);
        string n = "沒事";
        EventControl.IncidentHappen(i, ref n, pm.Player);
        if (i == 1)
        {
            if (ghostin.transform.childCount < 10)
            {
                GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), ghostin.transform) as GameObject;
            }
            else
            {
                n = "幽靈人口過剩";
            }
        }
        GameObject narrate = Instantiate(Resources.Load("Prefabs/Text"), Content.transform) as GameObject;
        narrate.GetComponent<Text>().text = n;
        narrate.transform.SetSiblingIndex(0);
        RectTransform rt = Content.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 100, 0);
        rt.sizeDelta += new Vector2(0, 200);

    }
    /*public void SellDrinks()
    {
        bool isnew = false;
        int c = -1, d = -1;
        ClientControl.SelltheDrink(pm.Player, ref c, ref isnew, ref d);


    }*/
    public void PressDevelop()
    {
        ui.developfastBtn.SetActive(true);
        if (DrinkhaveDevelop == false && pm.Player.DrinkSum < gm.Drink.DrinkData.Count)
        {
            ui.OpenDevelopCost();
            if (pm.Player.Coin >= 3)
            {
                ui.developcostWindow.GetComponentInChildren<Text>().text = "可免費抽一次\n(花費三枚代幣)";
            }
            else
            {
                ui.developcostWindow.GetComponentInChildren<Text>().text = "需花費" + gm.Drink.DrinkUse.DevelopCost;
            }

            if (pm.Player.Money < gm.Drink.DrinkUse.DevelopCost)
            {
                ui.developcostWindow.transform.GetChild(1).GetComponent<Button>().interactable = false;
                ui.developcostWindow.GetComponentInChildren<Text>().text += "\n(金額不足)";
            }
            else
            {
                ui.developcostWindow.transform.GetChild(1).GetComponent<Button>().interactable = true;
            }
        }
        else if (DrinkhaveDevelop == false && pm.Player.DrinkSum == gm.Drink.DrinkData.Count)
        {
            ui.OpenDevelopLimit();
        }
        else if (DrinkhaveDevelop == true)
        {
            DrinkhaveDevelop = false;
            csText.GetComponent<Text>().text = "";
            DevelopBtn.GetComponentInChildren<Text>().text = "研發";
            DoneDevelopText.GetComponent<Text>().text = "？？？？？";
            DrinkDevelop.GetComponent<Image>().sprite = null;
        }
    }
    public void Develop()
    {
        ui.shutdownLittle();
        if (DrinkhaveDevelop == false)
        {
            tm.TimeData.DevelopTemp = DrinkControl.DevelopDrink(pm.Player);
            if (tm.TimeData.DevelopTemp != -1)
                tm.TimeData.DevelopTime = gm.Drink.DrinkData[tm.TimeData.DevelopTemp].DevelopTime;
            DevelopBtn.GetComponent<Button>().enabled = false;
        }


    }
    public void developFast()
    {
        //ad
        tm.TimeData.DevelopTime = tm.TimeData.DevelopTime / 120;
        ui.developfastBtn.SetActive(false);
    }
    public void HaveDevelop()
    {
        DrinkhaveDevelop = true;
       
        if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) != true)
        {
            pm.Player.setHavetheDrink(tm.TimeData.DevelopTemp, true);
            pm.Player.DrinkSum++;
            pm.Player.addCanMake(tm.TimeData.DevelopTemp);
            Debug.Log(tm.TimeData.DevelopTemp + "研發成功");
            if (gm.Drink.DrinkData[tm.TimeData.DevelopTemp].isSpecial)
            {
                csText.GetComponent<Text>().text = "特殊飲料";
            }
            AddDrinkMenu(tm.TimeData.DevelopTemp);
            AddDrinkMakeMenu(tm.TimeData.DevelopTemp);
        }
        else if (tm.TimeData.DevelopTemp != -1 && pm.Player.getHavetheDrink(tm.TimeData.DevelopTemp) == true)
        {
            pm.Player.Coin++;
            Debug.Log("代幣增加" + pm.Player.Coin);
            csText.GetComponent<Text>().text = "代幣增加";
        }
        
        DrinkDevelop.GetComponent<Image>().sprite = gm.Drink.DrinkData[tm.TimeData.DevelopTemp].Image;
        DoneDevelopText.GetComponent<Text>().text = gm.Drink.DrinkData[tm.TimeData.DevelopTemp].Name;
        tm.TimeData.DevelopTime = -1;
        DevelopBtn.GetComponentInChildren<Text>().text = "完成";
        DevelopBtn.GetComponent<Button>().enabled = true;

    }
    public void CoinChange()
    {
        coinText.GetComponent<Text>().text = "代幣數量：" + pm.Player.Coin;
    }
    public void MakeAll()
    {
        for (int i = 0; i < pm.Player.DrinkSum; i++)
        {
            int make = -1, maketime = -1;
            DrinkControl.MakingDrink(pm.Player.getCanMake(i), pm.Player, ref make, ref maketime);
            Debug.Log(pm.Player.getCanMake(i) + "補齊了");
        }
    }
    public void ClientMenu()
    {
        for (int i = 0; i < gm.Client.ClientData.Count; i++)
        {
            clients[i] = Instantiate(Resources.Load("Prefabs/client"), ClientContent.transform) as GameObject;
            if (pm.Player.getHavetheClient(i) == true)
            {
                int a = i;
                clients[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(a, objectType.Client); });
                clients[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
                clients[i].transform.GetChild(1).GetComponent<Text>().text = gm.Client.ClientData[i].Name;
            }
            if (gm.Client.ClientData[i].isSpecial) { clients[i].transform.SetParent(SCContent.transform); }
        }
    }
    public void AddCientMenu(int i)
    {
        clients[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(i, objectType.Client); });
        clients[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
        clients[i].transform.GetChild(1).GetComponent<Text>().text = gm.Client.ClientData[i].Name;
    }
    public void DrinkMenu()
    {

        for (int i = 0; i < gm.Drink.DrinkData.Count; i++)
        {
            drinks[i] = Instantiate(Resources.Load("Prefabs/drink"), MenuContent.transform) as GameObject;
            //drinks[i].transform.SetParent(MenuContent.transform);
            if (pm.Player.getHavetheDrink(i) == true)
            {
                int a = i;
                drinks[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(a, objectType.Drink); });
                drinks[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
                drinks[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
                drinks[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Price.ToString();
            }

        }
    }
    public void AddDrinkMenu(int i)
    {
        drinks[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(i, objectType.Drink); });
        drinks[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
        drinks[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
        drinks[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Price.ToString();
    }
    public void PressObject(int i,objectType T)
    {
        if( T == objectType.Client)
        {
            ui.objectWindow.transform.GetChild(1).GetComponent<Text>().text = "顧客";
            ui.objectWindow.transform.GetChild(2).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
            ui.objectWindow.transform.GetChild(3).GetComponent<Text>().text = gm.Client.ClientData[i].Name;
            ui.objectWindow.transform.GetChild(4).GetComponent<Text>().text = gm.Client.ClientData[i].Info;
        }
        else if( T == objectType.Drink)
        {
            ui.objectWindow.transform.GetChild(1).GetComponent<Text>().text = "飲料";
            ui.objectWindow.transform.GetChild(2).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
            ui.objectWindow.transform.GetChild(3).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
            ui.objectWindow.transform.GetChild(4).GetComponent<Text>().text = gm.Drink.DrinkData[i].Narrate;
        }
        ui.OpenObject();
    }


    public void DrinkMakeMenu()
    {
        for (int i = 0; i < pm.Player.DrinkSum; i++)
        {
            drinksmake.Add(Instantiate(Resources.Load("Prefabs/drinkcanmake"), MakeContent.transform) as GameObject);
            int a = pm.Player.getCanMake(i);
            drinksmake[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[a].Image;
            drinksmake[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[a].Name;
            drinksmake[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[a].Cost.ToString();
            drinksmake[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(a); });
            Debug.Log(pm.Player.getCanMake(i));
            drinksmake[i].transform.GetChild(4).GetComponent<Text>().text = pm.Player.getDrinkinStock(a).ToString();
            Debug.Log(i + "/" + pm.Player.getDrinkinStock(a));
        }

    }
    public void StockAmount()
    {
        for (int i = 0; i < pm.Player.DrinkSum; i++)
        {
            int a = pm.Player.getCanMake(i);
            drinksmake[i].transform.GetChild(4).GetComponent<Text>().text = pm.Player.getDrinkinStock(a).ToString();

        }
    }
    public void AddDrinkMakeMenu(int i)
    {
        drinksmake.Add(Instantiate(Resources.Load("Prefabs/drinkcanmake"), MakeContent.transform) as GameObject);
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Cost.ToString();
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(i); });
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-451, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 580 + pm.Player.DrinkSum / 3 * 580);
    }
    public void DrinkMakeOnClick(int i)
    {
        if (pm.Player.getDrinkinStock(i) < pm.Player.AddStockLimit + gm.Drink.DrinkUse.StockLimit)
        {
            int make = -1, maketime = -1;
            DrinkControl.MakingDrink(i, pm.Player, ref make, ref maketime);
            tm.TimeData.setMakeTemp(i, make);
            tm.TimeData.setMakeTime(i, maketime);
            Debug.Log("補" + i + "  " + pm.Player.getDrinkinStock(i));
        }
        else
        {
            limitWindow.SetActive(true);
        }

    }


    public void OnApplicationPause()
    {
        if (Back == false)
        {
            pm.Player.LastEndTime = DateTime.Now;
            pm.Player.Endtimestring = pm.Player.LastEndTime.ToString();
            saveandLoad.Save(pm.Player, ms.Mission, tm.TimeData);
        }
        Back = true;
        ui.shutdownLittle();
        ui.shutdownAll();
        ui.shutdownLevelup();

        Debug.Log("save");
    }
    void OnApplicationQuit()
    {
        pm.Player.LastEndTime = DateTime.Now;
        pm.Player.Endtimestring = pm.Player.LastEndTime.ToString();
        Debug.Log(" pm.Player.LastEndTime" + pm.Player.Endtimestring);
        ui.shutdownLittle();
        ui.shutdownAll();
        ui.shutdownLevelup();
        saveandLoad.Save(pm.Player, ms.Mission, tm.TimeData);
        Debug.Log("Save");
    }
}

public enum objectType
{
    Client,
    Drink
}