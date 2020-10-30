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
    public GameObject[] staffs;
    public GameObject StaffContent;
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
        staffs = new GameObject[gm.Staff.StaffData.Count];
        StaffMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
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

