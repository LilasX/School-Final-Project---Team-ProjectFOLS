using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEscape : EnemyState
{
    public StateWander stateWander;
    public Vector3 escapePos;
    public Vector3 escapePosOrigin;
    public float playerDistance = 0;
    public float escapeDistance = 0;
    public bool once = false;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
        float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
        escapePos = new Vector3(randomX, transform.position.y, randomZ);

        if (escapeDistance <= enemyBehaviour.agent.stoppingDistance)
        {
            if(!once)
            {
                enemyBehaviour.agent.speed = 7f;
                enemyBehaviour.agent.acceleration = 12;
                once = true;
            }
            enemyBehaviour.agent.SetDestination(escapePos);
            escapePosOrigin = escapePos;
            enemyBehaviour.agent.isStopped = false;
        }

        escapeDistance = Vector3.Distance(escapePosOrigin, transform.position);
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        if (playerDistance >= 20)
        {
            enemyBehaviour.agent.speed = 3.5f;
            enemyBehaviour.agent.acceleration = 8;
            enemyBehaviour.GetComponent<EnemyMain>().Hp = 50; //Because StateMachine will stay at StateEscape since Hp is 30 or Below.
            return stateWander;
        }

        return this;
    }
}
