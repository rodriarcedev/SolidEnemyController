using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float damage = GetComponentInParent<EnemyController>().damageEnemy;
            other.gameObject.GetComponent<PlayerController>().DamagePlayer(damage);
            gameObject.SetActive(false);

        }
    }
}
