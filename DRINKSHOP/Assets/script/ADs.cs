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
    int I;

    private void Awake()
    {
        self = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3939969", false);
      //while (!Advertisement.IsReady(play))
       //   yield return null;
        
    }

    public void PlayAD(string p,string r,int i)
    {
        Reward = r;
        I = i;
        if (Advertisement.IsReady(p))
            Advertisement.Show(p);
        //    Advertisement.Show(p);
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
                GameManager.self.developFast();
            }
        }
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
