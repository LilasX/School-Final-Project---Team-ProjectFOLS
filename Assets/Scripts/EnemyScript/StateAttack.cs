using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyState
{
    public StateEscape stateEscape;
    public StatePursue statePursue;
    public float playerDistance;
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.GetComponent<EnemyMain>().AttackPlayer();

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //----- ----- Condition To Go To Script StateEscape ----- -----
        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30)
        {
            return stateEscape;
        }

        //----- ----- Condition To Go To Script StatePursue ----- -----
        if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
        {
            if (playerDistance >= 5)
            {
                return statePursue;
            }
        }
        else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
            if (playerDistance >= 10)
            {
                return statePursue;
            }
        }

        return this;
    }
}
