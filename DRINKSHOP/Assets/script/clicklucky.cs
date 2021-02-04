using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicklucky : MonoBehaviour
{
    Animator ani;
    private MyUIHoverListener uiListener;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        uiListener = MyUIHoverListener.self;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*private void OnMouseDown()
    {
        ani.SetTrigger("click");
        AudioManager.self.PlaySound("lucky");
    }*/
    void OnMouseUp()
    {
        if (uiListener.isUIOverride)
        {
            Debug.Log("Cancelled OnMouseDown! A UI element has override this object!");
        }
        else
        {
            ani.SetTrigger("click");
            AudioManager.self.PlaySound("lucky");

        }

    }
}
