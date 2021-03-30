using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coininfo : MonoBehaviour
{
    public GameObject infowindow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {

        infowindow.SetActive(true);


    }
    private void OnMouseUp()
    {
        infowindow.SetActive(false);
    }
}
