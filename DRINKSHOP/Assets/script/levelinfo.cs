using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelinfo : MonoBehaviour
{
    public GameObject infowindow;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.self.checkBigActive())
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnMouseDown()
    {
        level = PlayerDataManager.self.Player.Level;
        if (level < 5)
        {
            infowindow.transform.GetComponentInChildren<Text>().text = "下一星：解鎖" + GameDataManager.self.Level.LevelUpData[level].DrinkSum + "款飲料，賣出量" + GameDataManager.self.Level.LevelUpData[level].SellSum + "杯";
            infowindow.SetActive(true);
        }
        
        Debug.Log("downstar");
    }
    private void OnMouseUp()
    {
        infowindow.SetActive(false);
    }
}
