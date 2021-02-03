using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickcashier : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        ani.SetTrigger("click");
        AudioManager.self.PlaySound("cash");
    }
}
