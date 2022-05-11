using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEscape : EnemyState
{
    public StateWander stateWander; //To use Script StateWander in this script
    public Vector3 escapePos; //Position for Enemy to Escape towards to
    public float playerDistance = 0; //Distance between this enemy and escapePos
    public float escapeDistance = 0; //Distance between this enemy and player
    public bool once = false; //Bool once so that it does only Once

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour) //Need to be Reworked 
    {
        if (escapeDistance <= enemyBehaviour.agent.stoppingDistance) //if escapeDistance is equal or inferior to agent's stoppingDistance
        {
            if(!once) //if once is false, so not used
            {
                enemyBehaviour.agent.speed = 7f; //Increase agent's speed to 7f
                enemyBehaviour.agent.acceleration = 12; //Increase agent's acceleration to 12f
                once = true; //set once to true
            }
            float randomX = Random.Range(-enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius); //Retrieve Random X Coordinate
            float randomZ = Random.Range(-enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius); //Retrieve Random Z Coordinate
            escapePos = new Vector3(randomX, transform.position.y, randomZ); //Set escapePos with Random X and Z Coordinates

            enemyBehaviour.agent.SetDestination(escapePos); //set agent's destination to escapePos
            enemyBehaviour.agent.isStopped = false; //Set agent's isStopped to false, so it does not stop
        }

        escapeDistance = Vector3.Distance(escapePos, transform.position); //retrieve distance between this enemy and escapePos
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position); //retrieve distance between this enemy and player

        //----- ----- Condition To Go To Script StateWander ----- -----
        if (playerDistance >= 20) //if playerDistance is equal or superior to 20
        {
            enemyBehaviour.agent.speed = 3.5f; //Return agent's speed to 3.5f
            enemyBehaviour.agent.acceleration = 8; //Return agent's acceleration to 8f
            enemyBehaviour.GetComponent<EnemyMain>().Hp = 50; //Because StateMachine will stay at StateEscape since Hp is 30 or Below.
            playerDistance = 0; //Return playerDistance to 0;
            escapeDistance = 0; //Return escapeDistance to 0, so that the first if condition can be used
            once = false; //Return once to false, so that the agent's speed & acceleration can change when this gameObject goes to StateEscape once again
            return stateWander; //return a stateWander, so it goes to Script StateWander
        }

        return this; //return a stateEscape, so it stays in this Script
    }
}
