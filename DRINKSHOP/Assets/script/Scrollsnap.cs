using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scrollsnap : MonoBehaviour
{
    bool Move = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Move == false )
        {
           
            if (rt.localPosition.x >1080)
            {
               
                transform.DOLocalMoveX(1620, 0.2f) ;
            }
            else if (rt.localPosition.x <= 1080 && rt.localPosition.x > 0f)
            {
                
                transform.DOLocalMoveX(540, 0.2f) ;
            }
            else if(rt.localPosition.x <= 0 && rt.localPosition.x > -1080)
            {
                
                transform.DOLocalMoveX(-540, 0.2f) ;
            }
            else if(rt.localPosition.x <= -1080)
            {
               
                transform.DOLocalMoveX(-1620, 0.2f) ;
            }
        }
        
    }
}
