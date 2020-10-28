using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

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
    public GameObject Content,IncidentWindow,Scroll, MenuContent,MakeContent;
    public GameObject DevelopBtn,DrinkDevelop,DoneDevelopText,csText;
    public bool DrinkhaveDevelop;
    private GameObject[] drinks;
    private List<GameObject> drinksmake = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        drinks = new GameObject[gm.Drink.DrinkData.Count];
        
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
        GameObject narrate = Instantiate(Resources.Load("Prefabs/Text"), transform) as GameObject;
        narrate.transform.SetParent(Content.transform);
        narrate.GetComponent<Text>().text = n;
        RectTransform rt = Content.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 100, 0);
        rt.sizeDelta += new Vector2(0,200);
        
    }
    public void SellDrinks()
    {
        ClientControl.SelltheDrink(pm.Player);
        Debug.Log(pm.Player.getDrinkinStock(0));
    }
    public void Develop()
    {
        if (DrinkhaveDevelop == false)
        {
            tm.TimeData.DevelopTemp = DrinkControl.DevelopDrink(pm.Player);
            if (tm.TimeData.DevelopTemp != -1)
                tm.TimeData.DevelopTime = gm.Drink.DrinkData[tm.TimeData.DevelopTemp].DevelopTime;
            DevelopBtn.GetComponent<Button>().enabled = false;
        }
        else
        {
            DrinkhaveDevelop = false;
            csText.GetComponent<Text>().text = "";
            DevelopBtn.GetComponentInChildren<Text>().text = "研發";
            DoneDevelopText.GetComponent<Text>().text = "？？？？？";
            DrinkDevelop.GetComponent<Image>().sprite = null;
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
    public void MakeAll()
    {
        for (int i = 0;i< pm.Player.DrinkSum;i++)
        {
            int make = -1, maketime = -1;
            DrinkControl.MakingDrink(pm.Player.getCanMake(i), pm.Player,ref make,ref maketime);
            Debug.Log(pm.Player.getCanMake(i) + "補齊了");
        }
    }
    public void DrinkMenu()
    {
        
        for (int i = 0; i<gm.Drink.DrinkData.Count; i++)
        {
            drinks[i] = Instantiate(Resources.Load("Prefabs/drink"), transform) as GameObject;
            drinks[i].transform.SetParent(MenuContent.transform);
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
            drinksmake.Add(Instantiate(Resources.Load("Prefabs/drinkcanmake"), transform) as GameObject);
            drinksmake[i].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Image;
            drinksmake[i].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Name;
            drinksmake[i].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[pm.Player.getCanMake(i)].Cost.ToString();
            drinksmake[i].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(pm.Player.getCanMake(i));});
            drinksmake[i].transform.SetParent(MakeContent.transform);
            //GameObject root = PrefabUtility.FindPrefabRoot(drinksmake[i]);
            //PrefabUtility.UnpackPrefabInstance(drinksmake[i], PrefabUnpackMode.Completely, UnityEditor.InteractionMode.AutomatedAction);

        }
    }
    public void AddDrinkMakeMenu(int i)
    {
        drinksmake.Add(Instantiate(Resources.Load("Prefabs/drinkcanmake"), transform) as GameObject);
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(0).GetComponent<Image>().sprite = gm.Drink.DrinkData[i].Image;
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(1).GetComponent<Text>().text = gm.Drink.DrinkData[i].Name;
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(2).GetComponent<Text>().text = gm.Drink.DrinkData[i].Cost.ToString();
        drinksmake[pm.Player.DrinkSum - 1].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { DrinkMakeOnClick(i); });
        drinksmake[pm.Player.DrinkSum - 1].transform.SetParent(MakeContent.transform);
        //GameObject root = PrefabUtility.FindPrefabRoot(drinksmake[pm.Player.DrinkSum - 1]);
       // PrefabUtility.UnpackPrefabInstance(drinksmake[pm.Player.DrinkSum - 1], PrefabUnpackMode.Completely, UnityEditor.InteractionMode.AutomatedAction);


    }
    public void DrinkMakeOnClick(int i)
    {
        int make = -1,maketime = -1;
        DrinkControl.MakingDrink(i, pm.Player,ref make,ref maketime);
        tm.TimeData.setMakeTemp(i, make);
        tm.TimeData.setMakeTime(i, maketime);
        Debug.Log("補"+i+"  " +pm.Player.getDrinkinStock(i));
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
