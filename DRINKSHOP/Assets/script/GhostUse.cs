using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;




public class GhostUse : MonoBehaviour
{
    
    //public LayerMask _RaycastCollidableLayers;
    float now, move;
    float MinX = -1.5f, MinY = -1.4f, MaxX = 1.5f, MaxY = 1.4f;
    private MyUIHoverListener uiListener;
    private Animator anima;

    // Start is called before the first frame update
    void Start()
    {
        uiListener = MyUIHoverListener.self;// GameObject.Find("Canvas").GetComponent<MyUIHoverListener>();
        anima = GetComponent<Animator>();
        now = Time.time;
        move = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > now + move)
        {
            float X = Random.Range(0f, 1f);
            float Y = Random.Range(0f, 1f);
            int Dx = Random.Range(0, 2) == 1 ? -1 : 1;
            int Dy = Random.Range(0, 2) == 1 ? -1 : 1;

            float GoX = transform.position.x + X * Dx;
            GoX = Mathf.Clamp(GoX, MinX, MaxX);
            float GoY = transform.position.y + Y * Dy;
            GoY = Mathf.Clamp(GoY, MinY, MaxY);

            Vector3 Go = new Vector3(GoX, GoY, 0);
            float Dis = Vector3.Distance(Go, transform.position);
            if (Dis > 0.7f)
            {
                this.GetComponent<SpriteRenderer>().flipX = Go.x - transform.position.x > 0 ? true : false;
                transform.DOMove(Go, 2f).SetEase(Ease.Linear);
                now = Time.time;
            }
        }
    }
    void OnMouseUp()
    {
        if (uiListener.isUIOverride)
        {
            Debug.Log("Cancelled OnMouseDown! A UI element has override this object!");
        }
        else
        {
            PlayerDataManager.self.Player.CatchGhost++;
            GameObject FX = Instantiate(Resources.Load("Prefabs/CFX_Poof"), transform) as GameObject;
            anima.SetTrigger("click");
            //this.GetComponent<SpriteRenderer>().enabled = false;
            Invoke("dissapear",0.3f);
            Debug.Log(name.ToString() + "被點了一下");
            
        }
        //PerformRaycast();
        
    }
    void dissapear()
    {
        Destroy(gameObject);
    }
    /*void PerformRaycast()
    {
        RaycastHit _Hit;
        float _CheckDistance = 100f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out _Hit, _CheckDistance + 0.1f, _RaycastCollidableLayers);
        
        if (_Hit.collider != this.gameObject)
        {
            Debug.Log("Raycast hit other");
           
        }
        else
        {
            Destroy(gameObject);
        }
        
    }*/
}
