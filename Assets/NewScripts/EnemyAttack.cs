using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{

    [field: SerializeField]
    public GameObject EnemyAttackPoint { get; set; }
    public int AmountAttacks { get;set; }
    public IEnemyAnim EnemyAnimations { get;
        set;
    }

    public void ActiveAttackPointEnemy()
    {
        EnemyAttackPoint.SetActive(true);
        AmountAttacks++;
        if (AmountAttacks >= 4)
        {
            AmountAttacks = 0;
        }

    }

    public void DisableAttackPointEnemy()
    {
        EnemyAttackPoint.SetActive(false);
    }

    public void SwitchAnimations()
    {
        if (AmountAttacks >= 2)
        {
            EnemyAnimations.SetBoolAnim("isAttack", false);
            EnemyAnimations.SetBoolAnim("isAttack2", true);


        }
        else
        {
            EnemyAnimations.SetBoolAnim("isAttack2", false);
            EnemyAnimations.SetBoolAnim("isAttack", true);



        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimations = GetComponent<IEnemyAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
