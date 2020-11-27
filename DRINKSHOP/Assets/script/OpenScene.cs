using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    private SaveandLoad saveandLoad = new SaveandLoad();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerDataManager.self.Player.BGMswitch)
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
        if(PlayerDataManager.self.Player.SEswitch)
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
    void OnApplicationQuit()
    {
        saveandLoad.SavePlayer(PlayerDataManager.self.Player);
        Debug.Log("Save");
    }
}
