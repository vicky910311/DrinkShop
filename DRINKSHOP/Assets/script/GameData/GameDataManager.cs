using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDataManager : MonoBehaviour
{
    public static GameDataManager self;
    public DrinkDataList Drink;
    public StaffDataList Staff;
    public ClientDataList Client;
    public LevelDataList Level;
    private void Awake()
    {
        if (self == null)
        {
            self = this;
            DontDestroyOnLoad(this);
        }
        else if (this != self)
        {
            Destroy(gameObject);
        }
    }

}
