using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyState
{
    public StatePursue statePursue;
    public float playerDistance;
    private Vector3 target;
    public bool once = false;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once = true;
        }

        enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

        //Look At Player
        target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
        enemyBehaviour.gameObject.transform.LookAt(target);

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //----- ----- Condition To Go To Script StatePursue ----- -----
        if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
        {
            if (playerDistance >= 3)
            {
                //enemyBehaviour.agent.isStopped = false;
                once = false;
                return statePursue;
            }
        }
        else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
            if (playerDistance >= 10)
            {
                //enemyBehaviour.agent.enabled = true;
                //enemyBehaviour.agent.isStopped = false;
                once = false;
                return statePursue;
            }
        }

        return this;
    }
}
