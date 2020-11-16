using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds 
{
    public string name;
    public AudioClip clip;
    private AudioSource source;
    [Range(0f,1f)]
    public float volume = 1f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }
    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    public bool loop;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager self;
    [SerializeField]
    Sounds[] sounds;
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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("sounds" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            sounds[i].volume = 0;
        }
    }
    
    void Start()
    {
        
    }
     public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }
    public void SEoff()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = 0f;
        }
    }
    public void SEon()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = 1f;
        }
    }
}
