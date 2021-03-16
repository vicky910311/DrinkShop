using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager self;
    public GameObject StaffWindow;
    public GameObject DrinkWindow, DevelopWindow, MenuWindow, MakeWindow;
    public GameObject ClientWindow, NormalWindow, SpecialWindow;
    public GameObject IncidentWindow, MissionWindow, EventWindow;
    public GameObject SettingWindow;
    public GameObject PurchaseWindow;
    public GameObject stocklimitWindow, developlimitWindow, developcostWindow, lookadWindow, objectWindow, noticeWindow;
    public GameObject staffcostWindow;
    public GameObject levelupWindow, storyWindow, quitWindow;
    public GameObject manualBtn, adBtn, developfastBtn, SEBtn, BGMBtn;
    public GameObject drinkNotify,dNotify, clientNotify, staffNotify;
    public GameObject EventBtn;
    public Sprite SBlueBtn, SPinkBtn, SWhiteBtn, SYellowBtn,TabA,TabAon,TabB,TabBon,DarkDBtn,BrightDBtn,RRedBtn,RYellowBtn;
    public GameObject StaffBtn, ClientBtn, DrinkBtn, ShopBtn;
    public GameObject DevelopBtn, MakeBtn, MenuBtn;
    public GameObject SpecialBtn, NormalBtn;
    public GameObject MissionBtn, IncidentBtn;
    private Color32 OriColor = new Color32(230,200,200,255);
    private Color32 DarkColor = new Color32(80,60,60,255);
    public GameObject rewardWindow;
    public GameObject BuyInfoWindow;
    public GameObject MakeAllWindow;

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

    }


    void Update()
    {

    }
    public void SHUT()
    {
        shutdownAll();
        shutdownLittle();
    }
    public bool checkBigActive()
    {
        if (StaffWindow.activeSelf)
            return true;
        else if (DrinkWindow.activeSelf)
            return true;
        else if (ClientWindow.activeSelf)
            return true;
        else if (EventWindow.activeSelf)
            return true;
        else if (PurchaseWindow.activeSelf)
            return true;
        else if (MakeAllWindow.activeSelf)
            return true;
        else
            return false;
    }
    public bool checkLittleActive()
    {
        if (noticeWindow.activeSelf)
            return true;
        else if (lookadWindow.activeSelf)
            return true;
        else if (levelupWindow.activeSelf)
            return true;
        else if (quitWindow.activeSelf)
            return true;
        else if (objectWindow.activeSelf)
            return true;
        else if (stocklimitWindow.activeSelf)
            return true;
        else if (developlimitWindow.activeSelf)
            return true;
        else if (developcostWindow.activeSelf)
            return true;
        else if (staffcostWindow.activeSelf)
            return true;
        else if (rewardWindow.activeSelf)
            return true;
        else if (BuyInfoWindow.activeSelf)
            return true;
        else
            return false;
    }

    public void EventNotify()
    {
        EventBtn.transform.DOLocalMoveX(-350, 0.5f);//.OnComplete(() => { EventBtn.transform.DOLocalMoveX(-350, 0.5f); });
    }
    public void shutdownLittle()
    {
        stocklimitWindow.SetActive(false);
        developlimitWindow.SetActive(false);
        developcostWindow.SetActive(false);
        lookadWindow.SetActive(false);
        objectWindow.SetActive(false);
        staffcostWindow.SetActive(false);
        //quitWindow.SetActive(false);
        //noticeWindow.SetActive(false);
        //levelupWindow.SetActive(false);
        storyWindow.SetActive(false);
        if (storyWindow.transform.childCount > 1)
            Destroy(storyWindow.transform.GetChild(0).gameObject);
        AudioManager.self.PlaySound("Click");

    }
    public void shutdownStory()
    {
        storyWindow.SetActive(false);
        if (storyWindow.transform.childCount > 1)
            Destroy(storyWindow.transform.GetChild(0).gameObject);
        AudioManager.self.PlaySound("Click");
    }
    public void shutdownLevelup()
    {
        levelupWindow.SetActive(false);
        AudioManager.self.PlaySound("Click");
    }
    public void shutdownBuyInfo()
    {
        BuyInfoWindow.SetActive(false);
        AudioManager.self.PlaySound("Click");
    }
    public void shutdownReward()
    {
        rewardWindow.SetActive(false);
        AudioManager.self.PlaySound("Click");
    }
    public void shutdownNotice()
    {
        noticeWindow.SetActive(false);
        AudioManager.self.PlaySound("Click");
    }
    public void shutdownQuit()
    {
        quitWindow.SetActive(false);
        AudioManager.self.PlaySound("Click");
    }
    public void OpenQuit()
    {
        quitWindow.SetActive(true);
        AudioManager.self.PlaySound("Click");
    }
    public void OpenLevelup()
    {
        levelupWindow.SetActive(true);
        
    }
    public void OpenBuyInfo()
    {
        BuyInfoWindow.SetActive(true);
    }
    public void OpenReward()
    {
        rewardWindow.SetActive(true);
    }
    public void OpenStory()
    {
        shutdownLittle();
        storyWindow.SetActive(true);
        GameObject STORY = Instantiate(Resources.Load("Prefabs/story"), storyWindow.transform) as GameObject ;
        STORY.transform.SetSiblingIndex(0);
    }
    public void OpenNotice()
    {
        shutdownLittle();
        noticeWindow.SetActive(true);
        noticeWindow.transform.GetChild(0).GetComponent<Text>().text = GameManager.self.LeaveNarrate;
    }
    public void OpenObject()
    {
        shutdownLittle();
        objectWindow.SetActive(true);
    }
    public void OpenLookAD()
    {
        shutdownLittle();
        lookadWindow.SetActive(true);
    }
    public void OpenDevelopCost()
    {
        shutdownLittle();
        developcostWindow.SetActive(true);
    }
    public void OpenDevelopLimit()
    {
        shutdownLittle();
        developlimitWindow.SetActive(true);
    }
    public void OpenStaffCost()
    {
        shutdownLittle();
        staffcostWindow.SetActive(true);
    }
    public void OpenStockLimit()
    {
        shutdownLittle();
        stocklimitWindow.SetActive(true);
    }
    public void shutdownAll()
    {
        MakeWindow.SetActive(false);
        DevelopWindow.SetActive(false);
        MenuWindow.SetActive(false);
        DrinkWindow.SetActive(false);
        NormalWindow.SetActive(false);
        SpecialWindow.SetActive(false);
        ClientWindow.SetActive(false);
        StaffWindow.SetActive(false);
        IncidentWindow.SetActive(false);
        MissionWindow.SetActive(false);
        EventWindow.SetActive(false);
        PurchaseWindow.SetActive(false);
        SettingWindow.SetActive(false);
        MakeAllWindow.SetActive(false);
        StaffBtn.GetComponent<Image>().sprite = TabA;
        StaffBtn.GetComponentInChildren<Text>().color = OriColor;
        ClientBtn.GetComponent<Image>().sprite = TabA;
        ClientBtn.GetComponentInChildren<Text>().color = OriColor;
        DrinkBtn.GetComponent<Image>().sprite = TabA;
        DrinkBtn.GetComponentInChildren<Text>().color = OriColor;
        ShopBtn.GetComponent<Image>().sprite = TabA;
        ShopBtn.GetComponentInChildren<Text>().color = OriColor;
    }
    public void OpenMakeAllWindow()
    {
        shutdownLittle();
        MakeAllWindow.SetActive(true);
    }
    public void shutdownMakeAllWindow()
    {
        MakeAllWindow.SetActive(false);
    }
    public void OpenSettingWindow()
    {
        if (SettingWindow.activeSelf == true)
        {
            shutdownLittle();
            SettingWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            SettingWindow.SetActive(true);
        }
    }
    public void OpenPurchaseWindow()
    {
        if (PurchaseWindow.activeSelf == true)
        {
            shutdownLittle();
            PurchaseWindow.SetActive(false);
            ShopBtn.GetComponent<Image>().sprite = TabA;
            ShopBtn.GetComponentInChildren<Text>().color = OriColor;
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            PurchaseWindow.SetActive(true);
            ShopBtn.GetComponent<Image>().sprite = TabAon;
            ShopBtn.GetComponentInChildren<Text>().color = DarkColor;
        }

    }
    public void OpenStaffWindow()
    {
        if (StaffWindow.activeSelf == true)
        {
            shutdownLittle();
            StaffWindow.SetActive(false);
            StaffBtn.GetComponent<Image>().sprite = TabA;
            StaffBtn.GetComponentInChildren<Text>().color = OriColor;

        }
        else
        {
            staffNotify.SetActive(false);
            shutdownLittle();
            shutdownAll();
            StaffWindow.SetActive(true);
            StaffBtn.GetComponent<Image>().sprite = TabAon;
            StaffBtn.GetComponentInChildren<Text>().color = DarkColor;
        }

    }
    public void OpenClientWindow()
    {
        if (ClientWindow.activeSelf == true)
        {
            shutdownLittle();
            ClientWindow.SetActive(false);
            ClientBtn.GetComponent<Image>().sprite = TabA;
            ClientBtn.GetComponentInChildren<Text>().color = OriColor;
            SpecialBtn.GetComponent<Image>().sprite = TabB;
            NormalBtn.GetComponent<Image>().sprite = TabB;
        }
        else
        {
            clientNotify.SetActive(false);
            shutdownLittle();
            shutdownAll();
            ClientWindow.SetActive(true);
            NormalWindow.SetActive(true);
            ClientBtn.GetComponent<Image>().sprite = TabAon;
            ClientBtn.GetComponentInChildren<Text>().color = DarkColor;
            SpecialBtn.GetComponent<Image>().sprite = TabB;
            NormalBtn.GetComponent<Image>().sprite = TabBon;
        }

    }
    public void OpenNormalWindow()
    {
        if (NormalWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            ClientWindow.SetActive(true);
            NormalWindow.SetActive(true);
            SpecialBtn.GetComponent<Image>().sprite = TabB;
            NormalBtn.GetComponent<Image>().sprite = TabBon;
        }
    }
    public void OpenSpecialWindow()
    {
        if (SpecialWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            ClientWindow.SetActive(true);
            SpecialWindow.SetActive(true);
            SpecialBtn.GetComponent<Image>().sprite = TabBon;
            NormalBtn.GetComponent<Image>().sprite = TabB;
        }
    }
    public void OpenDrinkWindow()
    {
        if (DrinkWindow.activeSelf == true)
        {
            shutdownLittle();
            DrinkWindow.SetActive(false);
            DrinkBtn.GetComponent<Image>().sprite = TabA;
            DrinkBtn.GetComponentInChildren<Text>().color = OriColor;
            MakeBtn.GetComponent<Image>().sprite = TabB;
            DevelopBtn.GetComponent<Image>().sprite = TabB;
            MenuBtn.GetComponent<Image>().sprite = TabB;
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            MenuWindow.SetActive(true);
            DrinkBtn.GetComponent<Image>().sprite = TabAon;
            DrinkBtn.GetComponentInChildren<Text>().color = DarkColor;
            MakeBtn.GetComponent<Image>().sprite = TabB;
            DevelopBtn.GetComponent<Image>().sprite = TabB;
            MenuBtn.GetComponent<Image>().sprite = TabBon;
        }

    }
    public void OpenDevelopWindow()
    {
        if (DevelopWindow.activeSelf != true)
        {
            dNotify.SetActive(false);
            drinkNotify.SetActive(false);
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            DevelopWindow.SetActive(true);
            MakeBtn.GetComponent<Image>().sprite = TabB;
            DevelopBtn.GetComponent<Image>().sprite = TabBon;
            MenuBtn.GetComponent<Image>().sprite = TabB;
        }
    }
    public void OpenMenuWindow()
    {
        if (MenuWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            MenuWindow.SetActive(true);
            MakeBtn.GetComponent<Image>().sprite = TabB;
            DevelopBtn.GetComponent<Image>().sprite = TabB;
            MenuBtn.GetComponent<Image>().sprite = TabBon;
        }
    }
    public void OpenMakeWindow()
    {
        if (MakeWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            MakeWindow.SetActive(true);
            MakeBtn.GetComponent<Image>().sprite = TabBon;
            DevelopBtn.GetComponent<Image>().sprite = TabB;
            MenuBtn.GetComponent<Image>().sprite = TabB;
        }
    }
    public void OpenIncidentWindow()
    {
        if (IncidentWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            EventWindow.SetActive(true);
            IncidentWindow.SetActive(true);
            IncidentBtn.GetComponent<Image>().sprite = TabBon;
            MissionBtn.GetComponent<Image>().sprite = TabB;
        }

    }
    public void OpenMissionWindow()
    {
        if (MissionWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            EventWindow.SetActive(true);
            MissionWindow.SetActive(true);
            IncidentBtn.GetComponent<Image>().sprite = TabB;
            MissionBtn.GetComponent<Image>().sprite = TabBon;
        }

    }
    public void OpenEventWindow()
    {
        if (EventWindow.activeSelf == true)
        {
            EventWindow.SetActive(false);
            IncidentBtn.GetComponent<Image>().sprite = TabB;
            MissionBtn.GetComponent<Image>().sprite = TabB;
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            EventWindow.SetActive(true);
            IncidentWindow.SetActive(true);
            IncidentBtn.GetComponent<Image>().sprite = TabBon;
            MissionBtn.GetComponent<Image>().sprite = TabB;
        }
        if (EventBtn.transform.localPosition.x > -550)
        {
            EventBtn.transform.DOLocalMoveX(-550, 0.5f);
        }
    }
}
