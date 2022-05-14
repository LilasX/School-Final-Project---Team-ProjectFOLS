using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEscape : EnemyState
{
    public StateWander stateWander; 
    public Vector3 escapePos; 
    public float playerDistance = 0;
    public float escapeDistance = 0; 
    public bool once = false; 

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour) 
    {
        if (escapeDistance <= enemyBehaviour.agent.stoppingDistance) 
        {
            if(!once)
            {
                enemyBehaviour.agent.speed = 7f; 
                enemyBehaviour.agent.acceleration = 12; 
                once = true; 
            }
            float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
            float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius); 
            escapePos = new Vector3(randomX, transform.position.y, randomZ);

            enemyBehaviour.agent.SetDestination(escapePos);
            enemyBehaviour.agent.isStopped = false;
        }

        escapeDistance = Vector3.Distance(escapePos, transform.position); 
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position); 

        //----- ----- Condition To Go To Script StateWander ----- -----
        if (playerDistance >= 20)
        {
            enemyBehaviour.agent.speed = 3.5f; 
            enemyBehaviour.agent.acceleration = 8;
            enemyBehaviour.GetComponent<EnemyMain>().Hp = 50; 
            playerDistance = 0;
            escapeDistance = 0; 
            once = false; 
            return stateWander; 
        }

        return this; 
    }
}
