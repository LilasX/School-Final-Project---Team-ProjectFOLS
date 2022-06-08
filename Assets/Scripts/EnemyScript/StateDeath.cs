using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeath : EnemyState
{
    public bool once = false;
    public RuntimeAnimatorController deathAnimator;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); //Have another SetDestination after Death Animation is Done
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            enemyBehaviour.enemyAnim.runtimeAnimatorController = deathAnimator;
            once = true;
        }

        return this;
    }
}
