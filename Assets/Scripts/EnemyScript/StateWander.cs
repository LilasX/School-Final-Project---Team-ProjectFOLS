using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : EnemyState
{
    public StatePursue statePursue; //To use Script StatePursue in this script
    public StateEscape stateEscape; //To use Script StateEscape in this script
    public Vector3 wanderPos; //Position for Enemy to Wander towards to
    public float wanderDistance = 0; //Distance between Enemy and wanderPos

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour) //Need to Rethink how to do it after StateEscape goes to StateWander, since enemy stops after a time
    {
        if (wanderDistance <= enemyBehaviour.agent.stoppingDistance) //if wanderDistance is equal or inferior to agent's stoppingDistance
        {
            float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius); //Retrieve Random X Coordinate
            float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius); //Retrieve Random Z Coordinate
            wanderPos = new Vector3(randomX, transform.position.y, randomZ); //Set wanderPos with Random X and Z Coordinates

            enemyBehaviour.agent.SetDestination(wanderPos); //set agent's destination to wanderPos
            enemyBehaviour.agent.isStopped = false; //Set agent's isStopped to false, so it does not stop
        }

        wanderDistance = Vector3.Distance(wanderPos, transform.position); //Retrieve Distance between this gameObject and wanderPos

        //----- ----- Condition To Go To Script StateEscape ----- -----
        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30) //If this gameObject Health is equal or inferior to 30
        {
            wanderDistance = 0;
            return stateEscape; //return a stateEscape, so it goes to Script StateEscape
        }

        //----- ----- Condition To Go To Script StatePursue ----- -----
        if (enemyBehaviour.canSeePlayer) //if canSeePlayer is true, meaning this gameObject found player
		{
            wanderDistance = 0; //Return wanderDistance to 0, so that the first if condition can be used 
			return statePursue; //return a statePursue, so it goes to Script StatePursue
        }
		return this; //return a stateWander, so it stays in this Script
	}
}
