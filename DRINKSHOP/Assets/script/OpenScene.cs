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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void pressStart()
    {
        AudioManager.self.PlaySound("Click");
        SceneFade.self.Fadeout();

    }
    public void LoadScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        
    }
}
