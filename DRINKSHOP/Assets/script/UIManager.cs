using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager self;
    public GameObject DrinkWindow,DevelopWindow,MenuWindow,MakeWindow;
    public GameObject ClientWindow, NormalWindow, SpecialWindow;
    public GameObject IncidentWindow;
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
    public void shutdownAll()
    {
        MakeWindow.SetActive(false);
        DevelopWindow.SetActive(false);
        MenuWindow.SetActive(false);
        DrinkWindow.SetActive(false);
        NormalWindow.SetActive(false);
        SpecialWindow.SetActive(false);
        ClientWindow.SetActive(false);

        IncidentWindow.SetActive(false);
    }
    public void OpenClientWindow()
    {
        if (ClientWindow.activeSelf == true)
        {
            ClientWindow.SetActive(false);
        }
        else
        {
            shutdownAll();
            ClientWindow.SetActive(true);
            NormalWindow.SetActive(true);
        }

    }
    public void OpenNormalWindow()
    {
        if (NormalWindow.activeSelf != true)
        {
            shutdownAll();
            ClientWindow.SetActive(true);
            NormalWindow.SetActive(true);
        }
    }
    public void OpenSpecialWindow()
    {
        if (SpecialWindow.activeSelf != true)
        {
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
            shutdownAll();
            DrinkWindow.SetActive(true);
            MenuWindow.SetActive(true);
        }

    }
    public void OpenDevelopWindow()
    {
        if (DevelopWindow.activeSelf != true)
        {
            shutdownAll();
            DrinkWindow.SetActive(true);
            DevelopWindow.SetActive(true);
        }
    }
    public void OpenMenuWindow()
    {
        if (MenuWindow.activeSelf != true)
        {
            shutdownAll();
            DrinkWindow.SetActive(true);
            MenuWindow.SetActive(true);
        }
    }
    public void OpenMakeWindow()
    {
        if (MakeWindow.activeSelf != true)
        {
            shutdownAll();
            DrinkWindow.SetActive(true);
            MakeWindow.SetActive(true);
        }
    }
    public void OpenIncidentWindow()
    {
        if (IncidentWindow.activeSelf == true)
        {
            IncidentWindow.SetActive(false);
        }
        else
        {
            shutdownAll();
            IncidentWindow.SetActive(true);
        }

    }
}
