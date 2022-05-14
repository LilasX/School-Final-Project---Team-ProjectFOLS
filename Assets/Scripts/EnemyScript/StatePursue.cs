using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue : EnemyState
{
	public StateAttack stateAttack; 
	public StateEscape stateEscape; 
	public float playerDistance; 

	public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
	{
		enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position); 

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position); 

		//----- ----- Condition To Go To Script StateEscape ----- -----
		if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30) 
			return stateEscape; 

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (playerDistance <= 5) 
		{
			return stateAttack; 
		}

		return this; 
	}
}
