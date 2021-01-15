using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingAnime : MonoBehaviour
{
    public static SellingAnime self;
    public Animator DrinkAni, ClientAni, ArmAni, StaffAni;
    public bool havestock;
    public int D, C, S;
    public GameObject Drink, Client, Staff, Speak, Arm;
    public bool selling,sleeping;
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
        //Staffgosleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeStaff(int i)
    {
        S = PlayerDataManager.self.Player.FrontStaff = i;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
        Arm.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].ArmImage;
    }
    public void Staffgosleep()
    {
        sleeping = true;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].sleepImage;
    }
    public void StaffWakeup()
    {
        StaffAni.SetTrigger("wake");
        sleeping = false;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
    }
    
    public void Come()
    {
        ClientAni.SetTrigger("come");
        if (sleeping == true)
        {
            Invoke("StaffWakeup",1f);
        }
        Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].backImage;
        Client.transform.GetChild(0).gameObject.SetActive(false);
        Client.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Drink.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Speak.GetComponentInChildren<Text>().text = havestock ? GameDataManager.self.Drink.DrinkData[D].Name + "好囉~" : GameDataManager.self.Drink.DrinkData[D].Name + "沒貨了";
    }
    public void SellBegin()
    {
        selling = true;
        if (sleeping == true)
        {
            StaffWakeup();
        }
        Speak.SetActive(true);
        DrinkAni.SetTrigger("appear");
        ArmAni.SetTrigger("appear");
    }
    public void SellDone()
    {
        selling = false;
        DrinkAni.SetTrigger("disappear");
        ArmAni.SetTrigger("disappear");
    }
    public void Go()
    {
        if (havestock)
        {
            Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].happyImage;
        }
        else
        {
            if (sleeping == true)
            {
                StaffWakeup();
            }
            Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].angryImage;
        }
        Client.transform.GetChild(0).gameObject.SetActive(havestock ? true : false);
        ClientAni.SetTrigger("go");
    }
}
