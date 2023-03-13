using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    isWalking,
    isChasing,
    isAttack,
    isDeath,
    isFinish
};


public class EnemyMove : MonoBehaviour, IEnemyMove
{
    [field : SerializeField]
    public NavMeshAgent NavMeshEnemy {
        get;         
        set; 
    }
    public bool IsDeath
    {
        get;
        set;
    }
    [field: SerializeField]
    public float SpeedEnemy { 
        get;         
        set; 
    }
    [field: SerializeField]
    public float AttackDistance { 
        get; 
        set; 
    }
    [field: SerializeField]
    public float PatrolRadiusMin { 
        get; 
        set;
    
    }
    [field: SerializeField]
    public float PatrolRadiusMax { 
        get;
        set;
    }
    [field: SerializeField]
    public float ChaseDistance { 
        get; 
        set;
    }
    [field: SerializeField]
    public EnemyState EnemyState {        
        get; 
        set; 
    
    }
    public GameObject PlayerToAttack
    {
        get;
        set;
    }
    public IEnemyAnim EnemyAnim {
        get; 
        
        set; }
    public IEnemyAttack EnemyAttack { get; set; }

    public void AsignPlayer()
    {

        if (IsDeath)
            return;

        if (PlayerToAttack != null)
        {
            double distancePlayer = Vector3.Distance(transform.position, PlayerToAttack.transform.position);
            distancePlayer = System.Math.Round(distancePlayer, 1);






            if (distancePlayer > ChaseDistance)
            {
                EnemyState = EnemyState.isWalking;
                PlayerToAttack = null;
                return;
            }

            if ((distancePlayer <= AttackDistance))
            {
                EnemyState = EnemyState.isAttack;
                return;
            }

            if ((distancePlayer <= ChaseDistance) && (distancePlayer > AttackDistance + 0.5f))
            {
                EnemyState = EnemyState.isChasing;
                return;
            }





            return;
        }

        else
        {
            GetNewPlayerPos();
        }


    }

    public void ChasePlayer()
    {
        transform.LookAt(PlayerToAttack.transform.position);
        EnemyAnim.SetBoolAnim("isWalking", false);
        EnemyAnim.SetBoolAnim("isRunning", true);
        NavMeshEnemy.isStopped = false;
        NavMeshEnemy.SetDestination(PlayerToAttack.transform.position);
        EnemyAnim.SetBoolAnim("isAttack", false);
        EnemyAnim.SetBoolAnim("isAttack2", false);
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        //TestGenericClass<BoxCollider> newClass = new TestGenericClass<Rigidbody>(rigidBody);

        //if (soundActive)
        //{
        //    audioEnemy.Play();
        //    soundActive = false;

        //}

    }

    public void DeleteEnemy()
    {
        Destroy(this.gameObject);
    }

    public void EnemyDeath()
    {
        EnemyAnim.SetBoolAnim("isWalking", false);
        EnemyAnim.SetBoolAnim("isRunning", false);
        EnemyAnim.SetBoolAnim("isDeath", true);
        //sliderEnemy.SetActive(false);




        if (!NavMeshEnemy.enabled)
        {
            NavMeshEnemy.isStopped = true;
        }

        NavMeshEnemy.enabled = false;
        NavMeshEnemy.velocity = Vector3.zero;
        EnemyState = EnemyState.isDeath;
        Invoke("DeleteEnemy", 2f);
        IsDeath = true;
    }

    public void EnemyMotion()
    {
        switch (EnemyState)
        {
            case EnemyState.isWalking:
                EnemyAnim.SetBoolAnim("isRunning", false);
                EnemyAnim.SetBoolAnim("isWalking", true);
                NavMeshEnemy.isStopped = false;
                SetNewRandomDestination();
                //soundActive = true;
                EnemyAnim.SetBoolAnim("isAttack", false);
                EnemyAnim.SetBoolAnim("isAttack2", false);

                break;
            case EnemyState.isChasing:
                ChasePlayer();


                break;
            case EnemyState.isAttack:
                EnemyAnim.SetBoolAnim("isWalking", false);
                EnemyAnim.SetBoolAnim("isRunning", false);
                NavMeshEnemy.isStopped = true;
                EnemyAttack.SwitchAnimations();
       


                break;

            case EnemyState.isDeath:
                EnemyState = EnemyState.isFinish;
                break;
            case EnemyState.isFinish:
                EnemyAnim.SetBoolAnim("isWalking", false);
                EnemyAnim.SetBoolAnim("isRunning", false);
                EnemyAnim.SetBoolAnim("isDeath", true);
                
                break;

        }


    }

    public void GetNewPlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > ChaseDistance)
            {
                EnemyState = EnemyState.isWalking;
            }


            if (distance <=ChaseDistance)
            {
                EnemyState = EnemyState.isChasing;
                PlayerToAttack = player;
                return;

            }


            if ((distance < AttackDistance) && (distance >= ChaseDistance))
            {

                EnemyState = EnemyState.isAttack;
            }
        }
    }

    public void MoveEnemy()
    {
        if (!NavMeshEnemy.isOnNavMesh)
        {
            NavMeshHit pos1;
            NavMesh.SamplePosition(transform.position, out pos1, 1f, NavMesh.AllAreas);
            if (pos1.position != null)
            {
                NavMeshEnemy.Warp(pos1.position);
            }

        }
        if ((!NavMeshEnemy.isOnNavMesh) || (!NavMeshEnemy.enabled))
        {
            return;
        }
    }

    public void SetNewRandomDestination()
    {
       
    }

    public void SetSpeed()
    {
        
    }

    public void SetSpeedRunning()
    {
        NavMeshEnemy.speed = 1.2f;
        NavMeshEnemy.angularSpeed = 75f;
        NavMeshEnemy.acceleration = 5.5f;

    }

    public void SetSpeedWalking()
    {
        NavMeshEnemy.speed = 1f;
        NavMeshEnemy.angularSpeed = 60f;
        NavMeshEnemy.acceleration = 4f;
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyState = EnemyState.isWalking;
        NavMeshEnemy = GetComponent<NavMeshAgent>();
        EnemyAttack = GetComponent<IEnemyAttack>();
        EnemyAnim = GetComponent<IEnemyAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        AsignPlayer();
        EnemyMotion();
    }
}
