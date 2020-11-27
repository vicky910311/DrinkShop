using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]

public class Audiosave 
{
    [SerializeField]
    private bool seswitch;
    public bool SEswitch
    {
        set
        {
            seswitch = value;
            if (OnSEChange != null)
            {
                OnSEChange();
            }
        }
        get { return seswitch; }
    }
    public Action OnSEChange;
    [SerializeField]
    private bool bgmswitch;
    public bool BGMswitch
    {
        set
        {
            bgmswitch = value;
            if (OnBGMChange != null)
            {
                OnBGMChange();
            }
        }
        get { return bgmswitch; }
    }
    public Action OnBGMChange;
}
