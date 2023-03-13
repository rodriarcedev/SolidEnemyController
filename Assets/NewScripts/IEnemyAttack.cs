using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack 
{
    GameObject EnemyAttackPoint
    {
        get;
        set;
    }
    int AmountAttacks
    {
        get;
        set;
    }
    void ActiveAttackPointEnemy();
    void DisableAttackPointEnemy();
    IEnemyAnim EnemyAnimations
    {
        get;
        set;
    }
    void SwitchAnimations();

 
}
