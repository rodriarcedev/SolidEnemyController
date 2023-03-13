using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAnim 
{
    public Animator EnemyAnim
    {
        get;
        set;

    }

    void SetTriggerAnim(string anim);
    void SetBoolAnim(string anim, bool isTrue);

}
