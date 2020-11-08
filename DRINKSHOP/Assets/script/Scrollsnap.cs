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
        
        
    }
    
}
