using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventControl
{
    //public PlayerData Player;
    //public MissionList Mission;
    public DrinkDataList Drink;
    public LevelDataList Level;
    public MissionAssetList MissionAsset;
    public void PlayerAchieveMission(PlayerData Player,MissionList Mission)
    {
        for (int i = 0;i< Mission.Missions.Count;i++)
        {
            if(Mission.Missions[i].isActive == true)
            {
                if (MissionAsset.MissionInfo[i].Type == MissionType.GhostHunt)
                {
                    if (Player.CatchGhost >= MissionAsset.MissionInfo[i].NeedAmount)
                    {
                        Debug.Log("完成抓鬼任務  抓 " + MissionAsset.MissionInfo[i].NeedAmount + " 次");
                        Mission.Missions[i].isActive = false;
                        Mission.Missions[i].isReach = true;
                        Short = "完成任務";
                    }                 
                }
                if (MissionAsset.MissionInfo[i].Type == MissionType.WakeUp)
                {
                    if (Player.CatchSleep >= MissionAsset.MissionInfo[i].NeedAmount)
                    {
                        Debug.Log("完成叫醒任務  叫 " + MissionAsset.MissionInfo[i].NeedAmount + " 次");
                        Mission.Missions[i].isActive = false;
                        Mission.Missions[i].isReach = true;
                        Short = "完成任務";
                    }       
                }
                if (MissionAsset.MissionInfo[i].Type == MissionType.ReachSell)
                {
                    if (Player.DrinkSell >= MissionAsset.MissionInfo[i].NeedAmount)
                    {
                        {
                            Debug.Log("完成賣出任務  賣 " + MissionAsset.MissionInfo[i].NeedAmount + " 杯");
                            Mission.Missions[i].isActive = false;
                            Mission.Missions[i].isReach = true;
                            Short = "完成任務";
                        }
                    }
                }
                
                if (MissionAsset.MissionInfo[i].Type == MissionType.UnlockClient)
                {
                    if (Player.ClientSum >= MissionAsset.MissionInfo[i].NeedAmount)
                    {
                        {
                            Debug.Log("完成來客任務  來 " + MissionAsset.MissionInfo[i].NeedAmount + " 位");
                            Mission.Missions[i].isActive = false;
                            Mission.Missions[i].isReach = true;
                            Short = "完成任務";
                        }
                    }
                }
                if (MissionAsset.MissionInfo[i].Type == MissionType.Poor)
                {
                    if (Player.Money <= MissionAsset.MissionInfo[i].NeedAmount)
                    {
                        {
                            Debug.Log("急難救助金  財產低於 " + MissionAsset.MissionInfo[i].NeedAmount + " 元");
                            Mission.Missions[i].isActive = false;
                            Mission.Missions[i].isReach = true;
                            Short = "完成任務";
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
    public string Short;
    public void IncidentHappen(int j,ref string Narrate,PlayerData Player)
    {
        if (j == 1)
        {
            Narrate = "鬧鬼了";
            Short = "鬧鬼中";
            if (SellingAnime.self.sleeping == false)
            {
                SellingAnime.self.StaffAfraid();
            }
           
            //GameObject ghost = Instantiate(Resources.Load("Prefabs/yure"), transform) as GameObject;
        }
        if (j == 2)
        {
            if (SellingAnime.self.sleeping == true)
            {
                Narrate = "店員呼呼大睡";
                Short = "呼呼大睡";
            }
            else if (SellingAnime.self.selling == false && SellingAnime.self.afraiding == false)
            {
                SellingAnime.self.Staffgosleep();
                AudioManager.self.PlaySound("Sleep");
                Narrate = "店員睡著了";
                Short = "店員睡著了";
            }
            else
            {
                Narrate = "店員想睡覺";
                Short = "店員想睡覺";
            }
           
        }
        if (j == 3)
        {
            /*int Select;
            List<int> CanSell = new List<int>();
            for (int i = 0; i < Player.countHavetheDrink(); i++)
            {
                if (Player.getHavetheDrink(i))
                    CanSell.Add(i);
            }
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            if ((int)Random.Range(0,100)>0)
            {
                
                int buy = Player.getDrinkinStock(Select);
                if(buy > 0)
                {
                    AudioManager.self.PlaySound("Clean");
                    Narrate = Drink.DrinkData[Select].Name + "被買光了";
                    Short = Drink.DrinkData[Select].Name + "賣光了";
                    Player.setDrinkinStock(Select, 0);
                    Player.Money += Drink.DrinkData[Select].Price * buy;
                    Player.DrinkSell += buy;
                }
                else
                {
                    Narrate = "有人想買缺貨的" + Drink.DrinkData[Select].Name ;
                    Short = Drink.DrinkData[Select].Name + "沒貨了";
                }
                
                
            }
            else
            {
                AudioManager.self.PlaySound("Clean");
                Narrate = "全部飲料被買光了";
                Short = "全部賣光光";
                for (int i = 0; i < Drink.DrinkData.Count; i++)
                {
                    if (Player.getHavetheDrink(i))
                    {
                        int buy = Player.getDrinkinStock(i);
                        Player.setDrinkinStock(i,0);
                        Player.Money += Drink.DrinkData[i].Price * buy;
                        Player.DrinkSell += buy;
                    }
                        
                }
            }*/
            int Select;
            List<int> CanSell = new List<int>();
            for (int i = 0; i < Player.countHavetheDrink(); i++)
            {
                if (Player.getHavetheDrink(i))
                    CanSell.Add(i);
            }
            int a = UnityEngine.Random.Range(0, CanSell.Count);
            Select = CanSell[a];
            //if ((int)Random.Range(0, 100) > 0)
            //{

                int lose = Player.getDrinkinStock(Select);
                if (lose > 3)
                {
                    AudioManager.self.PlaySound("Clean");
                // Narrate = Drink.DrinkData[Select].Name + "被打翻"+ (int)(lose / 3)+"杯";
                // Short = "飲料被打翻了";
                    Narrate = "店員偷喝" + (int)(lose / 3) + "杯"+ Drink.DrinkData[Select].Name;
                    Short = "店員偷喝";
                Player.setDrinkinStock(Select,Player.getDrinkinStock(Select)-(int)(lose / 3));
                    //Player.Money += Drink.DrinkData[Select].Price * buy;
                    //Player.DrinkSell += buy;
                }
                else
                {
                // Narrate = "店員差點打翻飲料" ;
                // Short = "虛驚一場";
                    Narrate = "店員口渴了";
                    Short = "店員口渴";
            }


            //}
            /* else
             {
                 AudioManager.self.PlaySound("Clean");
                 Narrate = "全部飲料被買光了";
                 Short = "全部賣光光";
                 for (int i = 0; i < Drink.DrinkData.Count; i++)
                 {
                     if (Player.getHavetheDrink(i))
                     {
                         int buy = Player.getDrinkinStock(i);
                         Player.setDrinkinStock(i, 0);
                         Player.Money += Drink.DrinkData[i].Price * buy;
                         Player.DrinkSell += buy;
                     }

                 }
             }*/
        }
        if (j == 4)
        {
            if (Player.Money >= 2000)
            {
                AudioManager.self.PlaySound("Stole");
                int stole = ((int)Random.Range(1, 3)) * 100;
                Player.Money -= stole;
                Narrate = "被偷了" + stole + "元";
                Short = "遭小偷";
                SellingAnime.self.Info.transform.GetChild(6).GetComponent<Text>().text = "-" + stole;
                SellingAnime.self.Info.transform.GetChild(7).GetComponent<Text>().text = "";
                SellingAnime.self.SellInfo();
            }
            else
            {
                Narrate = "小偷不偷窮人";
                Short = "沒被偷";
            }
        }
        if(j == 5)
        {
            int drinking = 0;
            int drinkinstocksum = 0;
            for (int i=0;i< Player.DrinkSum;i++)
            {
                drinkinstocksum += Player.getDrinkinStock(Player.getCanMake(i));
            }
            if (drinkinstocksum > 0)
            {
                AudioManager.self.PlaySound("Clean");
                for (int i = 0; i < Player.DrinkSum; i++)
                {
                    int onedrink = (int)Random.Range(0,3);
                    onedrink = Mathf.Clamp(onedrink,0, Player.getDrinkinStock(Player.getCanMake(i)));
                    Player.setDrinkinStock(i,Player.getDrinkinStock(Player.getCanMake(i)) - onedrink);
                    drinking += onedrink;
                }
                if (drinking > 0)
                {

                    Narrate = "店員打翻" + drinking + "杯飲料";
                    Short = "飲料打翻了";
                }
                else
                {
                    Narrate = "店員差點打翻飲料";
                    Short = "虛驚一場";
                    //Narrate = "店員口渴了";
                    // Short = "店員口渴";
                }
            }
            else
            {
                Narrate = "店裡沒有飲料庫存" ;
                Short = "該補貨了";
            }
           
        }
        Debug.Log(Narrate);
    }
    public bool LevelUp(PlayerData Player)
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
            UIManager.self.levelupWindow.transform.GetChild(0).GetComponent<Text>().text = "升級了\n"+MotoLevel+" > "+ Player.Level;
            return true;
        }
        else
        {
            return false;
        }
    }
}
