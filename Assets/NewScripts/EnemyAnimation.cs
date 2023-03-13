using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour, IEnemyAnim
{
    public Animator EnemyAnim { get; set; }

    public void SetBoolAnim(string anim, bool isTrue)
    {
        EnemyAnim.SetBool(anim, isTrue);
    }

    public void SetTriggerAnim(string anim)
    {
        EnemyAnim.SetTrigger(anim);
    }


    private void Awake()
    {
       EnemyAnim = GetComponent<Animator>();
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
