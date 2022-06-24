using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWaiting : EnemyState
{
    public StatePursue statePursue;
    public bool start = false;
    public bool once = false;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once = true;
        }

        if (start)
        {
            start = false;
            once = false;
            return statePursue;
        }

        return this;
    }
}
