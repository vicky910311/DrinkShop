using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffUse : MonoBehaviour
{
    private MyUIHoverListener uiListener;
    // Start is called before the first frame update
    void Start()
    {
        uiListener = MyUIHoverListener.self;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp()
    {
        
        if (uiListener.isUIOverride)
        {
            Debug.Log("Cancelled OnMouseDown! A UI element has override this object!");
        }
        else if(SellingAnime.self.selling == false)
        {
            AudioManager.self.PlaySound("Drop");
            if (SellingAnime.self.sleeping == true)
            {
                PlayerDataManager.self.Player.CatchSleep++;
            }
            SellingAnime.self.StaffWakeup();
            
        }


    }
}
