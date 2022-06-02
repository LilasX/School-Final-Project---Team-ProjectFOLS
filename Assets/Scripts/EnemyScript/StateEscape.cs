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
        if (!once)
        {
            enemyBehaviour.agent.speed = 3f;
            enemyBehaviour.agent.acceleration = 6f;
            enemyBehaviour.enemyAnim.SetBool("IsRunning", true);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once = true;
        }

        if (escapeDistance <= enemyBehaviour.agent.stoppingDistance) 
        {
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
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            enemyBehaviour.GetComponent<EnemyMain>().GetCurrentHP = 50; 
            playerDistance = 0;
            escapeDistance = 0; 
            once = false; 
            return stateWander; 
        }

        return this; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(escapePos, 0.3f);
    }
}
