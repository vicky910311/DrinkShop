using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            AudioManager.self.BGMon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           AudioManager.self.PlaySound("Click");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void pressStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
