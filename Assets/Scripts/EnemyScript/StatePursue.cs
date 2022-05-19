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
			enemyBehaviour.agent.acceleration = 8;
			once = true;
		}

		enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position); 

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

		//----- ----- Condition To Go To Script StateEscape ----- -----
		if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30)
		{
			once = false;
			return stateEscape;
		}

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
		{
			if (playerDistance <= 6)
			{
				once = false;
				return stateAttack;
			}
		} 
		else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
			if (playerDistance <= 11)
			{
				once = false;
				return stateAttack;
			}
		}

		return this; 
	}
}
