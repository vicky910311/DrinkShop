using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using System.Collections;

public class MyUIHoverListener : MonoBehaviour
{
    public bool isUIOverride { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
        GameObject g;
        /*g = EventSystem.current.currentSelectedGameObject;
        if (EventSystem.current.currentSelectedGameObject == null || g.layer != 5)
        {
            isUIOverride = false;
        }
        */
        //  isUIOverride = EventSystem.current.IsPointerOverGameObject();
       //  Debug.Log(isUIOverride);
    }
}
