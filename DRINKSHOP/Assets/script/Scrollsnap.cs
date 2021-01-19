using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scrollsnap : MonoBehaviour
{
    bool Move = false;
    public GameObject Xbtn;
    RectTransform rt;
    bool arrowMove = false;
    bool TM = false;
    Tweener T;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        Xbtn.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

       
        
        if (Input.GetMouseButtonDown(0))
        {
            if (arrowMove == false)
            {
                Move = true;
            }
            else
            {
                Move = false;
            }
            Debug.Log("move "+ Move);
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            Move = false;
            Debug.Log("move " + Move);

        }
        if (Move == false && arrowMove == false && !TM)
        {

            if (rt.localPosition.x > 1080)
            {
                if (rt.localPosition.x != 1620)
                {
                    
                    transform.DOLocalMoveX(1620, 0.5f);
                    TM = true;
                    Invoke("finish",0.5f);
                }

            }
            if (rt.localPosition.x <= 1080 && rt.localPosition.x > 0)
            {
                if (rt.localPosition.x != 540)
                {
                    transform.DOLocalMoveX(540, 0.5f);
                    TM = true;
                    Invoke("finish", 0.5f);
                }
                    
            }
            if (rt.localPosition.x <= 0 && rt.localPosition.x > -1080)
            {
                if (rt.localPosition.x != -540)
                {
                    transform.DOLocalMoveX(-540, 0.5f);
                    TM = true;
                    Invoke("finish", 0.5f);
                }
                   

            }
            if (rt.localPosition.x <= -1080)
            {
                if (rt.localPosition.x != -1620)
                {
                    transform.DOLocalMoveX(-1620, 0.5f);
                    TM = true;
                    Invoke("finish", 0.5f);
                }
                
            }

        }
        if(Mathf.Abs(rt.localPosition.x -540)%1080 <= 120 || Mathf.Abs(rt.localPosition.x - 540) % 1080 >= 960)
        {
            Xbtn.SetActive(true);
        }
        else
        {
            Xbtn.SetActive(false);
        }
        
    }
    void finish()
    {
        TM = false;
    }
    public void RClick()
    {
        Debug.Log("RClick");
        
        
        if (rt.localPosition.x >= -580)
        {
            arrowMove = true;
            float X = rt.localPosition.x - 1080;
            transform.DOLocalMoveX(X, 0.5f);
            //rt.localPosition = new Vector3(X, rt.localPosition.y, rt.localPosition.z);
            Invoke("AMF", 0.5f);
        }
    }
    public void LClick()
    {
        Debug.Log("LClick");
        
        if (rt.localPosition.x <= 580)
        {
            arrowMove = true;
            float X = rt.localPosition.x + 1080;
            transform.DOLocalMoveX(X, 0.5f);
            //rt.localPosition = new Vector3(X, rt.localPosition.y, rt.localPosition.z);
            Invoke("AMF",0.5f);
        }
    }
    void AMF()
    {
        arrowMove = false;
    }
}
