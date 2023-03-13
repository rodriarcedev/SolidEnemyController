using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IEnemyHealth
{
   
    IEnemyAnim EnemyAnim
    {
        get;
        set;
    }
    IEnemyMove EnemyMove
    {
        get;
        set;
    }
    public bool isDeath
    {
        get;
        set;
    }
    public float HealthEnemy
    {
        get;
        set;
    }
    public float TotalHealth
    {
        get;
        set;
    }
    public float BarHealth
    {
        get;
        
    }
    public Slider BarSlider
    {
        get;
        set;
    }
    public IEnemyAudio EnemyAudio
    {
        get;
        set;
    }

    void DamageEnemy(float damage, string pointDamage);

}

