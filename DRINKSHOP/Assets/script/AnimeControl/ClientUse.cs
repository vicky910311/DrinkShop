using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void haveCome()
    {
        if (SellingAnime.self.havestock)
        {
            SellingAnime.self.SellBegin();
        }
        else
        {
            SellingAnime.self.Go();
        }
       
    } 
    public void Speaknostock()
    {
        if (!SellingAnime.self.havestock)
        {
            SellingAnime.self.Speak.SetActive(true);
        }
    }
    public void SpeakDone()
    {
        SellingAnime.self.Speak.SetActive(false);
    }
}
