using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingAnime : MonoBehaviour
{
    public static SellingAnime self;
    public Animator DrinkAni, ClientAni, ArmAni;
    public bool havestock;
    public int D, C, S;
    public GameObject Drink, Client, Staff, Speak;
    private void Awake()
    {
        self = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        S = PlayerDataManager.self.Player.FrontStaff;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
        Speak.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeStaff(int i)
    {
        S = PlayerDataManager.self.Player.FrontStaff = i;
        Staff.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Staff.StaffData[S].Image;
    }
    public void Staffgosleep()
    {

    }
    public void StaffWakeup()
    {

    }
    
    public void Come()
    {
        ClientAni.SetTrigger("come");
        Client.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Client.ClientData[C].Image;
        Client.transform.GetChild(0).gameObject.SetActive(false);
        Client.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Drink.GetComponent<SpriteRenderer>().sprite = GameDataManager.self.Drink.DrinkData[D].Image;
        Speak.GetComponent<Text>().text = havestock ? GameDataManager.self.Drink.DrinkData[D].Name + "好囉~" : GameDataManager.self.Drink.DrinkData[D].Name + "沒貨了";
    }
    public void SellBegin()
    {
        Speak.SetActive(true);
        DrinkAni.SetTrigger("appear");
        ArmAni.SetTrigger("appear");
    }
    public void SellDone()
    {
        DrinkAni.SetTrigger("disappear");
        ArmAni.SetTrigger("disappear");
    }
    public void Go()
    {
        Client.transform.GetChild(0).gameObject.SetActive(havestock ? true : false);
        ClientAni.SetTrigger("go");
    }
}
