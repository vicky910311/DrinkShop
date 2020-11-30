using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.height/Screen.width > 16f/9f)
        {
            float R = (1920f / Screen.height) * (Screen.width / 1080f);
            this.transform.localScale = new Vector3(R, R, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
