using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCalculator : MonoBehaviour  //Testing script
{
    private DateTime Time1, Time2;
    private TimeSpan Differ;
    public GameObject time1, time2, differ;
    public GameDataManager Drink;
    public PlayerDataManager Player;
    private DrinkControl DrinkControl = new DrinkControl();
    private ClientControl ClientControl = new ClientControl();
    // Start is called before the first frame update
    void Start()
    {
        Drink.DrinkByChara();
        Drink.ClientByLevel();
        Player.Default();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {


        /*   List<int> SpecialList= new List<int>();
           for (int i=0;i< Drink.DrinkDataList.Count;i++)
           {
               if (Drink.DrinkDataList[i].isSpecial)
                   SpecialList.Add(i); 
           }
           int a = UnityEngine.Random.Range(0, SpecialList.Count);
           Debug.Log(SpecialList[a] + "/"+ Drink.DrinkDataList[SpecialList[a]].isSpecial);
           Debug.Log(Player.PlayerData.FirstTime+"  "+ Player.PlayerData.Money+" " + Player.PlayerData.HavetheDrink.Count);
           Debug.Log(Player.PlayerData.HavetheDrink[0]);*/
        //int Select = DrinkControl.DevelopDrink(Drink,Player);
        // Debug.Log(Select);
        // ClientControl.SelltheDrink(Player,Drink);
        ClientControl.WhenNotPlayingSell(Player, Drink);
    }
    

    public void Time1click()
    {
        Time1 = DateTime.Now;
        Player.PlayerData.LastEndTime = Time1;
        time1.GetComponent<Text>().text = Time1.ToString();
    }

    public void Time2click()
    {
        Time2 = DateTime.Now;
        Player.PlayerData.ThisOpenTime = Time2;
        time2.GetComponent<Text>().text = Time2.ToString();
    }

    public void Differclick()
    {
        Differ = Time2 - Time1;
        differ.GetComponent<Text>().text = ((int)Differ.TotalSeconds).ToString();
    }
}
