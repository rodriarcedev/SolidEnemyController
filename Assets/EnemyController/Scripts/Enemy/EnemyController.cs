using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("speed/health")]
    [SerializeField]
    private float speedEnemy;

    [SerializeField]
    private float healthEnemy;
    public float HealthEnemy
    {
        get { return healthEnemy; }
        set { healthEnemy = value; }

    }
    private float totalHealth;
    public float BarEnemy => HealthEnemy / totalHealth;


    [Header("LimitsPatrol")]

    [SerializeField]
    private float attackDistance = 1f;
    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public NavMeshAgent navEnemy;
    public float chaseDistance = 5f;
    public float damageEnemy;
    public Animator enemyAnim;
    [SerializeField]
    private Slider sliderHealth;
    public GameObject enemyPointAttack;
    public GameObject playerToAttack;
    public AudioSource audioEnemy;
    public AudioClip chaseSound;
    public AudioClip deathSound;
    private bool soundActive;
    public bool isDeath = false;
    public GameObject sliderEnemy;
    public int amountAttacks = 0;
    private float distanceDestiny;
    public float SpeedEnemy
    {
        get
        {
            return speedEnemy / 10;

        }
        set
        {
            speedEnemy = value;
        }

    }





 
    [SerializeField]
    private EnemyState enemyState = EnemyState.isWalking;

    private void Awake()
    {

        navEnemy = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();

        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {


        totalHealth = HealthEnemy;
        audioEnemy.clip = chaseSound;
        SetSpeed();


    }


    private void SetSpeed()
    {
        navEnemy.acceleration *= SpeedEnemy;
        navEnemy.speed *= SpeedEnemy;
        navEnemy.angularSpeed *= SpeedEnemy;
    }


    // Update is called once per frame
    void Update()
    {

        MoveEnemy();
        AsignPlayer();
        EnemyMotion();



    }
    void MoveEnemy()
    {
        if (!navEnemy.isOnNavMesh)
        {
            NavMeshHit pos1;
            NavMesh.SamplePosition(transform.position, out pos1, 1f, NavMesh.AllAreas);
            if (pos1.position != null)
            {
                navEnemy.Warp(pos1.position);
            }

        }
        if ((!navEnemy.isOnNavMesh) || (!navEnemy.enabled))
        {
            return;
        }

 
    }



    private void EnemyMotion()
    {

        switch (enemyState)
        {
            case EnemyState.isWalking:
                enemyAnim.SetBool("isRunning", false);
                enemyAnim.SetBool("isWalking", true);
                navEnemy.isStopped = false;
                SetNewRandomDestination();
                soundActive = true;
                enemyAnim.SetBool("isAttack", false);
                enemyAnim.SetBool("isAttack2", false);

                break;
            case EnemyState.isChasing:
                ChasePlayer();


                break;
            case EnemyState.isAttack:
                enemyAnim.SetBool("isWalking", false);
                enemyAnim.SetBool("isRunning", false);
                navEnemy.isStopped = true;

                if (amountAttacks >= 2)
                {
                    enemyAnim.SetBool("isAttack", false);
                    enemyAnim.SetBool("isAttack2", true);


                }
                else
                {
                    enemyAnim.SetBool("isAttack2", false);
                    enemyAnim.SetBool("isAttack", true);

                                                           

                }


                break;

            case EnemyState.isDeath:
                 enemyState = EnemyState.isFinish;
                break;
            case EnemyState.isFinish:
                enemyAnim.SetBool("isWalking", false);
                enemyAnim.SetBool("isRunning", false);
                enemyAnim.SetBool("isDeath", true);

                break;

        }


    }


    private void ChasePlayer()
    {
        transform.LookAt(playerToAttack.transform.position);
        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isRunning", true);
        navEnemy.isStopped = false;
        navEnemy.SetDestination(playerToAttack.transform.position);
        enemyAnim.SetBool("isAttack", false);
        enemyAnim.SetBool("isAttack2", false);

        if (soundActive)
        {
            audioEnemy.Play();
            soundActive = false;

        }
                
    }

    private void SpeedRunning()
    {
        navEnemy.speed = 1.2f;
        navEnemy.angularSpeed = 75f;
        navEnemy.acceleration = 5.5f;
    }
    private void SpeedWalking()
    {
        navEnemy.speed = 1f;
        navEnemy.angularSpeed = 60f;
        navEnemy.acceleration = 4f;
    }


    private void AsignPlayer()
    {
        if (isDeath)
            return;

        if (playerToAttack != null)
        {
            double distancePlayer = Vector3.Distance(transform.position, playerToAttack.transform.position);
            distancePlayer = System.Math.Round(distancePlayer, 1);






            if (distancePlayer > chaseDistance)
            {
                enemyState = EnemyState.isWalking;
                playerToAttack = null;
                return;
            }

            if ((distancePlayer <= attackDistance))
            {
                enemyState = EnemyState.isAttack;
                return;
            }

            if ((distancePlayer <= chaseDistance) && (distancePlayer > attackDistance + 0.5f))
            {
                enemyState = EnemyState.isChasing;
                return;
            }





            return;
        }

        else
        {
            GetNewPlayerPos();
        }




    }


    private void GetNewPlayerPos()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance > chaseDistance)
            {
                enemyState = EnemyState.isWalking;
            }


            if (distance <= chaseDistance)
            {
                enemyState = EnemyState.isChasing;
                playerToAttack = player;
                return;

            }


            if ((distance < attackDistance) && (distance >= chaseDistance))
            {

                enemyState = EnemyState.isAttack;
            }
        }


    }







    public void DamageEnemy(float damage, string pointAttack)
    {
        if (pointAttack == "Head")
        {
            enemyAnim.SetTrigger("Hurt");
        }

        if (isDeath)
            return;
        HealthEnemy -= damage;
        sliderHealth.value = BarEnemy;

        if (HealthEnemy <= 0)
        {
            enemyAnim.SetBool("isWalking", false);
            enemyAnim.SetBool("isRunning", false);
            enemyAnim.SetBool("isDeath", true);
            audioEnemy.clip = deathSound;// Play Sound Death
            audioEnemy.Play();
            EnemyDeath();
            return;

        }

    }


    public void EnemyDeath()
    {

        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isRunning", false);
        enemyAnim.SetBool("isDeath", true);
        sliderEnemy.SetActive(false);




        if (!navEnemy.enabled)
        {
            navEnemy.isStopped = true;
        }

        navEnemy.enabled = false;
        navEnemy.velocity = Vector3.zero;
        enemyState = EnemyState.isDeath;
        Invoke("DeleteEnemy", 2f);
        isDeath = true;

    }



    public void DeleteEnemy()
    {

        Destroy(this.gameObject);

    }








    public void ActiveAttackPoint()
    {
        enemyPointAttack.SetActive(true);
        amountAttacks++;
        if (amountAttacks >= 4)
        {
            amountAttacks = 0;
        }

    }

    public void DisablePoint() => enemyPointAttack.SetActive(false);





    public void SetNewRandomDestination()
    {
        enemyAnim.SetBool("isWalking", true);
        //calculate a random position in the field

        if (navEnemy.velocity.sqrMagnitude <= 0)
        {
            enemyAnim.SetBool("isWalking", true);
            float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
            Vector3 ranDir = Random.insideUnitSphere * randRadius;
            ranDir += transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(ranDir, out navHit, randRadius, NavMesh.AllAreas);
            distanceDestiny = Vector3.Distance(transform.position, navHit.position);
            navEnemy.SetDestination(navHit.position);

        }
        enemyState = EnemyState.isWalking;




    }







}
