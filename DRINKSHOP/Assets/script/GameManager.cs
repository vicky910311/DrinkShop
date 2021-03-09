using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager self;
    public ADs ad;
    private PlayerDataManager pm;
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
    private float eventmin = 10f, eventmax = 11f;
    public int storynum;
    public float NowTime;
    public GameObject Content,  MenuContent, MakeContent, ClientContent, SCContent;
    public GameObject DevelopBtn, DrinkDevelop, DoneDevelopText, csText,  coinText;
    public bool DrinkhaveDevelop;
    public GameObject[] drinks, clients;
    public List<GameObject> drinksmake = new List<GameObject>();
    public int TempMoney;
    public string LeaveNarrate;
    public bool Back;
    public GameObject ghostin, Lucky;
    int[] UST;
    private int replenishamount = 25;
    bool star = false;
    public Sprite DrinkDefault;
    public GameObject stars,promoteBtn;
    public int ReplenishAmount
    {
        set
        {
            replenishamount = value;
            if (OnRAChange != null)
            {
                OnRAChange();
            }
        }
        get { return replenishamount; }
    }
    public Action OnRAChange;
    public GameObject Replenishment;
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
        pm = PlayerDataManager.self;
        if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsonplayersave")) != null&&
            JsonUtility.FromJson<MissionList>(PlayerPrefs.GetString("jsonmissionsave"))!= null)
        {
            saveandLoad.Load();
            //pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData = saveandLoad.Time;
            Debug.Log("Loading");
        }
        Back = true;
        if (pm.Player.FirstTime == false && pm.Player.Endtimestring != null)
            pm.Player.LastEndTime = DateTime.Parse(pm.Player.Endtimestring);
       
    }
    void Start()
    {
        SceneFade.self.Fadein();
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = gm.Drink;
        ClientControl.Client = gm.Client;
        EventControl.Level = gm.Level;
        EventControl.MissionAsset = gm.Mission;
        StaffControl.Staff = gm.Staff;
        ComeTimeMin = gm.Client.ComeTime.NormalMin;
        ComeTimeMax = gm.Client.ComeTime.NormalMax;
        lucky();
        pm.Player.OnLevelChanged += lucky;
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
        EventUseTime = UnityEngine.Random.Range(eventmin, eventmax);
        NowTime = EventHappenTime = Time.time;
        CoinChange();
        pm.Player.OnCoinChange += CoinChange;
        drinks = new GameObject[gm.Drink.DrinkData.Count];
        clients = new GameObject[gm.Client.ClientData.Count];
        DrinkMenu();
        DrinkMakeMenu();
        ClientMenu();
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-405, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 510 + pm.Player.DrinkSum / 3 * 510);
        
        /*ClientControl.WhenNotPlayingSell(pm.Player, ref TempMoney, ref LeaveNarrate);
        Debug.Log("少賺：" + TempMoney);
        ui.OpenNotice();*/
        for (int i = 0; i < gm.Drink.DrinkData.Count; i++)
        {
            pm.Player.OnDrinkinStockChanged[i] += StockAmount;
            ///pm.Player.OnDrinkinStockChanged[i] += HavenoDrink;
        }
        
        StockAmount();
        Back = true;
        checkMission();
        pm.Player.OnCatchGhostChange += checkMission;
        pm.Player.OnCatchSleepChange += checkMission;
        pm.Player.OnDrinkSellChanged += checkMission;
        pm.Player.OnClientSumChanged += checkMission;
        pm.Player.OnMoneyChanged += checkMission;
        CanReplenish();
        pm.Player.OnAddStockLimitChanged += CanReplenish;
        RAColor();
        OnRAChange += RAColor;
        if (tm.TimeData.DevelopTime > 0)
        {
            ui.developfastBtn.SetActive(true);
        }
        else
        {
            ui.developfastBtn.SetActive(false);
        }
        UST = new int[gm.Staff.StaffData.Count];
        SEContent();
        pm.Player.OnSEChange += SEContent;
        BGMContent();
        pm.Player.OnBGMChange += BGMContent;
        fastButtonColor();
        tm.TimeData.OnDTChanged += fastButtonColor;
        //saveandLoad.saveDefault(pm.Player, ms.Mission, tm.TimeData);
        frontBtnChange();
        pm.Player.OnFrontStaffChange += frontBtnChange;
        DevelopTime();
        tm.TimeData.OnDTChanged += DevelopTime;
        LeaveTimeCaculator();
        HavenoDrinkStart();
    }
    void OnApplicationFocus()
    {
       // if (pm.Player.LastEndTime > pm.Player.ThisOpenTime)//也許改記開始時候
       // {
           /* if (Back == true && pm.Player.FirstTime == false)
            {
                leaveback();
                TimeSpan During = pm.Player.ThisOpenTime - pm.Player.LastEndTime;
                if ((int)(During).TotalSeconds > 0 && tm.TimeData.DevelopTime > 0)
                {
                    tm.TimeData.DevelopTime -= (int)During.TotalSeconds;
                    tm.TimeData.DevelopTime = (int)Mathf.Clamp(tm.TimeData.DevelopTime, 0, 36000f);

                }
                for (int i = 0; i < gm.Staff.StaffData.Count; i++)
                {
                    if (tm.TimeData.getStaffUnlockTime(i) > 0 && (int)(During).TotalSeconds > 0)
                    {
                        tm.TimeData.setStaffUnlockTime(i, tm.TimeData.getStaffUnlockTime(i) - (int)During.TotalSeconds);
                        tm.TimeData.setStaffUnlockTime(i, (int)Mathf.Clamp(tm.TimeData.getStaffUnlockTime(i), 0, 36000f));
                    }
                }
            }*/
       // }
    }
    void DevelopTime()
    {
        int T = tm.TimeData.DevelopTime;
        if ( T  >= 0){
            DevelopBtn.GetComponentInChildren<Text>().text = (T / 3600).ToString("00") + ":" + (T % 3600 / 60).ToString("00") + ":" + (T % 3600 % 60).ToString("00");
            DevelopBtn.GetComponent<Button>().enabled = false;
        }
        
        
    }
    void LeaveTimeCaculator()
    {
        leaveback();
        TimeSpan During = pm.Player.ThisOpenTime - pm.Player.LastEndTime;
        if ((int)(During).TotalSeconds > 0 && tm.TimeData.DevelopTime > 0)
        {
            tm.TimeData.DevelopTime -= (int)During.TotalSeconds;
            tm.TimeData.DevelopTime = (int)Mathf.Clamp(tm.TimeData.DevelopTime, 0, 36000f);

        }
        for (int i = 0; i < gm.Staff.StaffData.Count; i++)
        {
            if (tm.TimeData.getStaffUnlockTime(i) > 0 && (int)(During).TotalSeconds > 0)
            {
                tm.TimeData.setStaffUnlockTime(i, tm.TimeData.getStaffUnlockTime(i) - (int)During.TotalSeconds);
                tm.TimeData.setStaffUnlockTime(i, (int)Mathf.Clamp(tm.TimeData.getStaffUnlockTime(i), 0, 36000f));
            }
        }
    }
    void HavenoDrinkStart()
    {
        string n = null,s = null;
        //
        for (int i = 0; i <pm.Player.DrinkSum; i++)
        {
            int a = pm.Player.getCanMake(i);
            if (pm.Player.getDrinkinStock(a) == 0)
            {
                // DrinkControl.havenoDrink(pm.Player, ref n, ref s);
                n = gm.Drink.DrinkData[a].Name + "缺貨中";
                s = "飲料缺貨中";
                if (ui.EventWindow.activeSelf == false)
                {
                    ui.EventNotify();
                }
                GameObject narrate = Instantiate(Resources.Load("Prefabs/Text"), Content.transform) as GameObject;
                narrate.GetComponent<Text>().text = n;
                narrate.transform.SetSiblingIndex(0);
                RectTransform rt = Content.GetComponent<RectTransform>();
                rt.position -= new Vector3(0, 100, 0);
                rt.sizeDelta += new Vector2(0, 200);
            
            }

        }
       
    }
    public void HavenoDrink(string n)
    {
        if (ui.EventWindow.activeSelf == false)
        {
            ui.EventNotify();
        }
        GameObject narrate = Instantiate(Resources.Load("Prefabs/Text"), Content.transform) as GameObject;
        narrate.GetComponent<Text>().text = n;
        narrate.transform.SetSiblingIndex(0);
        RectTransform rt = Content.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 100, 0);
        rt.sizeDelta += new Vector2(0, 200);
        string Short = "飲料缺貨";

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
        
        if (tm.TimeData.DevelopTime > 0 )
        {
            if (ui.DevelopWindow.activeSelf)
                DrinkDevelop.transform.Rotate(0, 0, -0.5f *100* Time.deltaTime);
            
        }
        else if (tm.TimeData.DevelopTime <= 0)
        {
            if ((DrinkDevelop.transform.rotation.z  <= -3.14f * 3f / 360f || DrinkDevelop.transform.rotation.z  >= 3.14f * 3f / 360f) && ui.DevelopWindow.activeSelf)
            {
                DrinkDevelop.transform.Rotate(0, 0, -0.7f *100* Time.deltaTime);
            }  
            else if(DrinkDevelop.transform.rotation.z >= -3.14f * 3f / 360f && DrinkDevelop.transform.rotation.z <= 3.14f * 3f / 360f)
            {
                DrinkDevelop.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (DrinkhaveDevelop == true && tm.TimeData.DevelopTime <= 0)
                {
                    DevelopBtn.GetComponentInChildren<Text>().text = "完成";
                }
                DevelopBtn.GetComponent<Button>().enabled = true;

                if (ui.DrinkWindow.activeSelf == true && ui.DevelopWindow.activeSelf == true && !ui.checkLittleActive() && star == true)
                {
                    GameObject FX = Instantiate(Resources.Load("Prefabs/CFX_Star"), transform) as GameObject;
                    AudioManager.self.PlaySound("Developdone");
                    star = false;
                }
            }
            else if (!ui.DevelopWindow.activeSelf)
            {
                if (DrinkhaveDevelop == true)
                {
                    DevelopBtn.GetComponentInChildren<Text>().text = "完成";
                }
                DevelopBtn.GetComponent<Button>().enabled = true;
                DrinkDevelop.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
               
            //DrinkDevelop.transform.DORotate(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.OutCubic);
           /* if (tm.TimeData.DevelopTime == 0 && ui.DevelopWindow.activeSelf == false)
            {
                ui.drinkNotify.SetActive(true);
            }*/
        }
        if (Time.time > NowTime + 1.0f)
        {
            if (tm.TimeData.DevelopTime > 0)
            {
                tm.TimeData.DevelopTime--;
               /* int T = tm.TimeData.DevelopTime;
                DevelopBtn.GetComponentInChildren<Text>().text = (T/3600).ToString("00") + ":" + (T%3600/60).ToString("00") + ":" + (T%3600%60).ToString("00");*/
                ui.developfastBtn.SetActive(true);
                if (tm.TimeData.DevelopTime <= 0)
                {
                    ui.developfastBtn.SetActive(false);
                }
            }
            else if (tm.TimeData.DevelopTime == 0)
            {
                DevelopBtn.GetComponentInChildren<Text>().text = "00:00:00";
                star = true;
                HaveDevelop();
                if (ui.DrinkWindow.activeSelf == false || ui.DevelopWindow.activeSelf == false)
                {
                    ui.drinkNotify.SetActive(true);
                    AudioManager.self.PlaySound("notify");
                    ui.dNotify.SetActive(true);
                }
            }
            else if (tm.TimeData.DevelopTime < 0)
            {
                ui.developfastBtn.SetActive(false);
            }
            for (int i = 0; i < pm.Player.DrinkSum; i++)
            {
                if (tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) > 0)
                {
                    tm.TimeData.setMakeTime(pm.Player.getCanMake(i), tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) - 1);
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作中";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = false;
                    drinksmake[i].transform.GetChild(3).GetComponent<Image>().sprite = ui.RRedBtn;
                    drinksmake[i].transform.GetChild(5).gameObject.SetActive(true);
                    drinksmake[i].transform.GetChild(5).GetComponentInChildren<Text>().text = tm.TimeData.getMakeTime(pm.Player.getCanMake(i)).ToString();
                }
                else if (tm.TimeData.getMakeTime(pm.Player.getCanMake(i)) == 0)
                {
                    int a = pm.Player.getCanMake(i);
                    pm.Player.setDrinkinStock(a, tm.TimeData.getMakeTemp(a) + pm.Player.getDrinkinStock(a));
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = true;
                    drinksmake[i].transform.GetChild(3).GetComponent<Image>().sprite = ui.RYellowBtn;
                    tm.TimeData.setMakeTime(a, -1);
                    drinksmake[i].transform.GetChild(5).gameObject.SetActive(false);
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
                    staffs[i].transform.GetChild(2).GetComponent<Button>().enabled = false;
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
            EventUseTime = UnityEngine.Random.Range(eventmin, eventmax);
        }

    }
    public void frontBtnChange()
    {
        if (pm.Player.getHavetheStaff(0) == true)
        {
            staffs[0].transform.GetChild(2).GetComponent<Image>().sprite = ui.SYellowBtn;
        }
        if (pm.Player.getHavetheStaff(1) == true)
        {
            staffs[1].transform.GetChild(2).GetComponent<Image>().sprite = ui.SYellowBtn;
        }
        if (pm.Player.getHavetheStaff(2) == true)
        {
            staffs[2].transform.GetChild(2).GetComponent<Image>().sprite = ui.SYellowBtn;
        }
        if (pm.Player.getHavetheStaff(3) == true)
        {
            staffs[3].transform.GetChild(2).GetComponent<Image>().sprite = ui.SYellowBtn;
        }
        int f = pm.Player.FrontStaff;
        staffs[f].transform.GetChild(2).GetComponent<Image>().sprite = ui.SPinkBtn;
    }
    public void LoadDefaultMT()
    {
        if (JsonUtility.FromJson<MissionList>(PlayerPrefs.GetString("defaultmissionsave"))!= null)
        {
            saveandLoad.loadDefault();
            pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData = saveandLoad.Time;
        }
        
    }
    public void lucky()
    {
        int L = pm.Player.Level-1;
        Lucky.GetComponent<SpriteRenderer>().sprite = gm.Level.LuckyData[L].Image;
    }
    public void sellDrinks()
    {
        bool isnew = false;
        int c = -1, d = -1;
        bool hs = false;
        ClientControl.SelltheDrink(pm.Player, ref c, ref isnew, ref d,ref hs);
        SellingAnime.self.havestock = hs;
        SellingAnime.self.C = c;
        SellingAnime.self.D = d;
        SellingAnime.self.Come();
    }
    public void headerInfo()
    {
        moneytext.text =  pm.Player.Money.ToString("000000");
        if(pm.Player.Money > 999999)
        {
            moneytext.text = 999999 + "+";
        }
        selltext.text =  pm.Player.DrinkSell.ToString("000000");
        if (pm.Player.DrinkSell > 999999)
        {
            moneytext.text = 999999 + "+";
        }
        leveltext.text = "Lv.";
        if (pm.Player.Level >= 1)
        {
            stars.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/starA_c");
        }
        if (pm.Player.Level >= 2)
        {
            stars.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/starA_c");
        }
        if (pm.Player.Level >= 3)
        {
            stars.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/starA_c");
        }
        if (pm.Player.Level >= 4)
        {
            stars.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/starA_c");
        }
        if (pm.Player.Level >= 5)
        {
            stars.transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/starA_c");
        }

    }
   
    public void PressADpromote()
    {
        ui.lookadWindow.transform.GetChild(0).GetComponent<Text>().text = "大量吸引顧客";
        ui.OpenLookAD();
        ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {/* ui.shutdownLittle();*/ ad.PlayAD("rewardedVideo", "adpromote",0); });
    }
    public void adpromote() 
    {
        promoteTime = Time.time;
        sellbetweenTime = gm.Client.ComeTime.ADMin;
        promotelasting = gm.Client.ComeTime.adpromoteTime;
        ClientControl.PromoteSell(ref ComeTimeMin,ref ComeTimeMax, ClientControl.PromoteType.AD);
        ui.adBtn.GetComponent<Button>().interactable = false;
        ui.manualBtn.GetComponent<Button>().interactable = false;
        Debug.Log("來客秒數" + ComeTimeMin + "~" + ComeTimeMax);
    }
    public void manualpromote()
    {
        AudioManager.self.PlaySound("Promote");
        Animator promoteBtnAni = promoteBtn.GetComponent<Animator>();
        promoteBtnAni.SetTrigger("click");
        sellbetweenTime = gm.Client.ComeTime.ManualMin;
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
                if (ui.EventWindow.activeSelf == false)
                {
                    ui.EventNotify();
                }
                ms.Mission.Missions[i].isReach = false;
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
            missions[i].transform.GetChild(0).GetComponent<Text>().text = gm.Mission.MissionInfo[i].Name;
            missions[i].transform.GetChild(1).GetComponent<Text>().text = gm.Mission.MissionInfo[i].Narrate;
            int a = i;
            missions[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { AudioManager.self.PlaySound("Click");  missionreward(a); });
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
            ui.rewardWindow.GetComponentInChildren<Text>().text = "獲得獎金" + gm.Mission.MissionInfo[i].Reward;
            ui.OpenReward();
            missions[i].transform.GetChild(2).GetComponent<Button>().enabled = false;
            missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "已領獎";
            missions[i].transform.GetChild(2).GetComponent<Image>().sprite = ui.DarkDBtn;
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
                missions[i].transform.GetChild(2).GetComponent<Image>().sprite = ui.BrightDBtn;
            }
            else if (ms.Mission.Missions[i].isActive == false && ms.Mission.Missions[i].isRewarded == true)
            {
                missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "已領獎";
                missions[i].transform.GetChild(2).GetComponent<Image>().sprite = ui.DarkDBtn;
            }
            else
            {
                missions[i].transform.GetChild(2).GetComponentInChildren<Text>().text = "未完成";
                missions[i].transform.GetChild(2).GetComponent<Image>().sprite = ui.DarkDBtn;
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
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { AudioManager.self.PlaySound("Click"); SellingAnime.self.ChangeStaff(a); ui.shutdownAll(); });
                staffs[i].transform.GetChild(3).GetComponent<Text>().text =gm.Staff.StaffData[i].Info;
                staffs[i].transform.GetChild(4).GetComponent<Text>().text = gm.Staff.StaffData[i].Chara;
                staffs[i].transform.GetChild(5).GetComponent<Text>().text = gm.Staff.StaffData[i].Name;
            }
            else
            {
                staffs[i] = Instantiate(Resources.Load("Prefabs/lockstaff"), StaffContent.transform) as GameObject;
                staffs[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Staff.StaffData[i].UnImage;
                staffs[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
                int a = i;
                staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { pressUnlockfast(a); });
                staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { PressUnlock(a); });
                staffs[i].transform.GetChild(3).GetComponent<Text>().text = "解鎖條件\n"+gm.Staff.StaffData[i].UnlockLevel+"星\n"+"資金"+gm.Staff.StaffData[i].UnlockCost;
            }
            
        }
    }
    public void AddStaffMenu(int i)
    {
        if (ui.StaffWindow.activeSelf == false)
        {
            ui.staffNotify.SetActive(true);
            AudioManager.self.PlaySound("notify");
        }
        pm.Player.setHavetheStaff(i, true);
        pm.Player.StaffSum++;
        Destroy(staffs[i].gameObject);
        staffs[i] = Instantiate(Resources.Load("Prefabs/unlockstaff"), StaffContent.transform) as GameObject;
        staffs[i].transform.SetSiblingIndex(i);
        staffs[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Staff.StaffData[i].Image;
        staffs[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ReadStory(i); });
        staffs[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { AudioManager.self.PlaySound("Click"); SellingAnime.self.ChangeStaff(i); ui.shutdownAll(); });
        staffs[i].transform.GetChild(3).GetComponent<Text>().text = gm.Staff.StaffData[i].Info;
        staffs[i].transform.GetChild(4).GetComponent<Text>().text = gm.Staff.StaffData[i].Chara;
        staffs[i].transform.GetChild(5).GetComponent<Text>().text = gm.Staff.StaffData[i].Name;
    }
    public void PressUnlock(int i)
    {
        AudioManager.self.PlaySound("Click");
        ui.OpenStaffCost();
        ui.staffcostWindow.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        ui.staffcostWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {UnlockStaff(i); ui.shutdownLittle(); });
        if (pm.Player.Money >= gm.Staff.StaffData[i].UnlockCost && pm.Player.Level >= gm.Staff.StaffData[i].UnlockLevel)
        {
            ui.staffcostWindow.GetComponentInChildren<Text>().text = "需花費" + gm.Staff.StaffData[i].UnlockCost;
            ui.staffcostWindow.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else
        {
            ui.staffcostWindow.GetComponentInChildren<Text>().text = "條件不足";
            ui.staffcostWindow.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
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
    public void pressUnlockfast(int i)
    {
        if (pm.Player.DeleteAD == false)
        {
            ui.lookadWindow.transform.GetChild(0).GetComponent<Text>().text = "減少等待時間";
            ui.OpenLookAD();
            ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {/* ui.shutdownLittle();*/ /*unlockstaffFast(i);*/ad.PlayAD("rewardedVideo", "unlockstaffFast(i)", i); });
        }
        else
        {
            unlockstaffFast(i);
        }
    }
    public void unlockstaffFast(int i)
    {
        //tm.TimeData.setStaffUnlockTime(i,tm.TimeData.getStaffUnlockTime(i)/60);
        tm.TimeData.setStaffUnlockTime(i, 0);
    }
    
    public void Purchase(int type)
    {
        AudioManager.self.PlaySound("Click");
        if (type >= 10000)
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
        if ((pm.Player.ThisOpenTime - pm.Player.LastEndTime).TotalMinutes >=10)
        {
            ClientControl.WhenNotPlayingSell(pm.Player, ref TempMoney, ref LeaveNarrate);
            ui.OpenNotice();
        }
        
    }
    public void Recapture()
    {
        AudioManager.self.PlaySound("Click");
        ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate 
        { /*ui.shutdownNotice(); ui.shutdownLittle();*//* pm.Player.Money += TempMoney;*/ ad.PlayAD("rewardedVideo", "Recapture", TempMoney); });
        ui.lookadWindow.transform.GetChild(0).GetComponent<Text>().text = "取回少賺的利益";
        ui.OpenLookAD();
        
    }
    public void Incidenthappen()
    {
        if (ui.EventWindow.activeSelf == false)
        {
            ui.EventNotify();
        }
        int i = UnityEngine.Random.Range(1, 6);
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
            AudioManager.self.PlaySound("Click");
            DrinkhaveDevelop = false;
            csText.GetComponent<Text>().text = "";
            DevelopBtn.GetComponentInChildren<Text>().text = "研發";
            DoneDevelopText.GetComponent<Text>().text = "？？？？？";
            DrinkDevelop.GetComponent<Image>().sprite = DrinkDefault;
            star = false;
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
    public void pressDevelopFast()
    {
        if (pm.Player.DeleteAD == false && tm.TimeData.DevelopTime > 30)
        {
            ui.lookadWindow.transform.GetChild(0).GetComponent<Text>().text = "減少等待時間";
            ui.OpenLookAD();
            ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            ui.lookadWindow.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { /*ui.shutdownLittle()*/;if (tm.TimeData.DevelopTime > 30) ad.PlayAD("rewardedVideo", "developFast", 0);});
           // int T = tm.TimeData.DevelopTime = tm.TimeData.DevelopTime / 120;
           // DevelopBtn.GetComponentInChildren<Text>().text = (T / 3600).ToString("00") + ":" + (T % 3600 / 60).ToString("00") + ":" + (T % 3600 % 60).ToString("00");

        }
        else
        {
            AudioManager.self.PlaySound("fast");
            developFast();
        }
    }
    public void developFast()
    {
        //ad
        /*if (tm.TimeData.DevelopTime > 30) {
            
            tm.TimeData.DevelopTime = tm.TimeData.DevelopTime / 60;
           
        }
        else if(tm.TimeData.DevelopTime > 0)
        {
            tm.TimeData.DevelopTime = Mathf.Clamp(tm.TimeData.DevelopTime-2,0,30);
        }*/
        tm.TimeData.DevelopTime = 0;

    }
    public void fastButtonColor()
    {
        if (tm.TimeData.DevelopTime > 30)
        {
            ui.developfastBtn.GetComponent<Image>().sprite = ui.SBlueBtn;
        }
        else if (tm.TimeData.DevelopTime > 0)
        {
            ui.developfastBtn.GetComponent<Image>().sprite = ui.SPinkBtn;
        }
        else
        {
            ui.developfastBtn.SetActive(false);
        }
       
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
        //DevelopBtn.GetComponentInChildren<Text>().text = "完成";
        //DevelopBtn.GetComponent<Button>().enabled = true;

    }
    public void CoinChange()
    {
        coinText.GetComponent<Text>().text = "代幣：";
        if (pm.Player.Coin == 3)
        {
            coinText.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            coinText.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            coinText.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        }
        if (pm.Player.Coin == 2)
        {
            coinText.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            coinText.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            coinText.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
        }
        if (pm.Player.Coin == 1)
        {
            coinText.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            coinText.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            coinText.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
        }
        if (pm.Player.Coin == 0)
        {
            coinText.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            coinText.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            coinText.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
        }
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
        if (ui.ClientWindow.activeSelf == false && pm.Player.getHavetheClient(i) == false)
        {
            ui.clientNotify.SetActive(true);
            AudioManager.self.PlaySound("notify");
            clients[i].transform.GetChild(2).gameObject.SetActive(true);
            Debug.Log( i + "客來了");
        }
        clients[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(i, objectType.Client); clients[i].transform.GetChild(2).gameObject.SetActive(false); });
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
                drinks[i].transform.GetChild(2).GetComponent<Text>().text = "售價 " + gm.Drink.DrinkData[i].Price.ToString();
            }

        }
    }
    public void AddDrinkMenu(int i)
    {
        drinks[i].GetComponent<Button>().onClick.AddListener(delegate { PressObject(i, objectType.Drink); });
        drinks[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
        drinks[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
        drinks[i].transform.GetChild(2).GetComponent<Text>().text = "售價 " + gm.Drink.DrinkData[i].Price.ToString();
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
            drinksmake[i].transform.GetChild(2).GetComponent<Text>().text = "成本 "+gm.Drink.DrinkData[a].Cost.ToString();
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
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(2).GetComponent<Text>().text = "成本 " + gm.Drink.DrinkData[i].Cost.ToString();
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(i); });
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-405, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 510 + pm.Player.DrinkSum / 3 * 510);
    }
    public void DrinkMakeOnClick(int i)
    {

        if (pm.Player.getDrinkinStock(i) < ReplenishAmount)
        {
            AudioManager.self.PlaySound("Replenish");
            int make = -1, maketime = -1;
            DrinkControl.MakingDrink(i, pm.Player, ref make, ref maketime);
            tm.TimeData.setMakeTemp(i, make);
            tm.TimeData.setMakeTime(i, maketime);
        }
        else
        {
            ui.OpenStockLimit();
        }

    }
    
    public void CanReplenish()
    {
        Replenishment.transform.GetChild(0).GetComponent<Button>().interactable = true;
        Replenishment.transform.GetChild(1).GetComponent<Button>().interactable = false;
        Replenishment.transform.GetChild(2).GetComponent<Button>().interactable = false;
        Replenishment.transform.GetChild(3).GetComponent<Button>().interactable = false;
        if (pm.Player.AddStockLimit >= 25)
        {
            Replenishment.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        if (pm.Player.AddStockLimit >= 50)
        {
            Replenishment.transform.GetChild(2).GetComponent<Button>().interactable = true;
        }
        if (pm.Player.AddStockLimit >= 75)
        {
            Replenishment.transform.GetChild(3).GetComponent<Button>().interactable = true;
        }
    }
    public void Setreplenish(int i)
    {
        AudioManager.self.PlaySound("Click");
        ReplenishAmount = i;
    }
    public void RAColor()
    {
        Replenishment.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        Replenishment.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        Replenishment.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        Replenishment.transform.GetChild(3).GetComponent<Image>().color = Color.white;
        int a = ReplenishAmount / 25 - 1;
        Replenishment.transform.GetChild(a).GetComponent<Image>().color = new Color32(255,200,0,255);
       
    }
    public void SEContent()
    {
        if (PlayerDataManager.self.Player.SEswitch == true)
        {
            ui.SEBtn.GetComponentInChildren<Text>().text = "ON";
            ui.SEBtn.GetComponent<Image>().color = Color.yellow;
            AudioManager.self.SEon();
        }
        else
        {
            ui.SEBtn.GetComponentInChildren<Text>().text = "OFF";
            ui.SEBtn.GetComponent<Image>().color = Color.gray;
            AudioManager.self.SEoff();
        }
    }
    public void PressSEBtn()
    {
        AudioManager.self.PlaySound("Click");
        pm.Player.SEswitch = !pm.Player.SEswitch;
    }
    public void BGMContent()
    {
        if (PlayerDataManager.self.Player.BGMswitch == true)
        {
            ui.BGMBtn.GetComponentInChildren<Text>().text = "ON";
            ui.BGMBtn.GetComponent<Image>().color = Color.yellow;
            AudioManager.self.BGMon();
        }
        else
        {
            ui.BGMBtn.GetComponentInChildren<Text>().text = "OFF";
            ui.BGMBtn.GetComponent<Image>().color = Color.gray;
            AudioManager.self.BGMoff();
        }
    }
    public void PressBGMBtn()
    {
        AudioManager.self.PlaySound("Click");
        pm.Player.BGMswitch = !pm.Player.BGMswitch;
        Debug.Log("BGM"+ pm.Player.BGMswitch);
    }

    public void OnApplicationPause()
    {
        if (Back == true && pm.Player.FirstTime == false)
        {
            LeaveTimeCaculator();
        }
        if (pm.Player.FirstTime == true && Back == false)
        {
            pm.Player.FirstTime = false;
        }
        if (Back == false && ad.ADback == false)
        {
            pm.Player.LastEndTime = DateTime.Now;
            pm.Player.Endtimestring = pm.Player.LastEndTime.ToString();
            saveandLoad.Save(pm.Player, ms.Mission, tm.TimeData);
        }
        if (ad.ADback == true)
        {
            Back = false;
        }
        else 
        {
            Back = true;
        }
       /* ui.shutdownLittle();
        ui.shutdownAll();
        ui.shutdownLevelup();*/

        Debug.Log("save");
    }
    void OnApplicationQuit()
    {
        if (pm.Player.FirstTime == true)
        {
            pm.Player.FirstTime = false;
        }
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