using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    private SaveandLoad saveandLoad = new SaveandLoad();
    public GameObject staff;
    // Start is called before the first frame update
    void Start()
    {
        int NUM = PlayerDataManager.self.Player.FrontStaff + 1;
        staff.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/staff/角色立繪_00"+ NUM.ToString());
        if (PlayerDataManager.self.Player.BGMswitch)
            AudioManager.self.BGMon();
        if (PlayerDataManager.self.Player.SEswitch)
            AudioManager.self.SEon();
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
    public void LoadDefault()
    {
        if (JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("defaultplayersave"))!= null)
        {
            saveandLoad.loadDefault();
            PlayerDataManager.self.Player = saveandLoad.Player;
        } 
    }

    void OnApplicationQuit()
    {
        saveandLoad.SavePlayer(PlayerDataManager.self.Player);
        Debug.Log("Save");
    }
}
