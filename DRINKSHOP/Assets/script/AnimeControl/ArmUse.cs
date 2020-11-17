using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmUse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sellDone()
    {
        SellingAnime.self.SellDone();
    }
    public void ClientGO()
    {
        SellingAnime.self.Go();
    }
}
