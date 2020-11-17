using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingAnime : MonoBehaviour
{
    public static SellingAnime self;
    public Animator DrinkAni, ClientAni, ArmAni;
    public bool havestock;
    public int D, C;
    private void Awake()
    {
        self = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeStaff()
    {
        
    }
    public void Staffgosleep()
    {

    }
    public void StaffWakeup()
    {

    }
    public void ChangeCandD()
    {

    }
    public void Come()
    {
        ClientAni.SetTrigger("come");
    }
    public void SellBegin()
    {
        DrinkAni.SetTrigger("appear");
        ArmAni.SetTrigger("appear");
    }
    public void SellDone()
    {
        DrinkAni.SetTrigger("disappear");
        ArmAni.SetTrigger("disappear");
    }
    public void Go()
    {
        ClientAni.SetTrigger("go");
    }
}
