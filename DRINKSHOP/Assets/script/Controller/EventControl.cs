using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class EventControl
{
    //public PlayerData Player;
    //public MissionList Mission;
    public DrinkDataList Drink;
    public LevelDataList Level;
    public void PlayerAchieveMission(PlayerData Player,MissionList Mission)
    {
        for (int i = 0;i< Mission.Missions.Count;i++)
        {
            if(Mission.Missions[i].isActive == true)
            {
                if (Mission.Missions[i].Type == 0 )
                {
                    if (Player.CatchGhost >= Mission.Missions[i].NeedAmount)
                    {
                        Debug.Log("完成抓鬼任務  抓 " + Mission.Missions[i].NeedAmount + " 次");
                        Mission.Missions[i].isActive = false;
                        Mission.Missions[i].isReach = true;
                    }                 
                }
                if (Mission.Missions[i].Type == MissionType.GhostHunt)
                {
                    if (Player.CatchSleep >= Mission.Missions[i].NeedAmount)
                    {
                        Debug.Log("完成叫醒任務  叫 " + Mission.Missions[i].NeedAmount + " 次");
                        Mission.Missions[i].isActive = false;
                        Mission.Missions[i].isReach = true;
                    }       
                }
                if (Mission.Missions[i].Type == MissionType.ReachSell)
                {
                    if (Player.DrinkSell >= Mission.Missions[i].NeedAmount)
                    {
                        {
                            Debug.Log("完成賣出任務  賣 " + Mission.Missions[i].NeedAmount + " 杯");
                            Mission.Missions[i].isActive = false;
                            Mission.Missions[i].isReach = true;
                        }
                    }
                }
                
                if (Mission.Missions[i].Type == MissionType.UnlockClient)
                {
                    if (Player.ClientSum >= Mission.Missions[i].NeedAmount)
                    {
                        {
                            Debug.Log("完成來客任務  來 " + Mission.Missions[i].NeedAmount + " 位");
                            Mission.Missions[i].isActive = false;
                            Mission.Missions[i].isReach = true;
                        }
                    }
                }
                if (Mission.Missions[i].Type == MissionType.Poor)
                {
                    if (Player.Money <= Mission.Missions[i].NeedAmount)
                    {
                        {
                            Debug.Log("急難救助金  財產低於 " + Mission.Missions[i].NeedAmount + " 元");
                            Mission.Missions[i].isActive = false;
                        }
                    }
                }

            }
       
           
        }
       
    }
    public bool CanReward(int i, MissionList Mission)
    {
        bool canreward = false;
        if (Mission.Missions[i].isActive == false && Mission.Missions[i].isRewarded == false)
        {
            Debug.Log("可領獎");
            canreward = true;
        }
        else if (Mission.Missions[i].isActive == false && Mission.Missions[i].isRewarded == true)
        {
            Debug.Log("已領獎");
            canreward = false;
        }
        else
        {
            Debug.Log("未完成");
            canreward = false;
        }
        return canreward;
    }
    public void GetReward(int i, MissionList Mission,PlayerData Player)
    {
        if (Mission.Missions[i].isActive == false && Mission.Missions[i].isRewarded == false)
        {
            Debug.Log("領獎");
            Mission.Missions[i].isRewarded = true;
            Player.Money += Mission.Missions[i].Reward;

        }
        else if (Mission.Missions[i].isActive == false && Mission.Missions[i].isRewarded == true)
        {
            Debug.Log("已領獎");
        }
        else
        {
            Debug.Log("未完成");
        }
    }
    public void IncidentHappen(int j,string Narrate,PlayerData Player)
    {
        if (j == 1)
        {
            Narrate = "鬧鬼了";
            //GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), transform) as GameObject;
        }
        if (j == 2)
        {
            Narrate = "店員睡著了";
        }
        if (j == 3)
        {
            int Select;
            List<int> CanSell = new List<int>();
            for (int i = 0; i < Player.HavetheDrink.Count; i++)
            {
                if (Player.HavetheDrink[i])
                    CanSell.Add(i);
            }
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            if ((int)Random.Range(0,10)>0)
            {
                Narrate = Select + "被買光了";
                int buy = Player.DrinkinStock[Select];
                Player.DrinkinStock[Select] = 0;
                Player.Money += Drink.DrinkData[Select].Price*buy;
            }
            else
            {
                Narrate = "全店被買光了";
                for (int i = 0; i < Player.HavetheDrink.Count; i++)
                {
                    if (Player.HavetheDrink[i])
                    {
                        int buy = Player.DrinkinStock[i];
                        Player.DrinkinStock[i] = 0;
                        Player.Money += Drink.DrinkData[i].Price * buy;
                    }
                        
                }
            }
            
        }
        if (j == 4)
        {
            int stole =((int)Random.Range(1,3))*100;
            Player.Money -= stole;
            Narrate = "被偷了" + stole + "元";
        }
        Debug.Log(Narrate);
    }
    public void LevelUp(PlayerData Player)
    {
        int MotoLevel = Player.Level;
        if (Player.DrinkSum >= Level.LevelUpData[4].DrinkSum && Player.DrinkSell >= Level.LevelUpData[4].SellSum)
        {
            Player.Level = 5;
        }
        else if (Player.DrinkSum >= Level.LevelUpData[3].DrinkSum && Player.DrinkSell >= Level.LevelUpData[3].SellSum)
        {
            Player.Level = 4;
        }
        else if (Player.DrinkSum >= Level.LevelUpData[2].DrinkSum && Player.DrinkSell >= Level.LevelUpData[2].SellSum)
        {
            Player.Level = 3;
        }
        else if (Player.DrinkSum >= Level.LevelUpData[1].DrinkSum && Player.DrinkSell >= Level.LevelUpData[1].SellSum)
        {
            Player.Level = 2;
        }
        else
        {
            Player.Level = 1;
        }
        if (Player.Level> MotoLevel)
        {
            Debug.Log("升級了");
        }
    }
}
