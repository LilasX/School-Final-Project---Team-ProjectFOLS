using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : EnemyState
{
    public StatePursue statePursue;
    public StateEscape stateEscape;
    public Vector3 wanderPos;
    public Vector3 wanderPosOrigin;
    public float wanderDistance = 0;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour) //Need to Rethink how to do it after StateEscape goes to StateWander, since enemy stops after a time
    {
        float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
        float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
        wanderPos = new Vector3(randomX, transform.position.y, randomZ);

        if (wanderDistance <= enemyBehaviour.agent.stoppingDistance)
        {
            enemyBehaviour.agent.SetDestination(wanderPos);
            wanderPosOrigin = wanderPos;
            enemyBehaviour.agent.isStopped = false;
        }

        wanderDistance = Vector3.Distance(wanderPosOrigin, transform.position);

        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30)
            return stateEscape;

        if (enemyBehaviour.canSeePlayer)
		{
			return statePursue;
		}
		return this;
	}
}
