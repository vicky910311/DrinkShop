using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeDrink : MonoBehaviour
{
    bool Down = false;
    public int Count = 0;
    bool up, uptemp;
    public bool shake;
    bool onit = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        UIManager.self.OpenMakeAllWindow();
        Count = 0;
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
