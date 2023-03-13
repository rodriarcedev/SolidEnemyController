using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour, IEnemyAudio
{
    [field : SerializeField]
    public AudioSource AudioSource {
        get;
        set;

    }
    [field: SerializeField]
    public AudioClip AudioChase { 
        get;
        set;
    }
    [field: SerializeField]

    public AudioClip AudioDeath {
        get;
        set; 
    }

    public void SetChaseSound()
    {
        AudioSource.clip = AudioChase;
        AudioSource.Play();
    }

    public void SetDeathSound()
    {
        AudioSource.clip = AudioDeath;
        AudioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
