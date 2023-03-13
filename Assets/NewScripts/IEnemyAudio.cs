using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAudio 
{

    void SetChaseSound();
    void SetDeathSound();    

    public AudioSource AudioSource
    {
        get;
        set;
    }
    public AudioClip AudioChase
    {
        get;
        set;
    }
    public AudioClip AudioDeath
    {
        get;
        set;
    }
        

}
