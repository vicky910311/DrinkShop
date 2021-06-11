using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;



public class ADs : MonoBehaviour, IUnityAdsListener
{
    public static ADs self;
    string P = "rewardedVideo";
    string Reward;
    int I,J;
    bool D = true;
    public bool ADback = false;
    private void Awake()
    {
        self = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
 
#if UNITY_ANDROID
        Advertisement.Initialize("4092601", false);
#elif UNITY_IOS
        Advertisement.Initialize("4092600", false);
#endif
        //while (!Advertisement.IsReady(play))
        //   yield return null;
        ADback = false;
    }

    public void PlayAD(string p,string r,int i)
    {
        Reward = r;
        I = i;
#if UNITY_ANDROID
        P = "Rewarded_Android";
#elif UNITY_IOS
        P= "Rewarded_iOS";
#endif
        D = false;
        J = 0;
        if (Advertisement.IsReady(p))
        {
            D = true;
            Advertisement.Show(p);
            
        }
        else
        {
            UIManager.self.lookadWindow.transform.GetChild(1).GetComponent<Button>().interactable = false;
            for (int j= 1;j<=10;j++)
            {
                Invoke("play",j);
            }
            
        }
        //    Advertisement.Show(p);
    }
    public void play()
    {
        if(UIManager.self.lookadWindow.activeSelf == true)
        {
            if (D == false)
            {
                J++;
            }

            if (Advertisement.IsReady(P) && D == false)
            {
                D = true;
                Advertisement.Show(P);
                UIManager.self.lookadWindow.transform.GetChild(1).GetComponent<Button>().interactable = true;
            }
            else if (J >= 10)
            {
                D = true;
                UIManager.self.lookadWindow.transform.GetChild(1).GetComponent<Button>().interactable = true;
            }

            if (D == true)
            {
                J = 0;
            }
        }
        else
        {
            D = true;
            UIManager.self.lookadWindow.transform.GetChild(1).GetComponent<Button>().interactable = true;
            J = 0;
        }
        
    }
    public void OnUnityAdsDidError(string message)
    {
    //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            if (Reward == "adpromote")
            {
                UIManager.self.shutdownLittle();
               
                GameManager.self.adpromote();
            }
            else if (Reward == "unlockstaffFast(i)")
            {
                UIManager.self.shutdownLittle();
                //UIManager.self.OpenStaffWindow();
                GameManager.self.unlockstaffFast(I);
            }
            else if (Reward == "Recapture")
            {
                UIManager.self.shutdownNotice();
                UIManager.self.shutdownLittle();
                PlayerDataManager.self.Player.Money += I;
            }
            else if(Reward == "developFast")
            {
                UIManager.self.shutdownLittle();
                //UIManager.self.OpenDrinkWindow();
                //UIManager.self.OpenDevelopWindow();
                GameManager.self.developFast();
            }
        }
        ADback = true;
        //   throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
     // throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
    //  throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
