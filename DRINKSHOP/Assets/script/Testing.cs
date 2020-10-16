using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public PlayerData Player;
    public ClientDataList Client;
    public DrinkDataList Drink;
    public LevelDataList Level;
    public StaffDataList Staff;
    public MissionList Mission;
    public DrinkControl DrinkControl = new DrinkControl();
    public ClientControl ClientControl = new ClientControl();
    public StaffControl StaffControl = new StaffControl();
    public EventControl EventControl = new EventControl();
    // Start is called before the first frame update
    void Start()
    {
        DrinkControl.Drink = ClientControl.Drink = EventControl.Drink = Drink;
        DrinkControl.Player = ClientControl.Player = StaffControl.Player = EventControl.Player = Player;
        ClientControl.Client = Client;
        EventControl.Level = Level;
        StaffControl.Staff = Staff;
        EventControl.Mission = Mission;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Incidenthappen()
    {
        int i = Random.Range(1, 5);
        string n = "沒事";
        EventControl.IncidentHappen(i, n);
    }
    public void SellDrinks()
    {
        ClientControl.SelltheDrink();
    }
    public void Develop()
    {
        DrinkControl.DevelopDrink();
    }
    public void MakeAll()
    {
        for (int i = 0;i<Player.CanMake.Count;i++)
        {
            DrinkControl.MakingDrink(Player.CanMake[i]);
            Debug.Log(Player.CanMake[i] + "補齊了");
        }
    }
   
}
