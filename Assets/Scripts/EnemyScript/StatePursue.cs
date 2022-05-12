using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue : EnemyState
{
	public StateAttack stateAttack; //To use Script StateAttack in this script
	public StateEscape stateEscape; //To use Script StateEscape in this script
	public float playerDistance; //Distance between this enemy and player

	public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
	{
		enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position); //set agent's destination to player's position

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position); //Retrieve Distance between this gameObject (enemy) and player

		//----- ----- Condition To Go To Script StateEscape ----- -----
		if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30) //If this gameObject Health is equal or inferior to 30
			return stateEscape; //return a stateEscape, so it goes to Script StateEscape

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (playerDistance <= 5) //If Distance is equal or inferior to 5
		{
			return stateAttack; //return a stateAttack, so it goes to Script StateAttack
		}

		return this; //return a statePursue, so it stays in this Script
	}
}
