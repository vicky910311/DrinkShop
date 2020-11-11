using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using System.Collections;

public class MyUIHoverListener : MonoBehaviour
{
    public static MyUIHoverListener self;
    private void Awake()
    {
        self = this;
    }
    public GraphicRaycaster m_Raycaster;
    public Camera uicamera;
    [SerializeField]
    private bool uiOverride;
    public bool isUIOverride { get { return uiOverride; } private set { uiOverride = value; } }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData PointerEventData = new PointerEventData(EventSystem.current);
        PointerEventData.position = Input.mousePosition;
        m_Raycaster.Raycast(PointerEventData, results);
        isUIOverride = results.Count != 0 ? true : false;
        //  isUIOverride = EventSystem.current.IsPointerOverGameObject();
        //  Debug.Log(isUIOverride);
    }
}
