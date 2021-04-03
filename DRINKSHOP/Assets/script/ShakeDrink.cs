using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;


public class ShakeDrink : MonoBehaviour
{
    bool Down = false;
    int count = 0;
    
    public int Count
    {
        set
        {
            count = value;
            if (OnCountChange != null)
            {
                OnCountChange();
            }
        }
        get
        { return count; }
    }
    public Action OnCountChange;
    bool up, uptemp;
    public bool shake;
    public bool onit = false;
    int countlast = -1;
    public Text  shakecount;
    public Sprite shaking;
    public Sprite open;

    // Start is called before the first frame update
    void Start()
    {
        OnCountChange += counting;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y >= 200 )
        {
            uptemp = true;
            if (up != uptemp)
            {
                shake = true;
                Count++;
                //AudioManager.self.PlaySound("shake");
            }
            else
            {
                shake = false;
            }
            up = true;

        }
        if (transform.localPosition.y <= 200)
        {
            uptemp = false;
            if (up != uptemp)
            {
                shake = true;
                Count++;
                AudioManager.self.PlaySound("shake");
            }
            else
            {
                shake = false;
            }
            up = false;
        }
        if (Down)
        {
            if (onit)
            {
                transform.localPosition = 1.2f * (Input.mousePosition + new Vector3(-Screen.width/2f, -Screen.height/2f, 0));
            }
           
            if (Input.GetMouseButtonUp(0))
            {
                Down = false;
                transform.DOMove(new Vector3(0, 0, 0), 0.5f);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Down = true;
            }
            
        }
    }
    public void countstart()
    {
        GetComponent<Image>().sprite = shaking;
        UIManager.self.OpenMakeAllWindow();
        Count = 0;
        GameManager.self.DrinkControl.makeAllcount(PlayerDataManager.self.Player,ref countlast);
        Debug.Log("countlast" + countlast);
        if(countlast > 0)
        {
            shakecount.text = "搖" + (countlast/2) + "下補滿";
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }
       
    }
    public void counting()
    {
        countlast --;
        shakecount.text = "搖" + (countlast/2) + "下補滿";
        if (countlast == 0)
        {
            shakecount.text = "已補滿";
            AudioManager.self.PlaySound("Developdone");
            onit = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.DOMove(new Vector3(0, 0, 0), 0.5f);
            GameManager.self.DrinkControl.makeAllcost(PlayerDataManager.self.Player);
            GetComponent<Image>().sprite = open;
        }
        if (countlast <= 0)
        {
            
            shakecount.text = "已補滿";
            onit = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.DOMove(new Vector3(0, 0, 0), 0.5f);
        }
       

    }
    public void close()
    {
        if (onit == false)
        {
            UIManager.self.shutdownMakeAllWindow();
        }
        
    }

    private void OnMouseDown()
    {
        if (UIManager.self.checkLittleActive() == false)
            onit = true;
        else
            onit = false;
      //  transform.localPosition = Input.mousePosition;
    }
    private void OnMouseUp()
    {
        onit = false;
      //  transform.DOMove(new Vector3(0, 0, 0), 1f);
    }

}
