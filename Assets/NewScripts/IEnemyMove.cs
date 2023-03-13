using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMove
{
    public NavMeshAgent NavMeshEnemy
    {
        get;
        set;

    }
    public float SpeedEnemy
    {
        get;
        set;
    }
    public float AttackDistance
    {
        get;
        set;
    }
    public float PatrolRadiusMin
    {
        get;
        set;
    }
    public float PatrolRadiusMax
    {
        get;
        set;
    }

    public float ChaseDistance
    {
        get;
        set;
    }
    public EnemyState EnemyState
    {
        get;
        set;
    }
    public bool IsDeath
    {
        get;
        set;
    }
    public GameObject PlayerToAttack
    {
        get;
        set;
    }

    void SetSpeed();
    void AsignPlayer();
    void SetSpeedWalking();
    void SetSpeedRunning();
    void GetNewPlayerPos();
    void ChasePlayer();
    void EnemyMotion();
    void MoveEnemy();
    void SetNewRandomDestination();
    void EnemyDeath();
    void DeleteEnemy();
    IEnemyAnim EnemyAnim
    {
        get;
        set;
    }
    IEnemyAttack EnemyAttack
    {
        get;
        set;
    }


}
