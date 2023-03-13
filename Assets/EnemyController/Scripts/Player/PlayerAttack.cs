using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Camera cameraPlayer;
    [Header("Damage Player")]
    public float damageHead;
    public float damageBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootWeapon();
        }
        
    }

    private void ShootWeapon()
    {
        Ray newRay = cameraPlayer.ViewportPointToRay(Vector3.one / 2);
        RaycastHit[] rays = Physics.RaycastAll(newRay);
        foreach (RaycastHit hit in rays)
        {
            if (hit.transform.gameObject.CompareTag("Head"))
            {
                var enemyController = hit.transform.GetComponentInParent<EnemyController>();
                enemyController.DamageEnemy(damageHead, "Head");
                Debug.Log("You Shoot the Head");
                return;


            }
            if (hit.transform.gameObject.CompareTag("Body"))
            {
                var enemyController = hit.transform.GetComponentInParent<EnemyController>();
                enemyController.DamageEnemy(damageBody, "Body");
                Debug.Log("You Shoot the body");
                return;

            }
            
        }
     }
}
