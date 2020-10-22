using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataUI : MonoBehaviour
{
    public Text text;
    public Data d;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        d.OnMoneyChanged += UpdateText;
    }

    public void UpdateText()
    {
        text.text = d.Money.ToString();
    }
  
}
