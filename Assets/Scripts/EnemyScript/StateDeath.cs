using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeath : EnemyState
{
    public bool once = false;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            //enemyAnim._("isDead");
            once = true;
        }

        return this;
    }
}
