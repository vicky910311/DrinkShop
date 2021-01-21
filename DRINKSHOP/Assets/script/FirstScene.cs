using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour
{
    private SaveandLoad saveandLoad = new SaveandLoad();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void OnApplicationQuit()
    {
        saveandLoad.SavePlayer(PlayerDataManager.self.Player);
        Debug.Log("Save");
    }
}
