using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue : EnemyState
{
	public StateAttack stateAttack; 
	public StateEscape stateEscape; 
	public float playerDistance;
	public bool once = false;

	public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
	{
		if (!once)
		{
			enemyBehaviour.agent.speed = 3.5f;
			enemyBehaviour.agent.acceleration = 8f;
			once = true;
		}

		enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position); 

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

		//----- ----- Condition To Go To Script StateEscape ----- -----
		if (enemyBehaviour.GetComponent<EnemyMain>().GetCurrentHP <= 30)
		{
			once = false;
			return stateEscape;
		}

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
		{
			if (playerDistance <= 5)
			{
				once = false; 
				enemyBehaviour.agent.speed = 1f;
				enemyBehaviour.agent.acceleration = 2f;
				return stateAttack;
			}
		} 
		else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
			if (playerDistance <= 10)
			{
				once = false;
				//enemyBehaviour.agent.stoppingDistance = 6f;
				enemyBehaviour.agent.speed = 0.5f;
				enemyBehaviour.agent.acceleration = 1f;
				//enemyBehaviour.agent.enabled = false;
				return stateAttack;
			}
		}

		return this; 
	}
}
