using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float R = Screen.height / 1920f;
        this.transform.localScale = new Vector3(R,R,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
