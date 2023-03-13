using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IEnemyHealth
{
  
    public float HealthEnemy {
        get
        {
            return healthEnemy; 
        }
        set
        {
            healthEnemy = value;
        }
    
    }

    public bool isDeath {
        get;
        set;
    }
    [field : SerializeField]
    public Slider BarSlider { get;
        set;

    }
    public float TotalHealth {
        get;
        set;
    
    }
    public float BarHealth =>  HealthEnemy / TotalHealth;

    
  
    public IEnemyAudio EnemyAudio {
        get;
        set;

    }
    public IEnemyAnim EnemyAnim { get; set; }
    public IEnemyMove EnemyMove { get; set; }

    public float healthEnemy;

    public void DamageEnemy(float damage, string pointAttack)
    {
        if (pointAttack == "Head")
        {
            EnemyAnim.SetTriggerAnim("Hurt");
        }

        if (isDeath)
            return;
        HealthEnemy -= damage;
       BarSlider.value = BarHealth;

        if (HealthEnemy <= 0)
        {
            EnemyAnim.SetBoolAnim("isWalking", false);
            EnemyAnim.SetBoolAnim("isRunning", false);
            EnemyAnim.SetBoolAnim("isDeath", true);
            EnemyAudio.SetDeathSound();

            EnemyMove.EnemyDeath();
            return;

        }
    }
    private void Awake()
    {
        EnemyAnim = GetComponent<IEnemyAnim>();
        EnemyAudio = GetComponent<IEnemyAudio>();
        EnemyMove = GetComponent<IEnemyMove>(); 
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
