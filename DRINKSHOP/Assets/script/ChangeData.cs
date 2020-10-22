using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeData : MonoBehaviour
{
    public Data data;
    private void OnGUI()
    {
        if(GUILayout.Button("Use money"))
        {
            data.Money -= 100;
        }
    }
}
