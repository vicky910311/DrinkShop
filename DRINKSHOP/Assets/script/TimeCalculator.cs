using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCalculator : MonoBehaviour
{
    private DateTime Time1, Time2;
    private TimeSpan Differ;
    public GameObject time1, time2, differ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Time1click()
    {
        Time1 = DateTime.Now;
        time1.GetComponent<Text>().text = Time1.ToString();
    }

    public void Time2click()
    {
        Time2 = DateTime.Now;
        time2.GetComponent<Text>().text = Time2.ToString();
    }

    public void Differclick()
    {
        Differ = Time2 - Time1;
        differ.GetComponent<Text>().text = ((int)Differ.TotalSeconds).ToString();
    }
}
