using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue : EnemyState
{
	public StateAttack stateAttack; 
	public float playerDistance;
	public bool once = false;

	public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
	{
		if (!once)
		{
			enemyBehaviour.agent.speed = 2f;
			enemyBehaviour.agent.acceleration = 4f; 
			enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
			enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
			once = true;
		}

		enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position); 

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
		{
			if (playerDistance <= 3)
			{
				once = false;
				//enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
				//enemyBehaviour.agent.speed = 1f;
				//enemyBehaviour.agent.acceleration = 2f;
				return stateAttack;
			}
		} 
		else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
			if (playerDistance <= 7)
			{
				once = false;
				//enemyBehaviour.agent.stoppingDistance = 6f;
				//enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
				//enemyBehaviour.agent.stoppingDistance = 3f; //Maybe does something. Need to check later
				//enemyBehaviour.agent.speed = 0.5f;
				//enemyBehaviour.agent.acceleration = 1f;
				//enemyBehaviour.agent.enabled = false;
				return stateAttack;
			}
		}

		return this; 
	}
}
