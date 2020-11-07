using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ES_MessageSystem))]
public class UsageCase : MonoBehaviour
{
    private ES_MessageSystem msgSys;
    public UnityEngine.UI.Text uiText;
    public TextAsset textAsset;
    public TextAsset[] story;
    public int number;
    private List<string> textList = new List<string>();
    private int textIndex = 0;
    public GameObject storyimage;
    //public static UsageCase self;
    public bool Reading;
    /*private void Awake()
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
        
    }*/

    void Start()
    {
        number = GameManager.self.storynum;
        storyimage.GetComponent<Image>().sprite = GameDataManager.self.Staff.StaffData[number].StoryImage;
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
            ReadTextDataFromAsset(story[number]);
       // msgSys.SetText("故事開始[w]");
        //add special chars and functions in other component.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        msgSys.AddSpecialCharToFuncMap("end", End);
       // Reading = false;
    }
    

    private void CustomizedFunction()
    {
        Debug.Log("Hi! This is called by CustomizedFunction!");
    }
    
    private void End()
    {
        Reading = false;
        UIManager.self.storyWindow.SetActive(false);
        Destroy(this.gameObject);
        
    }

    private void ReadTextDataFromAsset(TextAsset _textAsset)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }

    void Update()
    {
       /*if (Reading == false)
        {
            ReadTextDataFromAsset(story[number]);
            Reading = true;
        }*/
      
        if (Input.GetKeyDown(KeyCode.S))
        {
            //You can sending the messages from strings or text-based files.
            if (msgSys.IsCompleted)
            {
                msgSys.SetText("Send the messages![lr] HelloWorld![w]");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }

        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }

        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count)
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }
    }
    public void Click()
    {
        msgSys.Next();
    } 
}
