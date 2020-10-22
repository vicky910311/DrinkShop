using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [SerializeField]
    private int money;
    public int Money {
        set {
            money = value;
            if (OnMoneyChanged != null)
            {
                OnMoneyChanged();
            }
        }
        get { return money; }
    }
    public Action OnMoneyChanged;

    public Data()
    {
        money = 10000;
    }


}

