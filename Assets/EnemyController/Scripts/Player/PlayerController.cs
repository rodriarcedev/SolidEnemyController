using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float healthPlayer;


    public float damagePlayer;
    
    public float speedPlayer = 10f;
    [SerializeField]
    private float gravity = 20f;
    [SerializeField]
    private CharacterController prController;
    [SerializeField]
    private Animator playerAnim;
     public float SpeedPlayer
    {
        get
        {
            return speedPlayer / 10;
        }
        set
        {
            speedPlayer = value;
        }
    }
         
    
           
    private float moveVertical;
    private float moveHorizontal;
    public float rotatePlayer;
    
         
       
    
    // Start is called before the first frame update
    void Start()
    {

        prController = GetComponent<CharacterController>();// Load Components Player
        playerAnim = GetComponent<Animator>();

        

    }




    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            float rotate = Input.GetAxis("Mouse X");
            transform.Rotate(0f, rotate * rotatePlayer, 0f);
        }
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        if ((moveHorizontal != 0) || (moveVertical != 0))
        {
            playerAnim.SetBool("isWalking", true);
        }
        if ((moveHorizontal == 0) && (moveVertical == 0))
        {
            playerAnim.SetBool("isWalking", false);
        }


        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= SpeedPlayer;
        moveDirection.y -= gravity * Time.deltaTime;
        prController.Move(moveDirection);

 

        //}
    }

    public void DamagePlayer(float amount)
    {
        if (healthPlayer <= 0)
        {
            Debug.Log("You Are Death");
            return;
        }
        else
        {
            healthPlayer -= amount;
            if (healthPlayer < 0)
            {
                healthPlayer = 0;
            }

        }
        
    }























}
