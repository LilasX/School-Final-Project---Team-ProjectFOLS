using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : EnemyState
{
    public StatePursue statePursue; 
    public StateEscape stateEscape;
    public Vector3 wanderPos;
    public float wanderDistance = 0; 

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (wanderDistance <= enemyBehaviour.agent.stoppingDistance) 
        {
            float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius); 
            float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius); 
            wanderPos = new Vector3(randomX, transform.position.y, randomZ);

            enemyBehaviour.agent.SetDestination(wanderPos); 
            enemyBehaviour.agent.isStopped = false; 
        }

        wanderDistance = Vector3.Distance(wanderPos, transform.position); 

        //----- ----- Condition To Go To Script StateEscape ----- -----
        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30)
        {
            wanderDistance = 0;
            return stateEscape; 
        }

        //----- ----- Condition To Go To Script StatePursue ----- -----
        if (enemyBehaviour.canSeePlayer) 
		{
            wanderDistance = 0; 
			return statePursue; 
        }
		return this;
	}
}
