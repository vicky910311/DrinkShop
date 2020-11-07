using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scrollsnap : MonoBehaviour
{
    bool Move = false;
    bool tweening = false;
    
    Tweener tweener;
    
    // Start is called before the first frame update
    void Start()
    {
       // tweener.OnPlay(TStart);
       // tweener.OnComplete(TKill);

    }

    // Update is called once per frame
    void Update()
    {

        RectTransform rt = GetComponent<RectTransform>();
        /* int a = (int)rt.position.x;

          if (a%1080!=0)
          {
              Invoke("movetrue",0.5f);
              a = (int)((int)(a / 540) / 2) * 1080;
          }
          if (Input.GetMouseButtonUp(0)&&  Move == true)
          {
              Move = false;
              transform.DOMoveX(1080, 0.5f);
          }*/
        if (Input.GetMouseButtonDown(0))
        {
            Move = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Move = false;
        }
        /*if (Move == false )
        {
           
            if (rt.localPosition.x >1080)
            {
               if(rt.localPosition.x != 1620 && tweening == false)
                {
                    tweener = transform.DOLocalMoveX(1620, 0.2f).OnPlay(TStart).OnComplete(TKill);//.OnComplete(() => { tweener.Kill(); };
                    Debug.Log("是否正在tween" + tweening);
                }
                    
            }
            else if (rt.localPosition.x <= 1080 && rt.localPosition.x > 0)
            {
                if (rt.localPosition.x != 540 && tweening == false)
                    tweener = transform.DOLocalMoveX(540, 0.2f);//.OnComplete(() => { tweener.Kill(); }) ;
            }
            else if(rt.localPosition.x <= 0 && rt.localPosition.x > -1080)
            {
                if (rt.localPosition.x != -540 && tweening == false)
                    tweener = transform.DOLocalMoveX(-540, 0.2f);//.OnComplete(() => { tweener.Kill(); });
               
            }
            else if(rt.localPosition.x <= -1080)
            {
                if (rt.localPosition.x != -1620 && tweening == false)
                    tweener = transform.DOLocalMoveX(-1620, 0.2f);//.OnComplete(() => { tweener.Kill(); });
            }
            
        }*/
        /* if (Move == false)
         {

             if (rt.localPosition.x > 1080)
             {
                 if (rt.localPosition.x != 1620)
                 {
                     rt.localPosition =new Vector3(1620, rt.localPosition.y, rt.localPosition.z);
                 }

             }
             else if (rt.localPosition.x <= 1080 && rt.localPosition.x > 0)
             {
                 if (rt.localPosition.x != 540)
                     rt.localPosition = new Vector3(540, rt.localPosition.y, rt.localPosition.z);
             }
             else if (rt.localPosition.x <= 0 && rt.localPosition.x > -1080)
             {
                 if (rt.localPosition.x != -540)
                     rt.localPosition = new Vector3(-540, rt.localPosition.y, rt.localPosition.z);

             }
             else if (rt.localPosition.x <= -1080)
             {
                 if (rt.localPosition.x != -1620)
                     rt.localPosition = new Vector3(-1620, rt.localPosition.y, rt.localPosition.z);
             }

         }*/
        if (rt.localPosition.x > 1080)
        {
            if (rt.localPosition.x != 1620)
            {
                transform.DOLocalMoveX(1620, 0.5f);
            }

        }
        else if (rt.localPosition.x <= 1080 && rt.localPosition.x > 0)
        {
            if (rt.localPosition.x != 540)
                transform.DOLocalMoveX(540, 0.5f);
        }
        else if (rt.localPosition.x <= 0 && rt.localPosition.x > -1080)
        {
            if (rt.localPosition.x != -540)
                transform.DOLocalMoveX(-540, 0.5f);

        }
        else if (rt.localPosition.x <= -1080)
        {
            if (rt.localPosition.x != -1620)
                transform.DOLocalMoveX(-1620, 0.5f);
        }
    }
    void TStart()
    {
        tweening = true;
    }
    void TKill()
    {
        tweening = false;
    }
}
