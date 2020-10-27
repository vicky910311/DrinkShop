using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    public GameObject Content,IncidentWindow,Scroll;
    public GameObject DevelopBtn,DrinkDevelop,DoneDevelopText,csText;
    public bool DrinkhaveDevelop;
    // Start is called before the first frame update
    void Start()
    {

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
   
    public void OpenIncidentWindow()
    {
       if(IncidentWindow.activeSelf == true)
        {
            IncidentWindow.SetActive(false);
        }
       else
        {
            IncidentWindow.SetActive(true);
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
            DrinkControl.MakingDrink(pm.Player.getCanMake(i), pm.Player);
            Debug.Log(pm.Player.getCanMake(i) + "補齊了");
        }
    }
    void OnApplicationPause()
    {
       /* pm.Player.LastEndTime = DateTime.Now;
        saveandLoad.Save();
        Debug.Log("save");*/
    }
}
