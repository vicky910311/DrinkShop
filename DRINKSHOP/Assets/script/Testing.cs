using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

public class Testing : MonoBehaviour
{
    public static Testing self;
    private void Awake()
    {
        self = this;
    }
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
    public float NowTime;
    public float EventHappenTime,EventUseTime;
    public GameObject Content,IncidentWindow, MenuContent,MakeContent,ClientContent,SCContent;
    public GameObject DevelopBtn,DrinkDevelop,DoneDevelopText,csText,limitWindow,coinText;
    public bool DrinkhaveDevelop;
    private GameObject[] drinks, clients;
    private List<GameObject> drinksmake = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        pm.Player.OnCoinChange += CoinChange;
        drinks = new GameObject[gm.Drink.DrinkData.Count];
        clients = new GameObject[gm.Client.ClientData.Count];
        /* if (pm.Player.FirstTime == false)
         {
        saveandLoad.Load();
         pm.Player = saveandLoad.Player;
         ms.Mission = saveandLoad.Mission;
         tm.TimeData = saveandLoad.Time;
         pm.Player.ThisOpenTime = DateTime.Now;
             ClientControl.WhenNotPlayingSell(pm.Player);
         }*/
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = gm.Drink;   
        ClientControl.Client = gm.Client;
        EventControl.Level = gm.Level;
        StaffControl.Staff = gm.Staff;
        EventUseTime = UnityEngine.Random.Range(5f, 10f);
        NowTime = EventHappenTime = Time.time;
        /*TimeSpan During = pm.Player.ThisOpenTime - pm.Player.LastEndTime; 
        if((int)(During).TotalSeconds > 0 && tm.TimeData.DevelopTime > 0)
        {
            tm.TimeData.DevelopTime -= (int)(During).TotalSeconds;
            tm.TimeData.DevelopTime = (int)Mathf.Clamp(tm.TimeData.DevelopTime, 0, 36000f);
        }*/
        DrinkMenu();
        DrinkMakeMenu();
        ClientMenu();
        RectTransform rt = MakeContent.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(-451, pm.Player.DrinkSum / 3 * 270, 0);
        rt.sizeDelta = new Vector2(0, 580 + pm.Player.DrinkSum / 3 * 580);
        coinText.GetComponent<Text>().text = "代幣數量：" + pm.Player.Coin;

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
                    pm.Player.setDrinkinStock(pm.Player.getCanMake(i), tm.TimeData.getMakeTemp(i)+pm.Player.getDrinkinStock(i));
                    drinksmake[i].transform.GetChild(3).GetComponentInChildren<Text>().text = "製作";
                    drinksmake[i].transform.GetChild(3).GetComponent<Button>().enabled = true;
                    tm.TimeData.setMakeTime(i, -1);
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
        EventControl.IncidentHappen(i, ref n, pm.Player);
        if(i == 1)
        {
            GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), transform) as GameObject;
        }
        GameObject narrate = Instantiate(Resources.Load("Prefabs/Text"), Content.transform) as GameObject;
        //narrate.transform.SetParent(Content.transform);
        narrate.GetComponent<Text>().text = n;
        RectTransform rt = Content.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 100, 0);
        rt.sizeDelta += new Vector2(0,200);
        
    }
    public void SellDrinks()
    {
        bool isnew = false;
        int c = -1, d = -1;
        ClientControl.SelltheDrink(pm.Player,ref c,ref isnew,ref d);
        if(isnew == true)
        {
            AddCientMenu(c);
        }
        isnew = false;
        Debug.Log(pm.Player.getDrinkinStock(0));
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
            clients[i] = Instantiate(Resources.Load("Prefabs/client"), transform) as GameObject;
            if (pm.Player.getHavetheClient(i) == true )
            {
                clients[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Client.ClientData[i].Image;
                clients[i].transform.GetChild(1).GetComponent<Text > ().text = gm.Client.ClientData[i].Name;
            }
            if (gm.Client.ClientData[i].isSpecial) { clients[i].transform.SetParent(SCContent.transform); }
            else { clients[i].transform.SetParent(ClientContent.transform); }
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
             //drinksmake[i].transform.SetParent(MakeContent.transform);
             drinksmake[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Image;
             drinksmake[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Name;
             drinksmake[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Cost.ToString();
            int a = pm.Player.getCanMake(i);
             drinksmake[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(a);});
             Debug.Log(pm.Player.getCanMake(i));
         }
      
    }
    
    public void AddDrinkMakeMenu(int i)
    {
        drinksmake.Add(Instantiate(Resources.Load("Prefabs/drinkcanmake"), MakeContent.transform) as GameObject);
        //drinksmake[pm.Player.DrinkSum - 1].transform.SetParent(MakeContent.transform);
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
    public void shutdownlimitWindow()
    {
        limitWindow.SetActive(false);
    }

    void OnApplicationPause()
    {
       /* pm.Player.LastEndTime = DateTime.Now;
        saveandLoad.Save();
        Debug.Log("save");*/
    }
    void OnApplicationQuit()
    {
        /* pm.Player.LastEndTime = DateTime.Now;
         saveandLoad.Save();
         Debug.Log("save");*/
    }

}
