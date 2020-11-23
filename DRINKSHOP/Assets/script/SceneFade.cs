using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public static SceneFade self;
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
    private Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fadeout()
    {
        anima.SetTrigger("fadeout");
    }
    public void Fadein()
    {
        anima.SetTrigger("fadein");
    }
    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("MainScene");
        }

    }
}
