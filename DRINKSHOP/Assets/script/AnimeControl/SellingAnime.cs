using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingAnime : MonoBehaviour
{
    public static SellingAnime self;
    public Animator DrinkAni, ClientAni, ArmAni, StaffAni, InfoAni;
    public bool havestock;
    public int D, C, S;
    public GameObject Drink, Client, Staff, Speak, Arm, Info;
    public bool selling,sleeping,afraiding;
    private void Awake()
    {
        self = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        S = PlayerDataManager.self.Player.FrontStaff;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
        Arm.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].ArmImage;
        Speak.SetActive(false);
        selling = false;
        sleeping = false;
        afraiding = false;
        //Staffgosleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SellInfo()
    {
        //數字
        InfoAni.SetTrigger("sell");
    }
    public void StaffAfraid()
    {
        afraiding = true;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].AfraidImage;
        StaffAni.SetTrigger("afraid");
    }
    public void StaffDontAfraid()
    {
        afraiding = false;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
    }
    public void ChangeStaff(int i)
    {
        if (i != S)
        {
            S = PlayerDataManager.self.Player.FrontStaff = i;
            Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
            Arm.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].ArmImage;
            sleeping = false;
            if (afraiding == true)
            {
                StaffAfraid();
            }
        }
        
    }
    public void Staffgosleep()
    {
        sleeping = true;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].sleepImage;
        Speak.SetActive(false);
    }
    public void StaffWakeup()
    {
        if (sleeping == true)
        {
            Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;

            sleeping = false;
        }
        StaffAni.SetTrigger("wake");

    }
    
    public void Come()
    {
        ClientAni.SetTrigger("come");
       /* if (sleeping == true)
        {
            Invoke("StaffWakeup",1f);
        }*/
        Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].backImage;
        Client.transform.GetChild(0).gameObject.SetActive(false);
        Client.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Drink.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Speak.GetComponentInChildren<Text>().text = havestock ? GameDataManager.self.Drink.DrinkData[D].Name + "好囉~" : GameDataManager.self.Drink.DrinkData[D].Name + "沒貨了";
    }
    public void SellBegin()
    {
        
        /*   if (sleeping == true)
           {
               StaffWakeup();
           }*/
        if (sleeping == false)
        {
            selling = true;
            Speak.SetActive(true);
            DrinkAni.SetTrigger("appear");
            ArmAni.SetTrigger("appear");
        }

    }
    public void SellDone()
    {
        selling = false;
        if (sleeping == false)
        {
            
            DrinkAni.SetTrigger("disappear");
            ArmAni.SetTrigger("disappear");
        }
        
    }
    public void Go()
    {
        bool havesell = false;
        if (havestock && sleeping == false)
        {
            havesell = true;
            Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].happyImage;
        }
        else
        {
            /*if (sleeping == true)
            {
                StaffWakeup();
            }*/
            Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].angryImage;
        }
        Client.transform.GetChild(0).gameObject.SetActive(havesell ? true : false);
        ClientAni.SetTrigger("go");
    }
}
