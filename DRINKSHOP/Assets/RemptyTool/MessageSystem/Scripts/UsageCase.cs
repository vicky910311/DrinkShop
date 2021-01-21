using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            number = GameManager.self.storynum;
        }
        else if (SceneManager.GetActiveScene().name == "FirstScene")
        {
            SceneFade.self.Fadein();
            number = 0;
        }
        storyimage.GetComponent<Image>().sprite = GameDataManager.self.Staff.StaffData[number].StoryImage;
        msgSys = this.GetComponent<ES_MessageSystem>();
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else
        {
            story[number] = GameDataManager.self.Staff.StaffData[number].Story;
            ReadTextDataFromAsset(story[number]);
        }
           
       // msgSys.SetText("故事開始[w]");
        //add special chars and functions in other component.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        msgSys.AddSpecialCharToFuncMap("end", End);
        msgSys.AddSpecialCharToFuncMap("meow",Meow);
        msgSys.AddSpecialCharToFuncMap("wong", Wong);
        msgSys.AddSpecialCharToFuncMap("clap", Clap);
        msgSys.AddSpecialCharToFuncMap("Pclap", PauseClap);
        // Reading = false;
    }
    

    private void CustomizedFunction()
    {
        Debug.Log("Hi! This is called by CustomizedFunction!");
    }
    
    private void End()
    {
        Reading = false;
        if (PlayerDataManager.self.Player.FirstTime == true)
        {
            if (PlayerDataManager.self.Player.SEswitch)
                AudioManager.self.PlaySound("Click");
            SceneFade.self.Fadeout();
           
        }
        else
        {
            UIManager.self.storyWindow.SetActive(false);
            Destroy(this.gameObject);
        }
       
        
    }
    private void Meow()
    {
        AudioManager.self.PlaySound("Meow");
        Debug.Log("Meow");
    }

    private void Wong()
    {
        AudioManager.self.PlaySound("Wong");
    }

    private void Clap()
    {
        AudioManager.self.PlaySound("Clap");
    }
    private void PauseClap()
    {
        AudioManager.self.PauseSound("Clap");
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
        AudioManager.self.PlaySound("Click");
        msgSys.Next();
    } 
}
