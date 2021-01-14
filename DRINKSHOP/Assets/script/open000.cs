using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open000 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 8; i++)
        {

            transform.GetChild(i).transform.rotation = Quaternion.Euler(0, 0, 30 * i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i<= 8; i++)
        {
            transform.GetChild(i).Rotate(0,0,3f);
        }
    }
}
