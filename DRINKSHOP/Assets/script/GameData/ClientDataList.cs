using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ClientData", menuName = "CreateGameData/ClientDataList")]
public class ClientDataList : ScriptableObject
{
    public List<ClientData> ClientData;
    public ClientComeTime ComeTime;
    public void ClientByLevel()
    {
        for (int i = 0; i < ClientData.Count; i++)
        {
            for (int j = 0; j < ClientData.Count - i - 1; j++)
            {
                if (ClientData[j].UnlockLevel > ClientData[j + 1].UnlockLevel)
                {
                    ClientData temp;
                    temp = ClientData[j];
                    ClientData[j] = ClientData[j + 1];
                    ClientData[j + 1] = temp;
                }
            }
        }
    }

}
