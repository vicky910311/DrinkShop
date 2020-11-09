using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager self;
    public GameObject StaffWindow;
    public GameObject DrinkWindow,DevelopWindow,MenuWindow,MakeWindow;
    public GameObject ClientWindow, NormalWindow, SpecialWindow;
    public GameObject IncidentWindow, MissionWindow, EventWindow;
    public GameObject SettingWindow;
    public GameObject PurchaseWindow;
    public GameObject stocklimitWindow, developlimitWindow, developcostWindow,lookadWindow, objectWindow, noticeWindow;
    public GameObject levelupWindow, storyWindow, quitWindow;
    public GameObject manualBtn, adBtn;
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
    public void shutdownLittle()
    {
        stocklimitWindow.SetActive(false);
        developlimitWindow.SetActive(false);
        developcostWindow.SetActive(false);
        lookadWindow.SetActive(false);
        objectWindow.SetActive(false);
        quitWindow.SetActive(false);
        //noticeWindow.SetActive(false);
        //levelupWindow.SetActive(false);
        if(storyWindow.transform.childCount > 0)
            Destroy(storyWindow.transform.GetChild(0).gameObject);
        
    }
    public void shutdownLevelup()
    {
        levelupWindow.SetActive(false);
    }
    public void shutdownNotice()
    {
        noticeWindow.SetActive(false);
    }
    public void OpenQuit()
    {
        quitWindow.SetActive(true);
    }
    public void OpenLevelup()
    {
        levelupWindow.SetActive(true);
        
    }
    public void OpenStory()
    {
        shutdownLittle();
        storyWindow.SetActive(true);
        GameObject STORY = Instantiate(Resources.Load("Prefabs/story"), storyWindow.transform) as GameObject ;
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
    }
    public void OpenSettingWindow()
    {
        if (SettingWindow.activeSelf == true)
        {
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
            PurchaseWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            PurchaseWindow.SetActive(true);
        }

    }
    public void OpenStaffWindow()
    {
        if (StaffWindow.activeSelf == true)
        {
            shutdownLittle();
            StaffWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            StaffWindow.SetActive(true);
        }

    }
    public void OpenClientWindow()
    {
        if (ClientWindow.activeSelf == true)
        {
            ClientWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            ClientWindow.SetActive(true);
            NormalWindow.SetActive(true);
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
        }
    }
    public void OpenDrinkWindow()
    {
        if (DrinkWindow.activeSelf == true)
        {
            DrinkWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            MenuWindow.SetActive(true);
        }

    }
    public void OpenDevelopWindow()
    {
        if (DevelopWindow.activeSelf != true)
        {
            shutdownLittle();
            shutdownAll();
            DrinkWindow.SetActive(true);
            DevelopWindow.SetActive(true);
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
        }

    }
    public void OpenEventWindow()
    {
        if (EventWindow.activeSelf == true)
        {
            EventWindow.SetActive(false);
        }
        else
        {
            shutdownLittle();
            shutdownAll();
            EventWindow.SetActive(true);
            IncidentWindow.SetActive(true);
        }

    }
}
