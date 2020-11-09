using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    
    public static Testing self;
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
       /*if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsonplayersave"))!= null)
        {
            saveandLoad.Load();
            pm.Player = saveandLoad.Player;
            ms.Mission = saveandLoad.Mission;
            tm.TimeData = saveandLoad.Time;
            Debug.Log("Loading");
        }*/
    }
    /*public PlayerDataManager pm;
    public MissionState ms;
    public Timer tm;
    public GameDataManager gm;
    public UIManager ui;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    private StaffControl StaffControl = new StaffControl();
    private EventControl EventControl = new EventControl();
    public SaveandLoad saveandLoad = new SaveandLoad();
    public float NowTime;
    public float EventHappenTime,EventUseTime;
    public GameObject Content,IncidentWindow, MenuContent,MakeContent,ClientContent,SCContent;
    public GameObject DevelopBtn,DrinkDevelop,DoneDevelopText,csText,limitWindow,coinText;
    public bool DrinkhaveDevelop;
    private GameObject[] drinks, clients;
    private List<GameObject> drinksmake = new List<GameObject>();
    public int TempMoney;
    public string LeaveNarrate;
    public bool Back;
    public GameObject ghostin;*/
    void Start()
    {
        GameManager.self.drinks = new GameObject[GameManager.self.gm.Drink.DrinkData.Count];
        GameManager.self.clients = new GameObject[GameManager.self.gm.Client.ClientData.Count];
        GameManager.self.missions = new GameObject[GameManager.self.ms.Mission.Missions.Count];
        GameManager.self.DrinkMenu();
        GameManager.self.DrinkMakeMenu();
        GameManager.self.ClientMenu();
        GameManager.self.MissiomMenu();
        GameManager.self.staffs = new GameObject[GameManager.self.gm.Staff.StaffData.Count];
        GameManager.self.StaffMenu();
        for (int i = 0; i < GameManager.self.gm.Drink.DrinkData.Count; i++)
        {
            GameManager.self.pm.Player.OnDrinkinStockChanged[i] += GameManager.self.StockAmount;
        }
        GameManager.self.StockAmount();
        /*pm.Player.OnCoinChange += CoinChange;
        drinks = new GameObject[gm.Drink.DrinkData.Count];
        clients = new GameObject[gm.Client.ClientData.Count];
        
        pm.Player.ThisOpenTime = DateTime.Now;
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = gm.Drink;   
        ClientControl.Client = gm.Client;
        EventControl.Level = gm.Level;
        StaffControl.Staff = gm.Staff;
        EventUseTime = UnityEngine.Random.Range(5f, 10f);
        NowTime = EventHappenTime = Time.time;
        
        DrinkMenu();
        DrinkMakeMenu();
        ClientMenu();
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-451, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 580 + pm.Player.DrinkSum / 3 * 580);
        coinText.GetComponent<Text>().text = "代幣數量：" + pm.Player.Coin;
        ClientControl.WhenNotPlayingSell(pm.Player, ref TempMoney,ref LeaveNarrate);
        Debug.Log("少賺：" + TempMoney);
        ui.OpenNotice();
        for (int i=0;i < gm.Drink.DrinkData.Count;i++ )
        {
            pm.Player.OnDrinkinStockChanged[i] += StockAmount;
        }
        StockAmount();
        Back = true;
       
        GameManager.self.missions = new GameObject[ms.Mission.Missions.Count];
        GameManager.self.MissiomMenu();
        GameManager.self.staffs = new GameObject[gm.Staff.StaffData.Count];
        GameManager.self.StaffMenu();*/

    }
    // Update is called once per frame
    /*
    void Update()
    {
        if(pm.Player.LastEndTime> pm.Player.ThisOpenTime)
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

        if (Time.time > NowTime + 1.0f)
        {
            if (tm.TimeData.DevelopTime > 0)
            {
                tm.TimeData.DevelopTime--;
                Debug.Log(tm.TimeData.DevelopTime);
                DevelopBtn.GetComponentInChildren<Text>().text = tm.TimeData.DevelopTime.ToString();
            }
            else if(tm.TimeData.DevelopTime == 0)
            {
                HaveDevelop();
            }
            for (int i = 0;i < pm.Player.DrinkSum;i++)
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
                    pm.Player.setDrinkinStock(a, tm.TimeData.getMakeTemp(a)+pm.Player.getDrinkinStock(a));
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = true;
                    tm.TimeData.setMakeTime(a, -1);
                    //drinksmake[i].transform.GetChild(4).GetComponent<Text>().text = pm.Player.getDrinkinStock(i).ToString();
                }
            }
            
          
            NowTime = Time.time;
        }
        if (Time.time > EventHappenTime + EventUseTime)
        {
            Incidenthappen();
            EventHappenTime = Time.time;
            EventUseTime = UnityEngine.Random.Range(5f,10f);
        }
        
        
            
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
        if(i == 1)
        {
            if(ghostin.transform.childCount < 10)
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
        rt.sizeDelta += new Vector2(0,200);
        
    }
    public void SellDrinks()
    {
        bool isnew = false;
        int c = -1, d = -1;
        ClientControl.SelltheDrink(pm.Player,ref c,ref isnew,ref d);
       
        
    }
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
        for (int i = 0;i< pm.Player.DrinkSum;i++)
        {
            int make = -1, maketime = -1;
            DrinkControl.MakingDrink(pm.Player.getCanMake(i), pm.Player,ref make,ref maketime);
            Debug.Log(pm.Player.getCanMake(i) + "補齊了");
        }
    }
    public void ClientMenu()
    {
       for (int i = 0; i < gm.Client.ClientData.Count; i++)
        {
            clients[i] = Instantiate(Resources.Load("Prefabs/client"), ClientContent.transform) as GameObject;
            if (pm.Player.getHavetheClient(i) == true )
            {
                clients[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
                clients[i].transform.GetChild(1).GetComponent<Text > ().text = gm.Client.ClientData[i].Name;
            }
            if (gm.Client.ClientData[i].isSpecial) { clients[i].transform.SetParent(SCContent.transform); }
        }
    }
    public void AddCientMenu(int i)
    {
        clients[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
        clients[i].transform.GetChild(1).GetComponent<Text>().text = gm.Client.ClientData[i].Name;
    }
    public void DrinkMenu()
    {
        
        for (int i = 0; i<gm.Drink.DrinkData.Count; i++)
        {
            drinks[i] = Instantiate(Resources.Load("Prefabs/drink"), MenuContent.transform) as GameObject;
            //drinks[i].transform.SetParent(MenuContent.transform);
            if (pm.Player.getHavetheDrink(i) == true)
            {
                drinks[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
                drinks[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
                drinks[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Price.ToString();
            }
           
        }
    }
    public void AddDrinkMenu(int i)
    {
        drinks[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
        drinks[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
        drinks[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Price.ToString();
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
             drinksmake[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(a);});
             Debug.Log(pm.Player.getCanMake(i));
             drinksmake[i].transform.GetChild(4).GetComponent<Text>().text = pm.Player.getDrinkinStock(a).ToString();
             Debug.Log(i +"/" + pm.Player.getDrinkinStock(a));
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
        rt.localPosition= new Vector3(-451, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 580 + pm.Player.DrinkSum / 3 * 580);
    }
    public void DrinkMakeOnClick(int i)
    { 
        if(pm.Player.getDrinkinStock(i) < pm.Player.AddStockLimit + gm.Drink.DrinkUse.StockLimit)
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
       if(Back == false)
        {
            pm.Player.LastEndTime = DateTime.Now;
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
        if (Back == false)
        {
            pm.Player.LastEndTime = DateTime.Now;
        }
        Back = true;
        ui.shutdownLittle();
        ui.shutdownAll();
        ui.shutdownLevelup();
        saveandLoad.Save(pm.Player, ms.Mission, tm.TimeData);
        Debug.Log("save");
    }
    */
}
